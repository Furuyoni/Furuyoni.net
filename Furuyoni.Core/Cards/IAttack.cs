namespace Furuyoni.Core.Cards;

public interface IAttack
{
    int DefaultRangeFrom   { get; }
    int DefaultRangeTo     { get; }
    int DefaultArmorDamage { get; }
    int DefaultLifeDamage  { get; }

    Task BeforeAttackAsync();
    Task AfterAttackAsync();
}