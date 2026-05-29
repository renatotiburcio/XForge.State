> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 14 — Testing

## Testando State<T>

``csharp
[Fact]
public void Update_ShouldNotifySubscriber()
{
    var state = new State<int>(0);
    int notified = -1;
    state.Subscribe(v => notified = v);

    state.Update(42);

    Assert.Equal(42, notified);
}

[Fact]
public void Update_ShouldNotNotifyWhenValueUnchanged()
{
    var state = new State<int>(5);
    int callCount = 0;
    state.Subscribe(_ => callCount++);

    state.Update(5); // Same value

    Assert.Equal(1, callCount); // Only initial notification
}
``n
## Testando DerivedState

``csharp
[Fact]
public void DerivedState_ShouldRecalculateOnSourceChange()
{
    var source = new State<int>(10);
    var derived = new DerivedState<int, int>(source, x => x * 2);

    Assert.Equal(20, derived.Value);

    source.Update(15);
    Assert.Equal(30, derived.Value);
}
``n
---

**Navegação:** ← [Exemplos de Integração](./integration-examples.md) | → [Performance](./performance.md)
