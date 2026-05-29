> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 12 — Boas Práticas Enterprise

## Use IState<T> como dependência, não como campo estático

``csharp
// ✅ Bom — via DI
public class MyComponent : ComponentBase
{
    [Inject] private IStateStore StateStore { get; set; } = default!;
}

// ❌ Ruim — estado global estático
public static class Globals
{
    public static State<int> Counter = new(0);
}
``n
## Dispose subscriptions

``csharp
// Sempre dispose para evitar memory leaks
var subscription = state.Subscribe(handler);
// ... usar ...
subscription.Dispose();
``n
---

**Navegação:** ← [Uso Avançado](./advanced-usage.md) | → [Exemplos de Integração](./integration-examples.md)
