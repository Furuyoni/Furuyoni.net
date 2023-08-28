using Furuyoni.Core.Crystals;

namespace Furuyoni.Core.BasicActions;

public class Forward : IOperation
{
    public Task ExecuteAsync(MovingProvider moving, StatusHub statusHub, bool canFully)
    {
        moving.Escape();
        return Task.CompletedTask;
    }

    public bool Usable(StatusHub status)
    {
        return status.Distance.AvailableCrystal > 0 && status.Player.Armor.Empty > 0;
    }
}