namespace XForge.State;

/// <summary>
/// Thread-safe reactive state container that notifies subscribers when the value changes.
/// Uses <see cref="EqualityComparer{T}.Default"/> to avoid unnecessary notifications (D5 decision).
/// </summary>
/// <typeparam name="T">The type of the state value.</typeparam>
public sealed class State<T> : IState<T>
{
#if NET9_0_OR_GREATER
    private readonly Lock _lock = new();
#else
    private readonly object _lock = new();
#endif
    private readonly List<Action<T>> _subscribers = [];
    private T _value;

    /// <summary>
    /// Creates a new <see cref="State{T}"/> with the specified initial value.
    /// </summary>
    /// <param name="initialValue">The initial state value.</param>
    public State(T initialValue)
    {
        _value = initialValue;
    }

    /// <inheritdoc />
    public T Value
    {
        get
        {
            lock (_lock)
            {
                return _value;
            }
        }
    }

    /// <inheritdoc />
    public event Action<T>? OnChange
    {
        add
        {
            lock (_lock)
            {
                _subscribers.Add(value!);
            }
        }
        remove
        {
            lock (_lock)
            {
                _subscribers.Remove(value!);
            }
        }
    }

    /// <inheritdoc />
    public void Update(T newValue)
    {
        lock (_lock)
        {
            if (EqualityComparer<T>.Default.Equals(_value, newValue))
            {
                return;
            }

            _value = newValue;
            NotifySubscribers();
        }
    }

    /// <inheritdoc />
    public void Update(Func<T, T> updater)
    {
        ArgumentNullException.ThrowIfNull(updater);

        lock (_lock)
        {
            T newValue = updater(_value);
            if (EqualityComparer<T>.Default.Equals(_value, newValue))
            {
                return;
            }

            _value = newValue;
            NotifySubscribers();
        }
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<T> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        lock (_lock)
        {
            _subscribers.Add(handler);
        }

        // Invoke immediately with current value
        handler(Value);

        return new Subscription(this, handler);
    }

    private void Unsubscribe(Action<T> handler)
    {
        lock (_lock)
        {
            _subscribers.Remove(handler);
        }
    }

    private void NotifySubscribers()
    {
        // Called within lock — snapshot and invoke
        Action<T>[] snapshot = [.. _subscribers];
        foreach (Action<T> subscriber in snapshot)
        {
            subscriber(_value);
        }
    }

    /// <summary>
    /// Represents an active subscription that unsubscribes when disposed.
    /// </summary>
    private sealed class Subscription(State<T> state, Action<T> handler) : IDisposable
    {
        private volatile bool _disposed;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            state.Unsubscribe(handler);
        }
    }
}
