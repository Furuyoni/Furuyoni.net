namespace Furuyoni.Core.Events;

public class EventManager
{
    private readonly List<Buff>        _buffs        = new();
    private readonly List<Event>       _eventHistory = new(128);
    private readonly List<List<Event>> _events       = new();
    private readonly List<Event>       _paddingStack = new();
    private          int               _turn;

    public void AddEvent(Operation operation)
    {
        var (@event, buff) = operation.Use();

        @event.Id   = _events.Count;
        @event.Turn = _turn;
        _events.Add(new List<Event>(1) { @event });
        _paddingStack.Add(@event);
        _eventHistory.Add(@event);

        _buffs.AddRange(buff);
    }

    public async Task ExecuteAsync()
    {
        if (_paddingStack.Count == 0) { return; }

        var obj = _paddingStack.Last();
        ArgumentNullException.ThrowIfNull(obj.Operation);

        var events = _paddingStack.AsReadOnly();
        foreach (var @event in events) { @event.ResetBuff(); }

        foreach (var buff in _buffs) { buff.Compute(events); }

        if (!await obj.Operation.MoveNextAsync()) { _paddingStack.Remove(obj); }
        else
        {
            var newEvent = obj.Operation.Current;
            newEvent.Id    = _events.Count;
            newEvent.SubId = _events[obj.Id].Count;
            newEvent.Turn  = _turn;

            _paddingStack.Add(newEvent);
        }
    }

    public void NextTurn() { _turn++; }
}