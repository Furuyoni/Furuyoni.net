namespace Furuyoni.Core.Crystals;

public class ShadowHandler
{
    private int _value;

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