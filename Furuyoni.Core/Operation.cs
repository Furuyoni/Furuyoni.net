using Furuyoni.Core.Events;

namespace Furuyoni.Core;

public abstract class Operation
{
    protected internal abstract (Event Event, ICollection<Buff> Buff) Use();
}