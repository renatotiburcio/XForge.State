> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 27 — Integração Blazor

## Setup

``csharp
// Program.cs
builder.Services.AddXForgeStateBlazor();
``n
## Componente com estado

``razor
@inject IStateStore StateStore
@implements IDisposable

<p>Contador: @_counter.Value</p>
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
## Estado compartilhado entre componentes

``razor
@* ComponentA.razor *@
@inject IStateStore StateStore

<button @onclick="ChangeTheme">Toggle Theme</button>

@code {
    private void ChangeTheme()
    {
        var theme = StateStore.GetOrCreate("theme", "light");
        theme.Update(current => current == "light" ? "dark" : "light");
    }
}
``n
``razor
@* ComponentB.razor *@
@inject IStateStore StateStore
@implements IDisposable

<div class="@_theme.Value">Conteúdo</div>

@code {
    private IState<string> _theme = default!;
    private IDisposable? _subscription;

    protected override void OnInitialized()
    {
        _theme = StateStore.GetOrCreate("theme", "light");
        _subscription = _theme.Subscribe(_ => InvokeAsync(StateHasChanged));
    }

    public void Dispose() => _subscription?.Dispose();
}
``n
## DerivedState em Blazor

``razor
@inject IStateStore StateStore
@implements IDisposable

<p>Total: @_total.Value</p>

@code {
    private DerivedState<decimal, decimal> _total = default!;
    private IDisposable? _subscription;

    protected override void OnInitialized()
    {
        var price = StateStore.GetOrCreate("price", 100m);
        var qty = StateStore.GetOrCreate("quantity", 1);

        _total = new DerivedState<decimal, decimal>(price, p => p * qty.Value);
        _subscription = _total.Subscribe(_ => InvokeAsync(StateHasChanged));
    }

    public void Dispose()
    {
        _subscription?.Dispose();
        _total.Dispose();
    }
}
``n
---

**Navegação:** ← [Extensibilidade](./extensibility.md) | → [Compatibilidade Multi-TFM](./multi-tfm-compatibility.md)
