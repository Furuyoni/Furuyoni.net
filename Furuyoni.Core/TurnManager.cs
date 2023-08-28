using Furuyoni.Core.Cards;
using Furuyoni.Core.Crystals;
using Furuyoni.Core.Enums;
using Furuyoni.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Furuyoni.Core;

public class TurnManager
{
    public const int DrawCountOnPrepare = 2;

    private readonly ILogger<TurnManager> _logger;
    private readonly IServiceProvider     _serviceProvider;

    public TurnManager(ILogger<TurnManager> logger, IServiceProvider serviceProvider)
    {
        _logger          = logger;
        _serviceProvider = serviceProvider;
    }

    public PlayerType CurrentPlayer { get; private set; } = PlayerType.PlayerA;

    public event Func<PlayerType, Task>? OnTurnStartAsync;
    public event Func<PlayerType, Task>? OnPreparePhaseStartAsync;
    public event Func<PlayerType, Task>? OnPreparePhaseEndAsync;
    public event Func<PlayerType, Task>? OnMainPhaseStartAsync;
    public event Func<PlayerType, Task>? OnMainPhaseEndAsync;
    public event Func<PlayerType, Task>? OnEndingPhaseStartAsync;
    public event Func<PlayerType, Task>? OnEndingPhaseEndAsync;
    public event Func<PlayerType, Task>? OnTurnEndAsync;

    private async Task PreparePhaseAsync()
    {
        var force = _serviceProvider.GetRequiredKeyedService<ForceManager>(CurrentPlayer);
        force.Add();

        var moving = _serviceProvider.GetRequiredService<MovingProvider>();
        await moving.UpdateGrantAsync();

        var card = _serviceProvider.GetRequiredKeyedService<CardManager>(CurrentPlayer);
        await card.Rebuild();
        card.Draw(DrawCountOnPrepare);
    }

    private async Task MainPhaseAsync()
    {
        var already = false;
        var action  = _serviceProvider.GetRequiredKeyedService<IUserAction>(CurrentPlayer);
        var cards   = _serviceProvider.GetRequiredKeyedService<CardManager>(CurrentPlayer);
        var moving  = _serviceProvider.GetRequiredService<MovingProvider>();
        // var distance    = _serviceProvider.GetRequiredService<DistanceHandler>();
        // var voidHandler = _serviceProvider.GetRequiredService<VoidHandler>();
        // var current     = _serviceProvider.GetRequiredKeyedService<Player>(CurrentPlayer);
        // var opponent = _serviceProvider.GetRequiredKeyedService<Player>(CurrentPlayer switch
        // {
        //     PlayerType.PlayerA => PlayerType.PlayerB,
        //     PlayerType.PlayerB => PlayerType.PlayerA,
        //     _                  => throw new ArgumentOutOfRangeException()
        // });
        while (true)
        {
            var operation = await action.TackActionAsync(cards.Hand);
            if (operation == null) { break; }

            if (operation is IFully)
            {
                if (already)
                {
                    _logger.LogWarning("You can only play Fully card on first operation in turn.");
                    continue;
                }

                already = true;
            }

            await operation.ExecuteAsync(moving, new StatusHub(CurrentPlayer, _serviceProvider), !already);
        }
    }

    private async Task EndingPhaseAsync()
    {
        var card = _serviceProvider.GetRequiredKeyedService<CardManager>(CurrentPlayer);
        await card.DiscardOverloadAsync();
    }

    public async Task BattleExecute()
    {
        try
        {
            while (true)
            {
                using var scope = _logger.BeginScope("Turn: {CurrentPlayer}", CurrentPlayer);
                _logger.LogInformation("Turn started");
                await (OnTurnStartAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);

                try
                {
                    _logger.LogInformation("PreparePhase started");
                    await (OnPreparePhaseStartAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);
                    await PreparePhaseAsync();
                }
                catch (PhaseEndException) { }

                _logger.LogInformation("PreparePhase ended");
                await (OnPreparePhaseEndAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);

                try
                {
                    _logger.LogInformation("MainPhase started");
                    await (OnMainPhaseStartAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);
                    await MainPhaseAsync();
                }
                catch (PhaseEndException) { }

                _logger.LogInformation("MainPhase ended");
                await (OnMainPhaseEndAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);

                try
                {
                    _logger.LogInformation("EndingPhase started");
                    await (OnEndingPhaseStartAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);
                    await EndingPhaseAsync();
                }
                catch (PhaseEndException) { }

                _logger.LogInformation("EndingPhase ended");
                await (OnEndingPhaseEndAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);

                _logger.LogInformation("Turn ended");
                await (OnTurnEndAsync?.Invoke(CurrentPlayer) ?? Task.CompletedTask);

                CurrentPlayer = CurrentPlayer switch
                {
                    PlayerType.PlayerA => PlayerType.PlayerB,
                    PlayerType.PlayerB => PlayerType.PlayerA,
                    _                  => throw new ArgumentOutOfRangeException()
                };
                _logger.LogInformation("Switched player to {CurrentPlayer}", CurrentPlayer);
            }
        }
        catch (FatalDamageException e) { }
    }
}