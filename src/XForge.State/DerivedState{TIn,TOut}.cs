namespace XForge.State;

/// <summary>
/// A derived state that automatically recalculates when its source state changes.
/// Subscribes to the source state and applies a transformation function to produce the derived value.
/// </summary>
/// <typeparam name="TIn">The type of the source state value.</typeparam>
/// <typeparam name="TOut">The type of the derived state value.</typeparam>
public sealed class DerivedState<TIn, TOut> : IDerivedState<TOut>
{
    private readonly IState<TIn> _source;
    private readonly Func<TIn, TOut> _transform;
    private readonly State<TOut> _inner;
    private IDisposable? _sourceSubscription;

    /// <summary>
    /// Creates a new derived state from a source state and a transformation function.
    /// Automatically subscribes to the source state.
    /// </summary>
    /// <param name="source">The source state to derive from.</param>
    /// <param name="transform">The transformation function applied to the source value.</param>
    public DerivedState(IState<TIn> source, Func<TIn, TOut> transform)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(transform);

        _source = source;
        _transform = transform;
        _inner = new State<TOut>(transform(source.Value));

        // Subscribe to source changes — recalculate derived value
        _sourceSubscription = source.Subscribe(sourceValue =>
        {
            TOut derivedValue = _transform(sourceValue);
            _inner.Update(derivedValue);
        });
    }

    /// <inheritdoc />
    public TOut Value => _inner.Value;

    /// <inheritdoc />
    public event Action<TOut>? OnChange
    {
        add => _inner.OnChange += value;
        remove => _inner.OnChange -= value;
    }

    /// <inheritdoc />
    public void Update(TOut newValue)
    {
        _inner.Update(newValue);
    }

    /// <inheritdoc />
    public void Update(Func<TOut, TOut> updater)
    {
        _inner.Update(updater);
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<TOut> handler)
    {
        return _inner.Subscribe(handler);
    }

    /// <inheritdoc />
    public void Recalculate()
    {
        TOut derivedValue = _transform(_source.Value);
        _inner.Update(derivedValue);
    }

    /// <summary>
    /// Disposes the subscription to the source state.
    /// </summary>
    public void Dispose()
    {
        _sourceSubscription?.Dispose();
        _sourceSubscription = null;
    }
}
