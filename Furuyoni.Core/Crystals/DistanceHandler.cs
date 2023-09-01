namespace Furuyoni.Core.Crystals;

public class DistanceHandler
{
    public const int MaxDistance = 10;

    private int _crystal = MaxDistance;
    private int _frozenCrystal;
    private int _temporaryCrystal;

    public int Value => _crystal - _frozenCrystal + _temporaryCrystal;

    public int Empty => MaxDistance - _crystal - _temporaryCrystal;

    public int MasteryZone { get; internal set; } = 2;

    public bool InMasteryZone => Value <= MasteryZone;


    public int ValueOnStart { get; internal set; } = MaxDistance;

    public int Crystal
    {
        get => _crystal;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, Empty);
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _crystal = value;
        }
    }

    public int AvailableCrystal => _crystal - _frozenCrystal;

    public int FrozenCrystal
    {
        get => _frozenCrystal;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, AvailableCrystal);
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _frozenCrystal = value;
        }
    }

    public int TemporaryCrystal
    {
        get => _temporaryCrystal;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, Empty);
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _temporaryCrystal = value;
        }
    }
}