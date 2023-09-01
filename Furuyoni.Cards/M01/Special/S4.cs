using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;
using Furuyoni.Core.Models;

namespace Furuyoni.Cards.M01.Special;

public class S4 : SpecialCard, IAttack, IThroughout
{
    // public override string                   Name        { get; }
    // public override int?                     Cost        => 5;
    // public          IReadOnlyCollection<int> AttackRange { get; } = new[] { 1, 2, 3, 4 };
    // public          int?                     AuraDamage  => 5;
    // public          int?                     LifeDamage  => 5;
    //
    // public override IList<Effect> Forced()
    // {
    //     // TODO can not use when not Resolve
    // }
    public override string Name { get; }

    public override    int?                                      Cost      => 5;
    public             AttackRange                               Range     { get; } = 1..4;
    public             DamageValue                               Damage    { get; } = (5, 5);
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}