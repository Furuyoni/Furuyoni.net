namespace Furuyoni.Core.Cards;

public interface IGrant
{
    int Crystal { get; set; }

    int GetInitialCrystal();
}