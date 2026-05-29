namespace XForge.State;

/// <summary>
/// A centralized store for managing multiple named state instances.
/// Provides a registry pattern for state management across an application.
/// </summary>
public interface IStateStore
{
    /// <summary>
    /// Gets an existing state by key, or creates a new one with the specified initial value.
    /// </summary>
    /// <typeparam name="T">The type of the state value.</typeparam>
    /// <param name="key">The unique key identifying the state.</param>
    /// <param name="initialValue">The initial value for a newly created state.</param>
    /// <returns>The existing or newly created state.</returns>
    IState<T> GetOrCreate<T>(string key, T initialValue);

    /// <summary>
    /// Gets an existing state by key, or null if no state with that key exists.
    /// </summary>
    /// <typeparam name="T">The type of the state value.</typeparam>
    /// <param name="key">The unique key identifying the state.</param>
    /// <returns>The state if found; otherwise, null.</returns>
    IState<T>? Find<T>(string key);

    /// <summary>
    /// Removes a state by key.
    /// </summary>
    /// <param name="key">The unique key identifying the state to remove.</param>
    /// <returns>True if the state was found and removed; otherwise, false.</returns>
    bool Remove(string key);

    /// <summary>
    /// Removes all states from the store.
    /// </summary>
    void Clear();
}
