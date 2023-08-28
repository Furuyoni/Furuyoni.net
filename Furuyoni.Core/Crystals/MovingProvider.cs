using Furuyoni.Core.Cards;
using Furuyoni.Core.Enums;
using Furuyoni.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Furuyoni.Core.Crystals;

public class MovingProvider : IDisposable
{
    private readonly DistanceHandler         _distance;
    private readonly List<IGrant>            _grantCards = new();
    private readonly ILogger<MovingProvider> _logger;
    private readonly UserResource            _playerA;
    private readonly UserResource            _playerB;
    private readonly TurnManager             _turn;
    private readonly VoidHandler             _void;

    public MovingProvider(ILogger<MovingProvider> logger,
        TurnManager                               turn,
        IServiceProvider                          provider,
        VoidHandler                               @void,
        DistanceHandler                           distance)
    {
        _logger   = logger;
        _turn     = turn;
        _void     = @void;
        _distance = distance;

        _playerA = UserResource.GetByPlayer(provider, PlayerType.PlayerA);
        _playerB = UserResource.GetByPlayer(provider, PlayerType.PlayerB);

        turn.OnTurnStartAsync += OnTurnStartAsync;
    }

    public IReadOnlyCollection<IGrant> GrantCards => _grantCards;

    private Task OnTurnStartAsync(PlayerType _)
    {
        _distance.ValueOnStart = _distance.Value;
        return Task.CompletedTask;
    }

    public void Forward()
    {
        if (_distance.AvailableCrystal <= 0 || _distance.InFittingRange) { throw new InvalidOperationException(); }

        if (CurrentPlayer.Armor.Empty <= 0) { throw new InvalidOperationException(); }

        _distance.Crystal           -= 1;
        CurrentPlayer.Armor.Crystal += 1;
    }

    public void Backward()
    {
        if (_distance.Empty <= 0) { throw new InvalidOperationException(); }

        if (CurrentPlayer.Armor.Crystal <= 0) { throw new InvalidOperationException(); }

        CurrentPlayer.Armor.Crystal -= 1;
        _distance.Crystal           += 1;
    }

    public void Equip()
    {
        if (_void.Value <= 0) { throw new InvalidOperationException(); }

        if (CurrentPlayer.Armor.Empty <= 0) { throw new InvalidOperationException(); }

        _void.Value                 -= 1;
        CurrentPlayer.Armor.Crystal += 1;
    }

    public void Congest()
    {
        if (CurrentPlayer.Armor.Empty <= 0) { throw new InvalidOperationException(); }

        CurrentPlayer.Armor.Crystal -= 1;
        CurrentPlayer.Qi.Value      += 1;
    }

    public void Escape()
    {
        if (_void.Value <= 0) { throw new InvalidOperationException(); }

        if (_distance.Empty <= 0 || !_distance.InFittingRange) { throw new InvalidOperationException(); }

        _void.Value       -= 1;
        _distance.Crystal += 1;
    }

