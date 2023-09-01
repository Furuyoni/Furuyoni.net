using Furuyoni.Core.Cards;

namespace Furuyoni.Cards;

public abstract class BaseCatalog
{
    public abstract ICollection<NormalCard>  NormalCards  { get; }
    public abstract ICollection<SpecialCard> SpecialCards { get; }

    // public static ICollection<SpecialCard> SpecialCards { get; } = new List<SpecialCard>
    // {
}