> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 13 — Exemplos de Integração

## Blazor Server

``csharp
@inject IStateStore StateStore

<h1>Contador: @_counter.Value</h1>
<button @onclick="Increment">+1</button>

@code {
    private IState<int> _counter = default!;
    private IDisposable? _subscription;

    protected override void OnInitialized()
    {
        _counter = StateStore.GetOrCreate("counter", 0);
        _subscription = _counter.Subscribe(_ => InvokeAsync(StateHasChanged));
    }

    private void Increment() => _counter.Update(prev => prev + 1);

    public void Dispose() => _subscription?.Dispose();
}
``n
## Blazor WASM

``csharp
// Program.cs
builder.Services.AddXForgeStateBlazor();
``n
## Console App

``csharp
var store = new StateStore();
var counter = store.GetOrCreate("counter", 0);
counter.Subscribe(v => Console.WriteLine($"Count: {v}"));
``n
---

**Navegação:** ← [Boas Práticas Enterprise](./enterprise-best-practices.md) | → [Testing](./testing.md)
