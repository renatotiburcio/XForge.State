namespace XForge.State;

/// <summary>
/// Represents a reactive state container that notifies subscribers when the value changes.
/// </summary>
/// <typeparam name="T">The type of the state value.</typeparam>
public interface IState<T>
{
    /// <summary>
    /// Gets the current state value.
    /// </summary>
    T Value { get; }

    /// <summary>
    /// Event raised when the state value changes.
    /// </summary>
    event Action<T>? OnChange;

    /// <summary>
    /// Updates the state with a new value. Notifies subscribers only if the value has changed.
    /// </summary>
    /// <param name="newValue">The new state value.</param>
    void Update(T newValue);

    /// <summary>
    /// Updates the state using a transformation function. Notifies subscribers only if the value has changed.
    /// </summary>
    /// <param name="updater">A function that receives the current value and returns the new value.</param>
    void Update(Func<T, T> updater);

    /// <summary>
    /// Subscribes a handler to state changes. The handler is immediately invoked with the current value.
    /// Returns an IDisposable that unsubscribes when disposed.
    /// </summary>
    /// <param name="handler">The handler to invoke on state changes.</param>
    /// <returns>An IDisposable that unsubscribes the handler when disposed.</returns>
    IDisposable Subscribe(Action<T> handler);
}
