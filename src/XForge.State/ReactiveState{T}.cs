using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XForge.State;

/// <summary>
/// A reactive state container that implements both <see cref="IState{T}"/> and
/// <see cref="INotifyPropertyChanged"/> for use with WPF, MAUI, and other
/// data-binding frameworks.
/// </summary>
/// <typeparam name="T">The type of the state value.</typeparam>
public sealed class ReactiveState<T> : IState<T>, INotifyPropertyChanged
{
    private readonly State<T> _inner;

    /// <summary>
    /// Creates a new <see cref="ReactiveState{T}"/> with the specified initial value.
    /// </summary>
    /// <param name="initialValue">The initial state value.</param>
    public ReactiveState(T initialValue)
    {
        _inner = new State<T>(initialValue);
        _inner.OnChange += NotifyPropertyChanged;
    }

    /// <inheritdoc />
    public T Value => _inner.Value;

    /// <inheritdoc />
    public event Action<T>? OnChange
    {
        add => _inner.OnChange += value;
        remove => _inner.OnChange -= value;
    }

    /// <summary>
    /// Raised when the <see cref="Value"/> property changes.
    /// Compatible with WPF, MAUI, and other INotifyPropertyChanged consumers.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    public void Update(T newValue) => _inner.Update(newValue);

    /// <inheritdoc />
    public void Update(Func<T, T> updater) => _inner.Update(updater);

    /// <inheritdoc />
    public IDisposable Subscribe(Action<T> handler) => _inner.Subscribe(handler);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void NotifyPropertyChanged(T _) =>
        PropertyChanged?.Invoke(this, PropertyChangedEventArgsCache.Instance.ValueChanged);
}

/// <summary>
/// Caches <see cref="PropertyChangedEventArgs"/> to avoid allocations on hot paths.
/// </summary>
internal static class PropertyChangedEventArgsCache
{
    internal static readonly PropertyChangedEventArgsCacheHolder Instance = new();

    internal sealed class PropertyChangedEventArgsCacheHolder
    {
        internal readonly PropertyChangedEventArgs ValueChanged = new(nameof(IState<object>.Value));
    }
}
