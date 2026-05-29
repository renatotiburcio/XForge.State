using FluentAssertions;

namespace XForge.State.Tests;

public class StateStoreTests
{
    [Fact]
    public static void GetOrCreate_CreatesNewState()
    {
        var store = new StateStore();

        var state = store.GetOrCreate<int>("counter", 0);

        state.Value.Should().Be(0);
    }

    [Fact]
    public static void GetOrCreate_ReturnsExistingState()
    {
        var store = new StateStore();

        var state1 = store.GetOrCreate<int>("counter", 0);
        state1.Update(42);
        var state2 = store.GetOrCreate<int>("counter", 99);

        state2.Value.Should().Be(42); // Returns existing, ignores new initial value
    }

    [Fact]
    public static void GetOrCreate_ThrowsOnNullKey()
    {
        var store = new StateStore();

        var act = () => store.GetOrCreate<int>(null!, 0);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void GetOrCreate_ThrowsOnEmptyKey()
    {
        var store = new StateStore();

        var act = () => store.GetOrCreate<int>("", 0);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public static void GetOrCreate_ThrowsOnWhitespaceKey()
    {
        var store = new StateStore();

        var act = () => store.GetOrCreate<int>("   ", 0);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public static void Find_ReturnsExistingState()
    {
        var store = new StateStore();
        store.GetOrCreate<int>("counter", 42);

        var state = store.Find<int>("counter");

        state.Should().NotBeNull();
        state!.Value.Should().Be(42);
    }

    [Fact]
    public static void Find_ReturnsNullForMissingKey()
    {
        var store = new StateStore();

        var state = store.Find<int>("nonexistent");

        state.Should().BeNull();
    }

    [Fact]
    public static void Find_ThrowsOnNullKey()
    {
        var store = new StateStore();

        var act = () => store.Find<int>(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void Remove_RemovesState()
    {
        var store = new StateStore();
        store.GetOrCreate<int>("counter", 42);

        var result = store.Remove("counter");
        var state = store.Find<int>("counter");

        result.Should().BeTrue();
        state.Should().BeNull();
    }

    [Fact]
    public static void Remove_ReturnsFalseForMissingKey()
    {
        var store = new StateStore();

        var result = store.Remove("nonexistent");

        result.Should().BeFalse();
    }

    [Fact]
    public static void Remove_ThrowsOnNullKey()
    {
        var store = new StateStore();

        var act = () => store.Remove(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void Clear_RemovesAllStates()
    {
        var store = new StateStore();
        store.GetOrCreate<int>("a", 1);
        store.GetOrCreate<int>("b", 2);
        store.GetOrCreate<int>("c", 3);

        store.Clear();

        store.Find<int>("a").Should().BeNull();
        store.Find<int>("b").Should().BeNull();
        store.Find<int>("c").Should().BeNull();
    }

    [Fact]
    public static void DifferentKeys_StoreDifferentStates()
    {
        var store = new StateStore();

        var counter = store.GetOrCreate<int>("counter", 0);
        var name = store.GetOrCreate<string>("name", "default");

        counter.Update(42);
        name.Update("updated");

        counter.Value.Should().Be(42);
        name.Value.Should().Be("updated");
    }

    [Fact]
    public static async Task GetOrCreate_IsThreadSafe()
    {
        var store = new StateStore();
        var tasks = new List<Task>();

        for (int i = 0; i < 100; i++)
        {
            tasks.Add(Task.Run(() => store.GetOrCreate<int>("shared", 0)));
        }

        await Task.WhenAll(tasks);

        IState<int>? state = store.Find<int>("shared");
        state.Should().NotBeNull();
    }

    [Fact]
    public static void KeyComparison_IsCaseSensitive()
    {
        var store = new StateStore();

        store.GetOrCreate<int>("Counter", 1);
        store.GetOrCreate<int>("counter", 2);

        store.Find<int>("Counter")!.Value.Should().Be(1);
        store.Find<int>("counter")!.Value.Should().Be(2);
    }

    [Fact]
    public static void GetOrCreate_WithReferenceType_InitialValue()
    {
        var store = new StateStore();

        var state = store.GetOrCreate<List<int>>("list", [1, 2, 3]);

        state.Value.Should().Equal(1, 2, 3);
    }
}
