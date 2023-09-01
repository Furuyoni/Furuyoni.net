namespace Furuyoni.Core.Events.BaseEvents;

public abstract class BaseEvent : Event
{
    protected BaseEvent(IAsyncEnumerable<Event> operation) : base(operation) { }

    protected BaseEvent(IEnumerable<Event> operation) : base(operation) { }
}