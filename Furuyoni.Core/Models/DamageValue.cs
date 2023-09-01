namespace Furuyoni.Core.Models;

public class DamageValue : ConstDamageValue
{
    public DamageValue(int? auraDamage, int? lifeDamage, bool overwhelm = false) : base(auraDamage,
        lifeDamage,
        overwhelm)
    {
    }

    private DamageValue(int? auraDamage, int? lifeDamage, int overwhelm) : base(auraDamage, lifeDamage, overwhelm) { }


    public new int? AuraDamage
    {
        get => base.AuraDamage;
        set => base.AuraDamage = value;
    }

    public new int? LifeDamage
    {
        get => base.LifeDamage;
        set => base.LifeDamage = value;
    }

    public new int Overwhelm
    {
        get => _overwhelm;
        set => _overwhelm = value;
    }
    // public DamageValue(int? auraDamage, int? lifeDamage, bool overwhelm = false)
    // {
    //     AuraDamage     = AuraDamage;
    //     LifeDamage     = LifeDamage;
    //     Overwhelm      = overwhelm;
    //     OverwhelmValue = Overwhelm ? 1 : 0;
    // }
    //
    // public int? AuraDamage { get; internal set; }
    // public int? LifeDamage { get; internal set; }
    //
    // public bool Overwhelm
    // {
    //     get => OverwhelmValue > 0;
    //     set
    //     {
    //         OverwhelmValue = value switch
    //         {
    //             true when OverwhelmValue <= 0 => 1,
    //             false                         => 0,
    //             _                             => OverwhelmValue
    //         };
    //     }
    // }
    //
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

    public static implicit operator DamageValue((int? AuraDamage, int? LifeDamage) valueTuple)
    {
        return new DamageValue(valueTuple.AuraDamage, valueTuple.LifeDamage);
    }

    public static implicit operator DamageValue((int? AuraDamage, int? LifeDamage, bool Overwhelm) valueTuple)
    {
        return new DamageValue(valueTuple.AuraDamage, valueTuple.LifeDamage, valueTuple.Overwhelm);
    }

    public DamageValue Copy() { return new DamageValue(AuraDamage, LifeDamage, Overwhelm); }
}