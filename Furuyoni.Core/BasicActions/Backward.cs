using Furuyoni.Core.Crystals;

namespace Furuyoni.Core.BasicActions;

public class Backward : IOperation
{
    public Task ExecuteAsync(MovingProvider moving, StatusHub statusHub, bool canFully)
    {
        moving.Escape();
        return Task.CompletedTask;
    }

    public bool Usable(StatusHub status) { return status.Distance.Empty > 0 && status.Player.Armor.Value > 0; }
}