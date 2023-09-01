using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;
using Furuyoni.Core.Models;

namespace Furuyoni.Cards.M01.Special;

public class S2 : SpecialCard, IAttack, IReaction
{
    // public override string Name     { get; }
    // public override bool   Terminal => true;
    //
    // public override int?                     Cost        => 3;
    // public          IReadOnlyCollection<int> AttackRange { get; } = Enumerable.Range(0, 11).ToArray();
    // public          int?                     AuraDamage  => 2;
    // public          int?                     LifeDamage  => null;
    //
    // public void AfterAttack()
    // {
    //     // TODO reacted attack -2/-0
    // }
    public override string Name { get; }

    public override    int?                                      Cost      => 3;
    public             AttackRange                               Range     { get; } = ..10;
    public             DamageValue                               Damage    { get; } = (2, null);
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}