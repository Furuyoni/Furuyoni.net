using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;

namespace Furuyoni.Cards.M01.Normal;

public class N7 : NormalCard, IThroughout, IEnhancement
{
    // public override string Name          { get; }
    // public          int    DefaultCharge => 4;
    //
    // public void Ongoing()
    // {
    //     // TODO other megami' attack get +1/+1 and overwhelm
    // }
    public override    string                                    Name      { get; }
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}