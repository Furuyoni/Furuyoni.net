using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;
using Furuyoni.Core.Models;

namespace Furuyoni.Cards.M01.Normal;

public class N3 : NormalCard, IAttack
{
    // public override string                   Name        { get; }
    // public          IReadOnlyCollection<int> AttackRange { get; } = new[] { 1, 2 };
    // public          int?                     AuraDamage  => 2;
    // public          int?                     LifeDamage  => 1;
    //
    // public void AfterAttack()
    // {
    //     // TODO +1/+1 on the next attack when Resolve
    // }
    public override string Name { get; }

    public             AttackRange                               Range     { get; } = 1..2;
    public             DamageValue                               Damage    { get; } = (2, 1);
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}