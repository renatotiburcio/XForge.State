# 22 — Guia de Migração

## De eventos manuais para XForge.State

``csharp
// Antes
public class CounterService
{
    public int Value { get; private set; }
    public event Action<int>? Changed;
    public void Increment()
    {
        Value++;
        Changed?.Invoke(Value);
    }
}

// Depois
var counter = new State<int>(0);
counter.Subscribe(v => Console.WriteLine(v));
counter.Update(prev => prev + 1);
``n
---

**Navegação:** ← [Comparação](./package-comparison.md) | → [Contribuindo](./contributing.md)
