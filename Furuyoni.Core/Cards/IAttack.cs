using Furuyoni.Core.Models;

namespace Furuyoni.Core.Cards;

public interface IAttack
{
    AttackRange Range { get; }

    DamageValue Damage { get; }

    // IReadOnlyCollection<int> AttackRange { get; }
    //
    // bool Overwhelm => false;
    //
    // int? AuraDamage { get; }
    // int? LifeDamage { get; }
    //
    // IList<Effect> AfterAttack() => ArraySegment<Effect>.Empty;
    //
    // // (int? Arua, int? Life) GetDamage(StatusHub statusHub);
    // public IReadOnlyCollection<Bonus<Attack>> AttackBonus => ArraySegment<Bonus<Attack>>.Empty;
    //
    // public Attack ToAttack() => new(AttackRange, AuraDamage, LifeDamage, Overwhelm, AttackBonus);
}