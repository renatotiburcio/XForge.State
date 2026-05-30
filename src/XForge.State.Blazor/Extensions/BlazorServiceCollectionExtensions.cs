using Microsoft.Extensions.DependencyInjection;
using XForge.State.Extensions;

namespace XForge.State.Blazor.Extensions;

/// <summary>
/// Extension methods for registering XForge.State Blazor services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class BlazorServiceCollectionExtensions
{
    /// <summary>
    /// Adds XForge.State services configured for Blazor applications.
    /// Registers <see cref="IStateStore"/> as a singleton.
    /// Call this from your Blazor app's Program.cs or Startup.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddXForgeStateBlazor(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddXForgeState();

        return services;
    }
}
