using System.Collections.Concurrent;

namespace XForge.State;

/// <summary>
/// Thread-safe centralized store for managing multiple named state instances.
/// Uses <see cref="ConcurrentDictionary{TKey,TValue}"/> for thread-safe operations.
/// </summary>
public sealed class StateStore : IStateStore
{
    private readonly ConcurrentDictionary<string, object> _states = new(StringComparer.Ordinal);

    /// <inheritdoc />
    public IState<T> GetOrCreate<T>(string key, T initialValue)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        object state = _states.GetOrAdd(key, _ => new State<T>(initialValue));
        return (IState<T>)state;
    }

    /// <inheritdoc />
    public IState<T>? Find<T>(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        return _states.TryGetValue(key, out object? state) ? (IState<T>)state : null;
    }

    /// <inheritdoc />
    public bool Remove(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        return _states.TryRemove(key, out _);
    }

    /// <inheritdoc />
    public void Clear()
    {
        _states.Clear();
    }
}
