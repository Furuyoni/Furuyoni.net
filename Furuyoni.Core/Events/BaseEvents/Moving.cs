using Furuyoni.Core.Enums;

namespace Furuyoni.Core.Events.BaseEvents;

public class Moving : BaseEvent
{
    public Moving(AreaType from, AreaType to, int value, bool matchTarget = false, bool allOrThrow = true) : base(
        ExecuteAsync(from, to, value, matchTarget, allOrThrow))
    {
    }

    public Moving(AreaType from, AreaType to, Func<int> value, bool matchTarget = false, bool allOrThrow = true) : base(
        ExecuteAsync(from, to, value, matchTarget, allOrThrow))
    {
    }

    private static async IAsyncEnumerable<Event> ExecuteAsync(AreaType from,
        AreaType                                                       to,
        int                                                            value,
        bool                                                           matchTarget = false,
        bool                                                           allOrThrow  = true)
    {
        throw new NotImplementedException();
    }

    private static async IAsyncEnumerable<Event> ExecuteAsync(AreaType from,
        AreaType                                                       to,
        Func<int>                                                      value,
        bool                                                           matchTarget = false,
        bool                                                           allOrThrow  = true)
    {
        throw new NotImplementedException();
    }
}