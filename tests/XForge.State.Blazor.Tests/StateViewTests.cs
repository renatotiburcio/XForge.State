using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using XForge.State.Blazor;

namespace XForge.State.Blazor.Tests;

/// <summary>
/// Tests for the <see cref="StateView{T}"/> Blazor component.
/// </summary>
public class StateViewTests : Bunit.TestContext
{
    [Fact]
    public void StateView_Renders_InitialValue()
    {
        // Arrange
        State<string> state = new("hello");

        // Act
        IRenderedComponent<StateView<string>> cut = RenderComponent<StateView<string>>(parameters => parameters
            .Add(p => p.State, state)
            .Add(p => p.ChildContent, (RenderFragment<string>)(value => builder =>
            {
                builder.AddContent(0, value);
            }))
        );

        // Assert
        cut.MarkupMatches("hello");
    }

    [Fact]
    public void StateView_Renders_IntValue()
    {
        // Arrange
        State<int> state = new(42);

        // Act
        IRenderedComponent<StateView<int>> cut = RenderComponent<StateView<int>>(parameters => parameters
            .Add(p => p.State, state)
            .Add(p => p.ChildContent, (RenderFragment<int>)(value => builder =>
            {
                builder.AddContent(0, value.ToString());
            }))
        );

        // Assert
        cut.MarkupMatches("42");
    }

    [Fact]
    public void StateView_Rerenders_OnStateChange()
    {
        // Arrange
        State<string> state = new("hello");
        IRenderedComponent<StateView<string>> cut = RenderComponent<StateView<string>>(parameters => parameters
            .Add(p => p.State, state)
            .Add(p => p.ChildContent, (RenderFragment<string>)(value => builder =>
            {
                builder.AddContent(0, value);
            }))
        );
        cut.MarkupMatches("hello");

        // Act
        state.Update("world");

        // Assert
        cut.MarkupMatches("world");
    }

    [Fact]
    public void StateView_Rerenders_MultipleTimes()
    {
        // Arrange
        State<string> state = new("v1");
        IRenderedComponent<StateView<string>> cut = RenderComponent<StateView<string>>(parameters => parameters
            .Add(p => p.State, state)
            .Add(p => p.ChildContent, (RenderFragment<string>)(value => builder =>
            {
                builder.AddContent(0, value);
            }))
        );

        // Act & Assert — multiple updates
        state.Update("v2");
        cut.MarkupMatches("v2");

        state.Update("v3");
        cut.MarkupMatches("v3");
    }

    [Fact]
    public void StateView_DoesNotRerender_WhenValueUnchanged()
    {
        // Arrange
        State<string> state = new("hello");
        int renderCount = 0;
        IRenderedComponent<StateView<string>> cut = RenderComponent<StateView<string>>(parameters => parameters
            .Add(p => p.State, state)
            .Add(p => p.ChildContent, (RenderFragment<string>)(value => builder =>
            {
                renderCount++;
                builder.AddContent(0, value);
            }))
        );
        int initialRenderCount = renderCount;

        // Act — update with same value (equality check D5)
        state.Update("hello");

        // Assert — should not re-render
        renderCount.Should().Be(initialRenderCount);
    }

    [Fact]
    public void StateView_Dispose_Unsubscribes()
    {
        // Arrange
        State<string> state = new("hello");
        IRenderedComponent<StateView<string>> cut = RenderComponent<StateView<string>>(parameters => parameters
            .Add(p => p.State, state)
            .Add(p => p.ChildContent, (RenderFragment<string>)(value => builder =>
            {
                builder.AddContent(0, value);
            }))
        );

        // Act
        cut.Dispose();
        state.Update("world"); // Should not throw

        // Assert — no exception
        state.Value.Should().Be("world");
    }

    [Fact]
    public void StateView_WorksWith_DerivedState()
    {
        // Arrange
        State<int> source = new(10);
        DerivedState<int, string> derived = new(source, v => v.ToString(System.Globalization.CultureInfo.InvariantCulture));
        IRenderedComponent<StateView<string>> cut = RenderComponent<StateView<string>>(parameters => parameters
            .Add(p => p.State, derived)
            .Add(p => p.ChildContent, (RenderFragment<string>)(value => builder =>
            {
                builder.AddContent(0, value);
            }))
        );
        cut.MarkupMatches("10");

        // Act
        source.Update(1234);

        // Assert
        cut.MarkupMatches("1234");
    }

    [Fact]
    public void StateView_WithComplexType_RendersCorrectly()
    {
        // Arrange
        State<(string Name, int Age)> state = new(("Alice", 30));
        IRenderedComponent<StateView<(string Name, int Age)>> cut =
            RenderComponent<StateView<(string Name, int Age)>>(parameters => parameters
                .Add(p => p.State, state)
                .Add(p => p.ChildContent, (RenderFragment<(string Name, int Age)>)(value => builder =>
                {
                    builder.AddContent(0, $"{value.Name}:{value.Age}");
                }))
            );
        cut.MarkupMatches("Alice:30");

        // Act
        state.Update(("Bob", 25));

        // Assert
        cut.MarkupMatches("Bob:25");
    }

    [Fact]
    public void StateView_WithEmptyContent_RendersEmpty()
    {
        // Arrange
        State<string> state = new("hello");

        // Act — no ChildContent
        IRenderedComponent<StateView<string>> cut = RenderComponent<StateView<string>>(parameters => parameters
            .Add(p => p.State, state)
        );

        // Assert
        cut.MarkupMatches("");
    }
}
