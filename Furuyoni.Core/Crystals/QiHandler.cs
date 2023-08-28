namespace Furuyoni.Core.Crystals;

public class QiHandler
{
    private int _value;

    public int Value
    {
        get => _value;
        internal set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _value = value;
        }
    }
}