using Furuyoni.Core.Events;

namespace Furuyoni.Core.Cards;

public abstract class Card : Operation
{
    public abstract string Name { get; }

    protected internal override (Event Event, ICollection<Buff> Buff) Use() { return Effects(); }

    protected abstract (CardEvent Event, ICollection<Buff> Buff) Effects();
}