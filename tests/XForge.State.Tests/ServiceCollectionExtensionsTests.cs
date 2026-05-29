using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using XForge.State.Extensions;

namespace XForge.State.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public static void AddXForgeState_RegistersStateStoreAsSingleton()
    {
        var services = new ServiceCollection();

        services.AddXForgeState();

        var provider = services.BuildServiceProvider();
        var store1 = provider.GetRequiredService<IStateStore>();
        var store2 = provider.GetRequiredService<IStateStore>();

        store1.Should().BeSameAs(store2);
        store1.Should().BeOfType<StateStore>();
    }

    [Fact]
    public static void AddXForgeState_ThrowsOnNullServices()
    {
        var act = () => ((IServiceCollection)null!).AddXForgeState();

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void AddState_RegistersScopedState()
    {
        var services = new ServiceCollection();

        services.AddState(42);

        var provider = services.BuildServiceProvider();
        using var scope1 = provider.CreateScope();
        using var scope2 = provider.CreateScope();

        var state1 = scope1.ServiceProvider.GetRequiredService<IState<int>>();
        var state2 = scope1.ServiceProvider.GetRequiredService<IState<int>>();
        var state3 = scope2.ServiceProvider.GetRequiredService<IState<int>>();

        state1.Should().BeSameAs(state2); // Same scope
        state1.Should().NotBeSameAs(state3); // Different scope
        state1.Value.Should().Be(42);
        state3.Value.Should().Be(42);
    }

    [Fact]
    public static void AddState_ThrowsOnNullServices()
    {
        var act = () => ((IServiceCollection)null!).AddState(42);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void AddSingletonState_RegistersSingletonState()
    {
        var services = new ServiceCollection();

        services.AddSingletonState(42);

        var provider = services.BuildServiceProvider();
        using var scope1 = provider.CreateScope();
        using var scope2 = provider.CreateScope();

        var state1 = scope1.ServiceProvider.GetRequiredService<IState<int>>();
        var state2 = scope2.ServiceProvider.GetRequiredService<IState<int>>();

        state1.Should().BeSameAs(state2);
        state1.Value.Should().Be(42);
    }

    [Fact]
    public static void AddSingletonState_ThrowsOnNullServices()
    {
        var act = () => ((IServiceCollection)null!).AddSingletonState(42);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void AddXForgeState_MultipleCalls_DoesNotDuplicateRegistration()
    {
        var services = new ServiceCollection();

        services.AddXForgeState();
        services.AddXForgeState();

        var provider = services.BuildServiceProvider();
        var store = provider.GetRequiredService<IStateStore>();

        store.Should().BeOfType<StateStore>();
    }
}
