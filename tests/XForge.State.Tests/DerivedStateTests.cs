using FluentAssertions;

namespace XForge.State.Tests;

public class DerivedStateTests
{
    [Fact]
    public static void Constructor_ComputesInitialDerivedValue()
    {
        var source = new State<int>(5);
        var derived = new DerivedState<int, string>(source, v => v.ToString());

        derived.Value.Should().Be("5");
    }

    [Fact]
    public static void Constructor_ThrowsOnNullSource()
    {
        var act = () => new DerivedState<int, string>(null!, v => v.ToString());

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void Constructor_ThrowsOnNullTransform()
    {
        var source = new State<int>(5);

        var act = () => new DerivedState<int, string>(source, null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public static void SourceChange_UpdatesDerivedValue()
    {
        var source = new State<int>(5);
        var derived = new DerivedState<int, string>(source, v => $"Value: {v}");

        source.Update(10);

        derived.Value.Should().Be("Value: 10");
    }

    [Fact]
    public static void SourceChange_NotifiesDerivedSubscribers()
    {
        var source = new State<int>(0);
        var derived = new DerivedState<int, int>(source, v => v * 2);
        var received = new List<int>();

        derived.OnChange += v => received.Add(v);
        source.Update(5);
        source.Update(10);

        received.Should().Equal(10, 20);
    }

    [Fact]
    public static void Subscribe_InvokesImmediatelyWithCurrentDerivedValue()
    {
        var source = new State<int>(5);
        var derived = new DerivedState<int, string>(source, v => v.ToString());
        var received = new List<string>();

        derived.Subscribe(v => received.Add(v));

        received.Should().Equal("5");
    }

    [Fact]
    public static void Recalculate_ForcesUpdate()
    {
        var counter = 0;
        var source = new State<int>(5);
        var derived = new DerivedState<int, int>(source, v => v + counter);

        counter = 100;
        derived.Recalculate();

        derived.Value.Should().Be(105);
    }

    [Fact]
    public static void DerivedFromDerived_ChainWorks()
    {
        var source = new State<int>(5);
        var doubled = new DerivedState<int, int>(source, v => v * 2);
        var message = new DerivedState<int, string>(doubled, v => $"Doubled: {v}");

        message.Value.Should().Be("Doubled: 10");

        source.Update(7);

        message.Value.Should().Be("Doubled: 14");
    }

    [Fact]
    public static void Dispose_UnsubscribesFromSource()
    {
        var source = new State<int>(0);
        var derived = new DerivedState<int, int>(source, v => v * 2);
        var received = new List<int>();
        derived.OnChange += v => received.Add(v);

        derived.Dispose();
        source.Update(5);

        derived.Value.Should().Be(0); // Not updated
        received.Should().BeEmpty();
    }

    [Fact]
    public static void Dispose_MultipleCalls_DoesNotThrow()
    {
        var source = new State<int>(0);
        var derived = new DerivedState<int, int>(source, v => v * 2);

        var act = () =>
        {
            derived.Dispose();
            derived.Dispose();
        };

        act.Should().NotThrow();
    }

    [Fact]
    public static void Update_DerivedValueDirectly_Works()
    {
        var source = new State<int>(5);
        var derived = new DerivedState<int, string>(source, v => v.ToString());

        derived.Update("override");

        derived.Value.Should().Be("override");
    }

    [Fact]
    public static void Update_DerivedValueWithFunc_Works()
    {
        var source = new State<int>(5);
        var derived = new DerivedState<int, string>(source, v => v.ToString());

        derived.Update(current => current + "!");

        derived.Value.Should().Be("5!");
    }

    [Fact]
    public static void SourceSameValue_DoesNotNotifyDerived()
    {
        var source = new State<int>(5);
        var derived = new DerivedState<int, int>(source, v => v * 2);
        var notifyCount = 0;
        derived.OnChange += _ => notifyCount++;

        source.Update(5); // Same value

        notifyCount.Should().Be(0);
    }

    [Fact]
    public static void Value_ReturnsLatestAfterMultipleSourceUpdates()
    {
        var source = new State<int>(0);
        var derived = new DerivedState<int, int>(source, v => v * 10);

        source.Update(1);
        source.Update(2);
        source.Update(3);

        derived.Value.Should().Be(30);
    }
}
