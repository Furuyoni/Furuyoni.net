namespace Furuyoni.Core.Events.HelperEvents;

public sealed class Judge : Event
{
    public Judge(Func<bool> judgement, Event whenTrue, Event? whenFalse = null) : base(Execute(judgement,
        new[] { whenTrue },
        ArraySegment<Event>.Empty))
    {
    }

    public Judge(Func<bool> judgement, IEnumerable<Event> whenTrue, IEnumerable<Event>? whenFalse = null) : base(
        Execute(judgement, whenTrue, whenFalse))
    {
    }

    public Judge(Func<Task<bool>> judgement, Event whenTrue, Event? whenFalse = null) : base(ExecuteAsync(judgement,
        new[] { whenTrue },
        ArraySegment<Event>.Empty))
    {
    }

    public Judge(Func<Task<bool>> judgement, IEnumerable<Event> whenTrue, IEnumerable<Event>? whenFalse = null) : base(
        ExecuteAsync(judgement, whenTrue, whenFalse))
    {
    }

    private static IEnumerable<Event> Execute(Func<bool> judgement,
        IEnumerable<Event>                               whenTrue,
        IEnumerable<Event>?                              whenFalse)
    {
        var collection = judgement.Invoke() ? whenTrue : whenFalse;
        if (collection == null) { yield break; }

        foreach (var @event in collection) { yield return @event; }
    }

    private static async IAsyncEnumerable<Event> ExecuteAsync(Func<Task<bool>> judgementAsync,
        IEnumerable<Event>                                                     whenTrue,
        IEnumerable<Event>?                                                    whenFalse)
    {
        var collection = await judgementAsync.Invoke() ? whenTrue : whenFalse;
        if (collection == null) { yield break; }

        foreach (var @event in collection) { yield return @event; }
    }
}