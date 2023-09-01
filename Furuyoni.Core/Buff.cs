using Furuyoni.Core.Events;

namespace Furuyoni.Core;

public abstract class Buff
{
    public abstract void Compute(IList<Event> eventStack);
}