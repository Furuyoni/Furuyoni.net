using Furuyoni.Core.Crystals;
using Microsoft.Extensions.DependencyInjection;

namespace Furuyoni.Core;

public class Player
{
    public Player([ServiceKey] PlayerType playerType, IKeyedServiceProvider provider)
    {
        PlayerType = playerType;
        Vigor      = provider.GetRequiredKeyedService<VigorManager>(playerType);
        Life       = provider.GetRequiredKeyedService<LifeHandler>(playerType);
        Armor      = provider.GetRequiredKeyedService<ArmorHandler>(playerType);
        Qi         = provider.GetRequiredKeyedService<QiHandler>(playerType);
    }

    public PlayerType   PlayerType { get; }
    public VigorManager Vigor      { get; }
    public LifeHandler  Life       { get; }
    public ArmorHandler Armor      { get; }
    public QiHandler    Qi         { get; }
}