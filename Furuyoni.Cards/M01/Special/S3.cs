using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;

namespace Furuyoni.Cards.M01.Special;

public class S3 : ImmediateResurgableSpecialCard, IAction
{
    // public override string Name { get; }
    // public override int?   Cost => 2;
    //
    // public void Execute()
    // {
    //     // move 5 crystals from the shadow to arua
    // }
    //
    // public override bool CheckResurgence()
    // {
    //     // TODO Resolve
    // }
    public override string Name { get; }

    public override    int? Cost { get; }
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
    public override    bool CheckResurgence() { throw new NotImplementedException(); }
}