using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace XForge.State.Extensions;

/// <summary>
/// Extension methods for registering XForge.State services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds XForge.State services to the specified <see cref="IServiceCollection"/>.
    /// Registers <see cref="IStateStore"/> as a singleton and enables state management.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddXForgeState(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IStateStore, StateStore>();

        return services;
    }

    /// <summary>
    /// Adds a scoped <see cref="IState{T}"/> to the specified <see cref="IServiceCollection"/>.
    /// Each scope (e.g., each Blazor circuit or HTTP request) gets its own state instance.
    /// </summary>
    /// <typeparam name="T">The type of the state value.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="initialValue">The initial value for the state.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddState<T>(this IServiceCollection services, T initialValue)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddScoped<IState<T>>(_ => new State<T>(initialValue));

        return services;
    }

    /// <summary>
    /// Adds a singleton <see cref="IState{T}"/> to the specified <see cref="IServiceCollection"/>.
    /// The state is shared across all consumers within the application.
    /// </summary>
    /// <typeparam name="T">The type of the state value.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="initialValue">The initial value for the state.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddSingletonState<T>(this IServiceCollection services, T initialValue)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IState<T>>(_ => new State<T>(initialValue));

        return services;
    }
}
