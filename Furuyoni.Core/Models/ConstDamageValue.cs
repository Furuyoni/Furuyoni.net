namespace Furuyoni.Core.Models;

public class ConstDamageValue
{
    // protected int? _auraDamage;
    // protected int? _lifeDamage;
    protected int _overwhelm;

    protected ConstDamageValue(int? auraDamage, int? lifeDamage, int overwhelm)
    {
        AuraDamage = auraDamage;
        LifeDamage = lifeDamage;
        _overwhelm = overwhelm;
    }

    public ConstDamageValue(int? auraDamage, int? lifeDamage, bool overwhelm = false)
    {
        AuraDamage = auraDamage;
        LifeDamage = lifeDamage;
        _overwhelm = overwhelm ? 1 : 0;
    }

    public int? AuraDamage { get; protected set; }
    public int? LifeDamage { get; protected set; }

    public bool Overwhelm => _overwhelm > 0;

    // public int OverwhelmValue { get; internal set; }
    //
    // public int? ActualAuraDamage =>
    //     !AuraDamage.HasValue
    //         ? null
    //         : AuraDamage < 5 || Overwhelm
    //             ? AuraDamage
    //             : Math.Min(AuraDamage.Value, 5);
    //
    // public void Deconstruct(out int? auraDamage, out int? lifeDamage)
    // {
    //     auraDamage = AuraDamage;
    //     lifeDamage = LifeDamage;
    // }
    //
    // public void Deconstruct(out int? auraDamage, out int? lifeDamage, out bool overwhelm)
    // {
    //     auraDamage = AuraDamage;
    //     lifeDamage = LifeDamage;
    //     overwhelm  = Overwhelm;
    // }


    // public static implicit operator ConstDamageValue((int? AuraDamage, int? LifeDamage) valueTuple)
    // {
    //     return new ConstDamageValue(valueTuple.AuraDamage, valueTuple.LifeDamage);
    // }
    //
    // public static implicit operator ConstDamageValue((int? AuraDamage, int? LifeDamage, bool Overwhelm) valueTuple)
    // {
    //     return new ConstDamageValue(valueTuple.AuraDamage, valueTuple.LifeDamage, valueTuple.Overwhelm);
    // }
    //
    // public static implicit operator DamageValue(ConstDamageValue value)
    // {
    //     return new DamageValue(value.AuraDamage, value.LifeDamage, value.Overwhelm);
    // }
}