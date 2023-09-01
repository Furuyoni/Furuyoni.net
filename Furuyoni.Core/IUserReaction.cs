using Furuyoni.Core.Cards;
using Furuyoni.Core.Enums;
using Furuyoni.Core.Events;

namespace Furuyoni.Core;

public interface IUserReaction
{
    Task<bool> RebuildDeckAsync();

    Task<IEnumerable<Card>> DiscardOverloadAsync(ICollection<Card> collection);

    Task<Operation?> TackActionAsync(Event? target = null);
    Task<DamageType> ChooseDamageTypeAsync();
}