namespace Furuyoni.Core.Events;

public abstract class Event : IComparable<Event>, IAsyncDisposable
{
    protected Event(IAsyncEnumerable<Event> operation) { Operation = operation.GetAsyncEnumerator(); }


    protected Event(IEnumerable<Event> operation) { Operation = operation.ToAsyncEnumerable().GetAsyncEnumerator(); }

    public         int                     Id        { get; internal set; }
    public         int                     SubId     { get; internal set; }
    public         int                     Turn      { get; internal set; }
    public         int                     Issuer    { get; internal set; }
    public virtual IAsyncEnumerator<Event> Operation { get; }

    public async ValueTask DisposeAsync() { await Operation.DisposeAsync(); }

    // public MoveExecutor                MoveAsync                { get; }
    // public MoveFromEnhancementExecutor MoveFromEnhancementAsync { get; }
    // public MoveToEnhancementExecutor   MoveToEnhancementAsync   { get; }
    // public AddVigorExecutor            AddVigorAsync            { get; }
    // public FlinchExecutor              FlinchAsync              { get; }
    // public DrawExecutor                DrawAsync                { get; }
    // public PutToDeckTopExecutor        PutToDeckTopAsync        { get; }
    // public PutToDeckButtonExecutor     PutToDeckBottomAsync     { get; }
    // public DiscardCardExecutor         DiscardCardAsync         { get; }
    // public CoverCardExecutor           CoverCardAsync           { get; }
    // public RebuildDeckExecutor         RebuildDeckAsync         { get; }
    // public AttackExecutor              AttackAsync              { get; }
    //
    // public delegate Task AddVigorExecutor(TargetType target, int value);
    //
    // public delegate Task AttackExecutor(VirtualAttackCard virtualAttackCard);
    //
    // public delegate Task CoverCardExecutor(TargetType target, int value);
    //
    // public delegate Task DiscardCardExecutor(TargetType target, int value);
    //
    // public delegate Task DrawExecutor(TargetType target, int count);
    //
    // public delegate Task FlinchExecutor(TargetType target);
    //
    // public delegate Task MoveExecutor(AreaType from, AreaType to, int value);
    //
    // public delegate Task MoveFromEnhancementExecutor(IEnhancement from, AreaType to, int value);
    //
    // public delegate Task MoveToEnhancementExecutor(AreaType from, IEnhancement to, int value);
    //
    // public delegate Task PutToDeckButtonExecutor(TargetType target, IList<Card> cards);
    //
    // public delegate Task PutToDeckTopExecutor(TargetType target, IList<Card> cards);
    //
    // public delegate Task RebuildDeckExecutor(TargetType target);

    public int CompareTo(Event? other)
    {
        if (ReferenceEquals(this, other)) { return 0; }

        if (ReferenceEquals(null, other)) { return 1; }

        var idComparison = Id.CompareTo(other.Id);
        return idComparison != 0 ? idComparison : SubId.CompareTo(other.SubId);
    }

    protected internal virtual void RegisterEvent() { }
}