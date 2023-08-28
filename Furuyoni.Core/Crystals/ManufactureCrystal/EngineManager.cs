using Microsoft.Extensions.Logging;

namespace Furuyoni.Core.Crystals.ManufactureCrystal;

public class EngineManager
{
    public const int DefaultCrystal = 5;

    private readonly ILogger<EngineManager> _logger;
    private          int                    _burned;

    private int _engine = DefaultCrystal;

    public EngineManager(ILogger<EngineManager> logger) { _logger = logger; }

    public int Engine
    {
        get => _engine;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _engine = value;
        }
    }

    public int Burned
    {
        get => _burned;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            _burned = value;
        }
    }

    public void Burn()
    {
        _engine -= 1;
        _burned += 1;
    }
}