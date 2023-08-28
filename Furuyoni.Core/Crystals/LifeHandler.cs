namespace Furuyoni.Core.Crystals;

public class LifeHandler
{
    public const int DefaultLife = 10;

    private int _value = DefaultLife;

    public int Value
    {
        get => _value;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);

            _value = value;
        }
    }
}