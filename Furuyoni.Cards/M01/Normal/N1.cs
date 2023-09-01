using Furuyoni.Core;
using Furuyoni.Core.Cards;
using Furuyoni.Core.Events;
using Furuyoni.Core.Models;

namespace Furuyoni.Cards.M01.Normal;

public class N1 : NormalCard, IAttack
{
    public override    string                                    Name      { get; }
    public             AttackRange                               Range     { get; } = 3..4;
    public             DamageValue                               Damage    { get; } = (3, 1);
    protected override (CardEvent Event, ICollection<Buff> Buff) Effects() { throw new NotImplementedException(); }
}