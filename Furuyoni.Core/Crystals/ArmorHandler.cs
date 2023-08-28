namespace Furuyoni.Core.Crystals;

public class ArmorHandler
{
    public const int MaxArmor     = 5;
    public const int DefaultArmor = 3;

    private int _fakeCrystal;

    public int Value { get; private set; } = DefaultArmor;

    public int Empty => MaxArmor - Value - _fakeCrystal;

    public int Crystal
    {
        get => Value;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, Empty);
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            Value = value;
        }
    }

    public int FakeCrystal
    {
        get => _fakeCrystal;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, Empty);
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _fakeCrystal = value;
        }
    }
}