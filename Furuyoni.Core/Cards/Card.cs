using Furuyoni.Core.Crystals;

namespace Furuyoni.Core.Cards;

public abstract class Card : IOperation
{
    public Task ExecuteAsync(MovingProvider moving, StatusHub statusHub, bool canFully)
    {
        throw new NotImplementedException();
    }

    public bool Usable(StatusHub status) { throw new NotImplementedException(); }
}