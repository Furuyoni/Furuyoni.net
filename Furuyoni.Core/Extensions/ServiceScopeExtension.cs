using Microsoft.Extensions.DependencyInjection;

namespace Furuyoni.Core.Extensions;

public static class ServiceScopeExtension
{
    public static TurnManager StartNewGame(this IServiceScope self)
    {
        return self.ServiceProvider.GetRequiredService<TurnManager>();
    }
}