# XForge.State — Manual Enterprise

> ✅ **Estável:** v0.4.0

---

## 09 — Uso Básico

### IState\<T\> — Estado reativo

`csharp
var counter = new State<int>(0);

// Ler valor
Console.WriteLine(counter.Value); // 0

// Atualizar com valor direto
counter.Update(5);

// Atualizar com função
counter.Update(prev => prev + 1);

// Subscribe (recebe valor atual imediatamente)
var subscription = counter.Subscribe(value =>
{
    Console.WriteLine($"Valor mudou: {value}");
});

// Dispose para unsubscribe
subscription.Dispose();
`

### OnChange event

`csharp
counter.OnChange += value =>
{
    Console.WriteLine($"Novo valor: {value}");
};
`

### IDerivedState\<T\> — Estado computado

`csharp
var items = new State<List<string>>(new List<string { "a", "b" });

var count = new DerivedState<List<string>, int>(
    items,
    list => list.Count);

Console.WriteLine(count.Value); // 2
items.Update(prev => { prev.Add("c"); return prev; });
Console.WriteLine(count.Value); // 3
`

### IStateStore — Store centralizado

`csharp
var store = new StateStore();

// GetOrCreate — cria se não existe
var theme = store.GetOrCreate("theme", "light");

// Find — retorna null se não existe
var found = store.Find<string>("theme");

// Remove
store.Remove("theme");

// Clear
store.Clear();
`

---

**Navegação:** ← [Arquitetura](./architecture.md) | → [Uso Intermediário](./intermediate-usage.md)
