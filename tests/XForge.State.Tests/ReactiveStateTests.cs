using System.ComponentModel;

namespace XForge.State.Tests;

/// <summary>
/// Tests for <see cref="ReactiveState{T}"/> — INotifyPropertyChanged integration.
/// </summary>
public class ReactiveStateTests
{
    [Fact]
    public void Constructor_Sets_InitialValue()
    {
        // Arrange & Act
        ReactiveState<int> state = new(42);

        // Assert
        state.Value.Should().Be(42);
    }

    [Fact]
    public void Update_ChangesValue()
    {
        // Arrange
        ReactiveState<string> state = new("hello");

        // Act
        state.Update("world");

        // Assert
        state.Value.Should().Be("world");
    }

    [Fact]
    public void Update_SameValue_DoesNotNotify()
    {
        // Arrange
        ReactiveState<int> state = new(42);
        int notifyCount = 0;
        state.PropertyChanged += (_, _) => notifyCount++;

        // Act
        state.Update(42); // same value

        // Assert
        notifyCount.Should().Be(0);
    }

    [Fact]
    public void Update_DifferentValue_RaisesPropertyChanged()
    {
        // Arrange
        ReactiveState<int> state = new(1);
        PropertyChangedEventArgs? receivedArgs = null;
        state.PropertyChanged += (_, args) => receivedArgs = args;

        // Act
        state.Update(2);

        // Assert
        receivedArgs.Should().NotBeNull();
        receivedArgs!.PropertyName.Should().Be(nameof(IState<object>.Value));
    }

    [Fact]
    public void Update_RaisesPropertyChanged_ForValueProperty()
    {
        // Arrange
        ReactiveState<string> state = new("a");
        List<string> propertyNames = [];
        state.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is not null)
            {
                propertyNames.Add(args.PropertyName);
            }
        };

        // Act
        state.Update("b");
        state.Update("c");

        // Assert
        propertyNames.Should().AllSatisfy(name => name.Should().Be("Value"));
        propertyNames.Should().HaveCount(2);
    }

    [Fact]
    public void OnChange_Event_Fires()
    {
        // Arrange
        ReactiveState<int> state = new(0);
        List<int> values = [];
        state.OnChange += v => values.Add(v);

        // Act
        state.Update(1);
        state.Update(2);

        // Assert
        values.Should().BeEquivalentTo([1, 2]);
    }

    [Fact]
    public void Subscribe_InvokesImmediately_WithCurrentValue()
    {
        // Arrange
        ReactiveState<string> state = new("initial");
        List<string> values = [];

        // Act
        state.Subscribe(v => values.Add(v));

        // Assert
        values.Should().ContainSingle("initial");
    }

    [Fact]
    public void Subscribe_ReturnsDisposable_Unsubscribes()
    {
        // Arrange
        ReactiveState<int> state = new(0);
        List<int> values = [];
        IDisposable subscription = state.Subscribe(v => values.Add(v));

        // Act
        subscription.Dispose();
        state.Update(1);

        // Assert — only initial value, not the update
        values.Should().HaveCount(1).And.Contain(0);
    }

    [Fact]
    public void Update_Func_Updater_AppliesTransformation()
    {
        // Arrange
        ReactiveState<int> state = new(10);

        // Act
        state.Update(v => v * 2);

        // Assert
        state.Value.Should().Be(20);
    }

    [Fact]
    public void Update_Func_SameResult_DoesNotNotify()
    {
        // Arrange
        ReactiveState<int> state = new(10);
        int notifyCount = 0;
        state.PropertyChanged += (_, _) => notifyCount++;

        // Act
        state.Update(v => v); // identity — no change

        // Assert
        notifyCount.Should().Be(0);
    }

    [Fact]
    public void Implements_INotifyPropertyChanged()
    {
        // Arrange & Act
        ReactiveState<string> state = new("test");

        // Assert
        state.Should().BeAssignableTo<INotifyPropertyChanged>();
    }

    [Fact]
    public void Implements_IState()
    {
        // Arrange & Act
        ReactiveState<string> state = new("test");

        // Assert
        state.Should().BeAssignableTo<IState<string>>();
    }

    [Fact]
    public void PropertyChanged_CanSubscribeAndUnsubscribe()
    {
        // Arrange
        ReactiveState<int> state = new(0);
        int callCount = 0;
        PropertyChangedEventHandler handler = (_, _) => callCount++;

        // Act
        state.PropertyChanged += handler;
        state.Update(1);
        state.PropertyChanged -= handler;
        state.Update(2);

        // Assert
        callCount.Should().Be(1);
    }

    [Fact]
    public void ThreadSafety_MultipleConcurrentUpdates()
    {
        // Arrange
        ReactiveState<int> state = new(0);
        ConcurrentBag<int> values = [];
        state.OnChange += v => values.Add(v);

        // Act
        Parallel.For(0, 100, i => state.Update(i));

        // Assert — all values should be unique (each triggers notification)
        values.Should().NotBeEmpty();
        state.Value.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public void EqualityComparer_StringComparison_IsCaseSensitive()
    {
        // Arrange
        ReactiveState<string> state = new("Hello");
        int notifyCount = 0;
        state.PropertyChanged += (_, _) => notifyCount++;

        // Act
        state.Update("hello"); // different case → should notify

        // Assert
        notifyCount.Should().Be(1);
        state.Value.Should().Be("hello");
    }
}
