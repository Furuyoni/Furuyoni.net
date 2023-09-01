using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;

namespace Furuyoni.Cards.M01.Normal;

public class N5 : NormalCard, IAction
{
    //     public override string Name { get; }
    //
    //     public IList<Effect> Execute()
    //     {
    //
    //         BaseEffect.AddVigorAsync(TargetType.Issuer, 1);
    //         /*
    //          * TODO
    //          * +1 vigor
    //          * next other megami' not special attack get No Reactions (Normal) and range low 1
    //          */
    //     }
    public override    string                                    Name      { get; }
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}