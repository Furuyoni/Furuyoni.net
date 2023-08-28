using Furuyoni.Core.Crystals;

namespace Furuyoni.Core.BasicActions;

public class Congest : IOperation
{
    public Task ExecuteAsync(MovingProvider moving, StatusHub statusHub, bool canFully)
    {
        moving.Escape();
        return Task.CompletedTask;
    }

    public bool Usable(StatusHub status) { return status.Player.Armor.Value > 0; }
}