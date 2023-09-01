namespace Furuyoni.Core.Models;

public class ConstAttackRange
{
    protected readonly bool[] Range = new bool[11];

    internal ConstAttackRange(bool[] range)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(range.Length, 11);
        Range = range;
    }

    public ConstAttackRange(params int[] range) : this(range as IEnumerable<int>) { }

    public ConstAttackRange(IEnumerable<int> range)
    {
        foreach (var i in range)
        {
            if (i is > 0 and < 11) { Range[i] = true; }
        }
    }

    public ConstAttackRange(Range range)
    {
        var start = range.Start.IsFromEnd ? 10 - range.Start.Value : range.Start.Value;
        var end   = range.End.IsFromEnd ? 10   - range.End.Value : range.End.Value;

        ArgumentOutOfRangeException.ThrowIfLessThan(start, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(end, 10);

        for (var i = range.Start.Value; i <= 10 - range.End.Value; i++) { Range[i] = true; }
    }

    public bool Check(int distance)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(distance, 10);
        ArgumentOutOfRangeException.ThrowIfLessThan(distance, 0);

        return Range[distance];
    }
}