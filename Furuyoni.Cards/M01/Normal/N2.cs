using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;
using Furuyoni.Core.Models;

namespace Furuyoni.Cards.M01.Normal;

public class N2 : NormalCard, IAttack
{
    // // public override IList<Effect> Forced()
    // // {
    // //     // TODO +1/+1 when Resolve
    // // }
    //
    // public IReadOnlyCollection<Bonus<Attack>> AttackBonus => new[] { new M01N2Forced() };
    public override string Name { get; }

    public             AttackRange                               Range     { get; } = 3;
    public             DamageValue                               Damage    { get; } = (2, 2);
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}

// public class M01N2Forced : Bonus<Attack>
// {
//     private bool _valid = false;
//
//     public override void Process(Attack target, StatusHub statusHub)
//     {
//         if (!statusHub.Resolve || _valid) { return; }
//
//         target.AuraDamage += 1;
//         target.LifeDamage += 1;
//
//         _valid = true;
//     }
//
//     public override void Fallback(Attack target, StatusHub statusHub)
//     {
//         if (!_valid) { return; }
//
//         target.AuraDamage -= 1;
//         target.LifeDamage -= 1;
//
//         _valid = false;
//     }
//
//     public override void RegisterEvent() { base.RegisterEvent(); }
//     public override void Dispose()       { base.Dispose(); }
// }