using Furuyoni.Core.Crystals;

namespace Furuyoni.Core;

public interface IOperation
{
    Task ExecuteAsync(MovingProvider moving, StatusHub statusHub, bool canFully);

    bool Usable(StatusHub status);
}