    public void Damage(DamageType type, int value, bool isSelf = false)
    {
        if (value <= 0) { return; }

        switch (type)
        {
            case DamageType.Life:
                DamageLife(value, isSelf);
                break;
            case DamageType.Armor:
                DamageArmor(value, isSelf);
                break;
            default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public void Grant(IGrant card, bool additionCrystal = false)
    {
        var value = card.GetInitialCrystal();
        int actual;

        if (additionCrystal) { actual = value; }
        else
        {
            if (_void.Value >= value)
            {
                _void.Value -= value;
                actual      =  value;
            }
            else
            {
                (actual, var padding, _void.Value)    = (_void.Value, value - _void.Value, 0);
                value                                 = Math.Min(padding, CurrentPlayer.Armor.Value);
                (actual, CurrentPlayer.Armor.Crystal) = (actual + value, CurrentPlayer.Armor.Value - value);
            }
        }

        card.Crystal = actual;
        _grantCards.Add(card);
    }

    internal async Task UpdateGrantAsync()
    {
        var remove = new List<IGrant>();
        foreach (var grantCard in _grantCards)
        {
            grantCard.Crystal -= 0;
            _void.Value       += 1;

            if (grantCard.Crystal <= 0) { remove.Add(grantCard); }
        }

        foreach (var grantCard in remove)
        {
            _grantCards.Remove(grantCard);
            if (grantCard is IAsyncDisposable disposable) { await disposable.DisposeAsync(); }
        }
    }

    public void Moving(AreaType from, AreaType to, int value, bool matchTarget, bool allOrThrow)
    {
        if (value == 0) { return; }

        ArgumentOutOfRangeException.ThrowIfNegative(value);

        var target = from switch
        {
            AreaType.SelfLife      => CurrentPlayer.Life.Value,
            AreaType.SelfArmor     => CurrentPlayer.Armor.Value,
            AreaType.SelfQi        => CurrentPlayer.Qi.Value,
            AreaType.Void          => _void.Value,
            AreaType.Distance      => _distance.AvailableCrystal,
            AreaType.OpponentLife  => Opponent.Life.Value,
            AreaType.OpponentArmor => Opponent.Armor.Value,
            AreaType.OpponentQi    => Opponent.Qi.Value,
            _                      => throw new ArgumentOutOfRangeException(nameof(from), from, null)
        };

        if (!matchTarget) { ArgumentOutOfRangeException.ThrowIfGreaterThan(value, target); }

        value = Math.Min(value, target);

        var available = from switch
        {
            AreaType.SelfLife      => CurrentPlayer.Life.Value,
            AreaType.SelfArmor     => CurrentPlayer.Armor.Value,
            AreaType.SelfQi        => CurrentPlayer.Qi.Value,
            AreaType.Void          => _void.Value,
            AreaType.Distance      => _distance.AvailableCrystal,
            AreaType.OpponentLife  => Opponent.Life.Value,
            AreaType.OpponentArmor => Opponent.Armor.Value,
            AreaType.OpponentQi    => Opponent.Qi.Value,
            _                      => throw new ArgumentOutOfRangeException(nameof(from), from, null)
        };

        if (allOrThrow) { ArgumentOutOfRangeException.ThrowIfGreaterThan(value, available); }

        value = Math.Min(value, available);

        switch (from)
        {
            case AreaType.SelfLife:
                try { CurrentPlayer.Life.Value -= value; }
                catch (ArgumentOutOfRangeException) { throw new FatalDamageException(CurrentPlayer.Player); }

                break;
            case AreaType.SelfArmor:
                CurrentPlayer.Armor.Crystal -= value;
                break;
            case AreaType.SelfQi:
                CurrentPlayer.Qi.Value -= value;
                break;
            case AreaType.Void:
                _void.Value -= value;
                break;
            case AreaType.Distance:
                _distance.Crystal -= value;
                break;
            case AreaType.OpponentLife:
                try { Opponent.Life.Value -= value; }
                catch (ArgumentOutOfRangeException) { throw new FatalDamageException(Opponent.Player); }

                break;
            case AreaType.OpponentArmor:
                Opponent.Armor.Crystal -= value;
                break;
            case AreaType.OpponentQi:
                Opponent.Qi.Value -= value;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(from), from, null);
        }

        switch (to)
        {
            case AreaType.SelfLife:
                CurrentPlayer.Life.Value += value;
                break;
            case AreaType.SelfArmor:
                CurrentPlayer.Armor.Crystal += value;
                break;
            case AreaType.SelfQi:
                CurrentPlayer.Qi.Value += value;
                break;
            case AreaType.Void:
                _void.Value += value;
                break;
            case AreaType.Distance:
                _distance.Crystal += value;
                break;
            case AreaType.OpponentLife:
                Opponent.Life.Value += value;
                break;
            case AreaType.OpponentArmor:
                Opponent.Armor.Crystal += value;
                break;
            case AreaType.OpponentQi:
                Opponent.Qi.Value += value;
                break;
            default: throw new ArgumentOutOfRangeException(nameof(to), to, null);
        }
    }

    #region Tools

    private void DamageLife(int value, bool isSelf)
    {
        var entity = isSelf ? CurrentPlayer : Opponent;

        try { entity.Life.Value -= value; }
        catch (ArgumentOutOfRangeException) { throw new FatalDamageException(entity.Player); }

        entity.Qi.Value += value;
    }

    private void DamageArmor(int value, bool isSelf)
    {
        var player = isSelf ? CurrentPlayer : Opponent;

        if (player.Armor.Value < value) { throw new InvalidOperationException(); }

        player.Armor.Crystal -= value;
        _void.Value          += value;
    }

    private UserResource CurrentPlayer =>
        _turn.CurrentPlayer switch
        {
            PlayerType.PlayerA => _playerA,
            PlayerType.PlayerB => _playerB,
            _                  => throw new ArgumentOutOfRangeException()
        };

    private UserResource Opponent =>
        _turn.CurrentPlayer switch
        {
            PlayerType.PlayerA => _playerB,
            PlayerType.PlayerB => _playerA,
            _                  => throw new ArgumentOutOfRangeException()
        };

    private record UserResource(PlayerType Player, LifeHandler Life, ArmorHandler Armor, QiHandler Qi)
    {
        internal static UserResource GetByPlayer(IServiceProvider provider, PlayerType playerType)
        {
            var life  = provider.GetRequiredKeyedService<LifeHandler>(playerType);
            var armor = provider.GetRequiredKeyedService<ArmorHandler>(playerType);
            var qi    = provider.GetRequiredKeyedService<QiHandler>(playerType);
            return new UserResource(playerType, life, armor, qi);
        }
    }

    public void Dispose()
    {
        _turn.OnTurnStartAsync -= OnTurnStartAsync;
        GC.SuppressFinalize(this);
    }

    #endregion
}