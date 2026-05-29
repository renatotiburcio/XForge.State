namespace XForge.State;

/// <summary>
/// Represents a derived state that automatically recalculates when its source state changes.
/// </summary>
/// <typeparam name="T">The type of the derived state value.</typeparam>
public interface IDerivedState<T> : IState<T>
{
    /// <summary>
    /// Forces a recalculation of the derived state from the current source value.
    /// </summary>
    void Recalculate();
}
