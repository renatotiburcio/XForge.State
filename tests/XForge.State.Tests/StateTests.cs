using FluentAssertions;

namespace XForge.State.Tests;

public class StateTests
{
    [Fact]
    public static void Constructor_SetsInitialValue()
    {
        var state = new State<int>(42);

        state.Value.Should().Be(42);
    }

    [Fact]
    public static void Constructor_AllowsNullForReferenceType()
    {
        var state = new State<string?>(null);

        state.Value.Should().BeNull();
    }

    [Fact]
    public static void Update_WithNewValue_UpdatesValue()
    {
        var state = new State<int>(0);

        state.Update(42);

        state.Value.Should().Be(42);
    }

    [Fact]
    public static void Update_WithSameValue_DoesNotNotify()
    {
        var state = new State<int>(42);
        var notifyCount = 0;
        state.OnChange += _ => notifyCount++;

        state.Update(42);

        notifyCount.Should().Be(0);
        state.Value.Should().Be(42);
    }

    [Fact]
    public static void Update_WithNewValue_NotifiesSubscribers()
    {
        var state = new State<int>(0);
        var received = new List<int>();
        state.OnChange += v => received.Add(v);

        state.Update(1);
        state.Update(2);
        state.Update(3);

        received.Should().Equal(1, 2, 3);
    }

    [Fact]
    public static void Update_WithFunc_UpdatesValue()
    {
        var state = new State<int>(10);

        state.Update(v => v * 2);

        state.Value.Should().Be(20);
    }

    [Fact]
    public static void Update_WithFunc_ThrowsOnNull()
    {
        var state = new State<int>(0);

        var act = () => state.Update((Func<int, int>)null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void Update_WithFunc_DoesNotNotifyWhenSameValue()
    {
        var state = new State<int>(42);
        var notifyCount = 0;
        state.OnChange += _ => notifyCount++;

        state.Update(v => v); // Returns same value

        notifyCount.Should().Be(0);
    }

    [Fact]
    public static void Subscribe_InvokesImmediatelyWithCurrentValue()
    {
        var state = new State<int>(42);
        var received = new List<int>();

        state.Subscribe(v => received.Add(v));

        received.Should().Equal(42);
    }

    [Fact]
    public static void Subscribe_ThrowsOnNullHandler()
    {
        var state = new State<int>(0);

        var act = () => state.Subscribe(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void Subscribe_ReturnsDisposable_ThatUnsubscribes()
    {
        var state = new State<int>(0);
        var received = new List<int>();

        var subscription = state.Subscribe(v => received.Add(v));
        state.Update(1);
        subscription.Dispose();
        state.Update(2);

        received.Should().Equal(0, 1); // Initial + first update, not second
    }

    [Fact]
    public static void Subscribe_MultipleSubscribers_AllNotified()
    {
        var state = new State<int>(0);
        var received1 = new List<int>();
        var received2 = new List<int>();

        state.OnChange += v => received1.Add(v);
        state.OnChange += v => received2.Add(v);

        state.Update(42);

        received1.Should().Equal(42);
        received2.Should().Equal(42);
    }

    [Fact]
    public void OnChange_AddRemove_WorksCorrectly()
    {
        var state = new State<int>(0);
        var received = new List<int>();
        void handler(int v) => received.Add(v);

        state.OnChange += handler;
        state.Update(1);
        state.OnChange -= handler;
        state.Update(2);

        received.Should().Equal(1);
    }

    [Fact]
    public static void DisposeSubscription_MultipleCalls_DoesNotThrow()
    {
        var state = new State<int>(0);
        var subscription = state.Subscribe(_ => { });

        var act = () =>
        {
            subscription.Dispose();
            subscription.Dispose();
        };

        act.Should().NotThrow();
    }

    [Fact]
    public static void Update_ReferenceType_NotifiesOnDifferentReference()
    {
        var state = new State<string>("hello");
        var received = new List<string>();
        state.OnChange += v => received.Add(v);

        state.Update("world");

        received.Should().Equal("world");
    }

    [Fact]
    public static void Update_ReferenceType_DoesNotNotifyOnEqualValue()
    {
        var state = new State<string>("hello");
        var notifyCount = 0;
        state.OnChange += _ => notifyCount++;

        state.Update("hello");

        notifyCount.Should().Be(0);
    }

    [Fact]
    public async Task Value_IsThreadSafe()
    {
        var state = new State<int>(0);
        var tasks = new List<Task>();

        for (int i = 0; i < 100; i++)
        {
            tasks.Add(Task.Run(() => state.Update(v => v + 1)));
        }

        await Task.WhenAll(tasks);

        state.Value.Should().Be(100);
    }

    [Fact]
    public async Task Subscribe_WithConcurrentUpdates_IsThreadSafe()
    {
        var state = new State<int>(0);
        var received = new ConcurrentBag<int>();

        state.Subscribe(v => received.Add(v));

        var tasks = new List<Task>();
        for (int i = 1; i <= 50; i++)
        {
            int val = i;
            tasks.Add(Task.Run(() => state.Update(val)));
        }

        await Task.WhenAll(tasks);

        received.Should().NotBeEmpty();
        state.Value.Should().BeGreaterThan(0);
    }
}
