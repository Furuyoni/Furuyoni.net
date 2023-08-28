using Furuyoni.Core.Cards;

namespace Furuyoni.Core;

public interface IUserAction
{
    Task<bool> RebuildDeckAsync();

    Task<IEnumerable<Card>> DiscardOverloadAsync(ICollection<Card> collection);

    Task<IOperation?> TackActionAsync(ICollection<Card> collection);
}