using Furuyoni.Core.Actions;

namespace Furuyoni.Core.Events;

public class ActionEvent : Event
{
    public ActionEvent(BaseAction action, IEnumerable<Event> events) : base(events) { Action = action; }
    public BaseAction Action { get; }
}