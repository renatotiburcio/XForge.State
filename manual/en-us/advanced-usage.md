> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 11 — Uso Avançado

## Estado persistente

`csharp
public class PersistedState<T> : State<T>
{
    private readonly string _key;
    private readonly ILocalStorage _storage;

    public PersistedState(string key, T initialValue, ILocalStorage storage)
        : base(initialValue)
    {
        _key = key;
        _storage = storage;

        // Carregar valor persisted
        var saved = _storage.GetItem<T>(key);
        if (saved is not null) Update(saved);

        // Persistir em cada mudança
        Subscribe(value => _storage.SetItem(_key, value));
    }
}
`

## State com debounce

`csharp
public class DebouncedState<T> : State<T>
{
    private readonly TimeSpan _delay;
    private CancellationTokenSource? _cts;

    public DebouncedState(T initialValue, TimeSpan delay) : base(initialValue)
    {
        _delay = delay;
    }

    public new void Update(T newValue)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        Task.Delay(_delay, token).ContinueWith(_ =>
        {
            if (!token.IsCancellationRequested)
                base.Update(newValue);
        });
    }
}
`

---

**Navegação:** ← [Uso Intermediário](./intermediate-usage.md) | → [Boas Práticas Enterprise](./enterprise-best-practices.md)
