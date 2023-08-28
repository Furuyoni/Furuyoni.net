using Furuyoni.Core.Crystals;
using Furuyoni.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Furuyoni.Core;

public class Player
{
    public Player([ServiceKey] PlayerType playerType, IKeyedServiceProvider provider)
    {
        PlayerType = playerType;
        Force      = provider.GetRequiredKeyedService<ForceManager>(playerType);
        Life       = provider.GetRequiredKeyedService<LifeHandler>(playerType);
        Armor      = provider.GetRequiredKeyedService<ArmorHandler>(playerType);
        Qi         = provider.GetRequiredKeyedService<QiHandler>(playerType);
    }

    public PlayerType   PlayerType { get; }
    public ForceManager Force      { get; }
    public LifeHandler  Life       { get; }
    public ArmorHandler Armor      { get; }
    public QiHandler    Qi         { get; }
}