using Furuyoni.Core.Crystals;
using Furuyoni.Core.Crystals.ManufactureCrystal;
using Furuyoni.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Furuyoni.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddFuruyoni(this IServiceCollection self)
    {
        self.AddScoped<DistanceHandler>();
        self.AddScoped<VoidHandler>();
        self.AddKeyedScoped<ArmorHandler>(PlayerType.PlayerA);
        self.AddKeyedScoped<ArmorHandler>(PlayerType.PlayerB);
        self.AddKeyedScoped<LifeHandler>(PlayerType.PlayerA);
        self.AddKeyedScoped<LifeHandler>(PlayerType.PlayerB);
        self.AddKeyedScoped<QiHandler>(PlayerType.PlayerA);
        self.AddKeyedScoped<QiHandler>(PlayerType.PlayerB);

        self.AddKeyedScoped<EngineManager>(PlayerType.PlayerA);
        self.AddKeyedScoped<EngineManager>(PlayerType.PlayerB);
        self.AddKeyedScoped<RideManager>(PlayerType.PlayerA);
        self.AddKeyedScoped<RideManager>(PlayerType.PlayerB);

        self.AddKeyedScoped<ForceManager>(PlayerType.PlayerA);
        self.AddKeyedScoped<ForceManager>(PlayerType.PlayerB);

        self.AddKeyedScoped<CardManager>(PlayerType.PlayerA);
        self.AddKeyedScoped<CardManager>(PlayerType.PlayerB);

        self.AddKeyedScoped<Player>(PlayerType.PlayerA);
        self.AddKeyedScoped<Player>(PlayerType.PlayerB);

        self.AddScoped<TurnManager>();
    }
}