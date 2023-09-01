using Furuyoni.Core.Enums;
using Furuyoni.Core.Events.HelperEvents;
using Furuyoni.Core.Models;

namespace Furuyoni.Core.Events.BaseEvents;

public class Damage : BaseEvent
{
    public Damage(DamageValue originalValue) : base(ArraySegment<Event>.Empty)
    {
        Value = originalValue;

        var events = new List<Event>
        {
            new Judge(ChooseDamageType, // Choose damage type
                new Moving(AreaType.OpponentAura, AreaType.Shadow, () => Value.AuraDamage!.Value, true),
                new Moving(AreaType.OpponentLife, AreaType.OpponentFlare, () => Value.LifeDamage!.Value))
        };

        Operation = events.ToAsyncEnumerable().GetAsyncEnumerator();
    }

    public DamageValue Value { get; }

    public override IAsyncEnumerator<Event> Operation { get; }

    private async Task<bool> ChooseDamageType()
    {
        if (Value.AuraDamage == null)
        {
            if (Value.LifeDamage == null) { throw new ArgumentNullException(); }

            return false;
        }

        if (Value.LifeDamage == null) { return true; }

        throw new NotImplementedException();
    }
}