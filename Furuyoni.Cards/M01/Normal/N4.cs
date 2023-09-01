using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;
using Furuyoni.Core.Models;

namespace Furuyoni.Cards.M01.Normal;

public class N4 : NormalCard, IThroughout, IAttack
{
    // public override string                   Name        { get; }
    // public          IReadOnlyCollection<int> AttackRange { get; } = new[] { 1, 2 };
    // public          int?                     AuraDamage  => 4;
    // public          int?                     LifeDamage  => 3;
    //
    // public override IList<Effect> Forced()
    // {
    //     // TODO -1-1 when distance <= 2
    // }
    public override string Name { get; }

    public             AttackRange                               Range     { get; } = 1..2;
    public             DamageValue                               Damage    { get; } = (4, 3);
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}