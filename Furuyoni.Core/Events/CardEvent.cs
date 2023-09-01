using Furuyoni.Core.Cards;

namespace Furuyoni.Core.Events;

public class CardEvent : Event
{
    public CardEvent(Card card, IEnumerable<Event> events) : base(events) { Card = card; }
    public Card Card { get; }
}