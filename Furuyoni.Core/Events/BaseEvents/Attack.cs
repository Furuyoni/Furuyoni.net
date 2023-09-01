using Furuyoni.Core.Events.HelperEvents;
using Furuyoni.Core.Models;

namespace Furuyoni.Core.Events.BaseEvents;

public class Attack : BaseEvent
{
    private readonly AttackRange _originalAttackRange;
    private readonly DamageValue _originalValue;

    public Attack(AttackRange attackRange, DamageValue damageValue) : base(ArraySegment<Event>.Empty)
    {
        _originalAttackRange = attackRange;
        _originalValue       = damageValue;
        AttackRange          = attackRange.Copy();
        ActualValue          = damageValue.Copy();

        var events = new List<Event> { new Judge(CheckRange, new Damage(ActualValue)) };

        Operation = events.ToAsyncEnumerable().GetAsyncEnumerator();
    }

    public ConstAttackRange OriginalAttackRange => _originalAttackRange;
    public ConstDamageValue OriginalValue       => _originalValue;

    public AttackRange AttackRange { get; internal set; }
    public DamageValue ActualValue { get; private set; }

    public override IAsyncEnumerator<Event> Operation { get; }

    private bool CheckRange() { throw new NotImplementedException(); }

    protected override void ResetBuff()
    {
        AttackRange = _originalAttackRange.Copy();
        ActualValue = _originalValue.Copy();
    }
}