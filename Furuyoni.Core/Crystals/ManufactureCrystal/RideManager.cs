using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Furuyoni.Core.Crystals.ManufactureCrystal;

public class RideManager : IDisposable
{
    private readonly DistanceHandler      _distance;
    private readonly EngineManager        _engine;
    private readonly ILogger<RideManager> _logger;
    private readonly PlayerType           _player;
    private readonly TurnManager          _turn;
    private          bool                 _active;

    private int _rideBackward;
    private int _rideForward;

    public RideManager(ILogger<RideManager> logger,
        [ServiceKey] PlayerType             player,
        TurnManager                         turn,
        IKeyedServiceProvider               provider,
        DistanceHandler                     distance)
    {
        _logger   = logger;
        _turn     = turn;
        _distance = distance;
        _engine   = provider.GetRequiredKeyedService<EngineManager>(player);
        _player   = player;
    }

    public bool Active
    {
        get => _active;
        set
        {
            if (value) { _turn.OnTurnStartAsync += OnTurnStart; }
            else { _turn.OnTurnStartAsync       -= OnTurnStart; }

            _active = value;
        }
    }


    public void Dispose()
    {
        if (_active) { _turn.OnTurnStartAsync -= OnTurnStart; }

        GC.SuppressFinalize(this);
    }

    internal int RideForward(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        value = Math.Min(_distance.AvailableCrystal, value);

        _engine.Engine          -= value;
        _rideForward            += value;
        _distance.FrozenCrystal += value;

        return value;
    }

    internal int RidBackward(int value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);

        value = Math.Min(_distance.Empty, value);

        _engine.Engine             -= value;
        _rideBackward              += value;
        _distance.TemporaryCrystal += value;

        return value;
    }

    private Task OnTurnStart(PlayerType player)
    {
        if (player != _player) { return Task.CompletedTask; }

        _distance.FrozenCrystal -= _rideForward;
        _engine.Engine          += _rideForward;
        _rideForward            =  0;

        _distance.TemporaryCrystal -= _rideBackward;
        _engine.Engine             += _rideBackward;
        _rideBackward              =  0;

        return Task.CompletedTask;
    }
}