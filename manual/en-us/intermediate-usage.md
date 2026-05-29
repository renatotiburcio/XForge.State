# XForge.State — Manual Enterprise

> ✅ **Estável:** v0.4.0

---

## 10 — Uso Intermediário

### DerivedState com múltiplas fontes

`csharp
var price = new State<decimal>(100m);
var quantity = new State<int>(2);

// DerivedState de uma fonte, mas lendo outra
var total = new DerivedState<decimal, decimal>(
    price,
    p => p * quantity.Value);

// Atualizar preço recalcula total
price.Update(150m);
Console.WriteLine(total.Value); // 300
`

### State com objetos complexos

`csharp
public record AppState(string Theme, string Language, bool IsAuthenticated);

var appState = new State<AppState>(new AppState("light", "pt-BR", false));

appState.Update(prev => prev with { IsAuthenticated = true });
`

### Subscription management

`csharp
var subscriptions = new CompositeDisposable();

subscriptions.Add(counter.Subscribe(v => Console.WriteLine($"Counter: {v}")));
subscriptions.Add(name.Subscribe(v => Console.WriteLine($"Name: {v}")));

// Dispose todas de uma vez
subscriptions.Dispose();
`

### ReactiveState\<T\>

`csharp
var state = new ReactiveState<int>(0);
state.Value = 5; // Notifica subscribers
`

---

**Navegação:** ← [Uso Básico](./basic-usage.md) | → [Uso Avançado](./advanced-usage.md)
