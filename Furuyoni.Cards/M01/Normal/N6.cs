using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;

namespace Furuyoni.Cards.M01.Normal;

public class N6 : NormalCard, IEnhancement
{
    // public override string Name          { get; }
    // public          int    DefaultCharge => 2;
    //
    // public void Disenchant()
    // {
    //     // TODO exec attack 1-4 3/-
    public override    string                                    Name      { get; }
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}