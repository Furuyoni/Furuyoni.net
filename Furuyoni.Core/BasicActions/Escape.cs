using Furuyoni.Core.Crystals;

namespace Furuyoni.Core.BasicActions;

public class Escape : IOperation
{
    public Task ExecuteAsync(MovingProvider moving, StatusHub statusHub, bool canFully)
    {
        moving.Escape();
        return Task.CompletedTask;
    }

    public bool Usable(StatusHub status)
    {
        return status.Distance.InFittingRange && status.Void.Value > 0 && status.Distance.Empty > 0;
    }
}