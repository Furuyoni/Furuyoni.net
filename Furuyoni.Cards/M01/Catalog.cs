using Furuyoni.Cards.M01.Normal;
using Furuyoni.Cards.M01.Special;
using Furuyoni.Core.Cards;

namespace Furuyoni.Cards.M01;

internal class Catalog : BaseCatalog
{
    public override ICollection<NormalCard> NormalCards { get; } = new List<NormalCard>
    {
        new N1(),
        new N2(),
        new N3(),
        new N4(),
        new N5(),
        new N6(),
        new N7()
    };

    public override ICollection<SpecialCard> SpecialCards { get; } = new List<SpecialCard>
    {
        new S1(), new S2(), new S3(), new S4()
    };
}