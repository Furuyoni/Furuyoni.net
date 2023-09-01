using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;
using Furuyoni.Core.Models;

namespace Furuyoni.Cards.M01.Special;

public class S1 : SpecialCard, IAttack
{
    // public override string Name { get; }
    //
    // public override int?                     Cost        => 7;
    // public          IReadOnlyCollection<int> AttackRange { get; } = new[] { 3, 4 };
    // public          int?                     AuraDamage  => 4;
    // public          int?                     LifeDamage  => 4;
    public override string Name { get; }

    public override    int?                                      Cost      => 7;
    public             AttackRange                               Range     { get; } = 3..4;
    public             DamageValue                               Damage    { get; } = (4, 4);
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}