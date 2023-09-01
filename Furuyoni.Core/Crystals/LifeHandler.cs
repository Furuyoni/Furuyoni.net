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

            var origin    = _value <= 3;
            var newStatus = _value <= 3;
            _value = value;

            if (origin != newStatus) { OnResolveStateChange?.Invoke(newStatus); }
        }
    }

    public bool Resolve => _value <= 3;

    public event Action<bool>? OnResolveStateChange;
}