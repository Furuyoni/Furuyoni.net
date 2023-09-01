namespace Furuyoni.Core.Models;

public class AttackRange : ConstAttackRange
{
    private AttackRange(bool[] range) : base(range) { }

    public AttackRange(params int[] range) : base(range) { }

    public AttackRange(IEnumerable<int> range) : base(range) { }

    public AttackRange(Range range) : base(range) { }

    public void AddAtNear(int value = 1)
    {
        var i = 0;
        for (; i < 11; i++)
        {
            if (Range[i]) { break; }
        }

        for (var v = 1; v <= value; v++)
        {
            i = Math.Max(0, i - v);

            Range[i] = true;
        }
    }

    public void AddAtFar(int value = 1)
    {
        var i = 10;
        for (; i >= 0; i--)
        {
            if (Range[i]) { break; }
        }

        for (var v = 1; v <= value; v++)
        {
            i = Math.Min(10, i + v);

            Range[i] = true;
        }
    }


    public static implicit operator AttackRange(Range range) { return new AttackRange(range); }

    public static implicit operator AttackRange(int range) { return new AttackRange(range); }

    public static implicit operator AttackRange(int[] range) { return new AttackRange(range); }

    public AttackRange Copy() { return new AttackRange(Range); }
}