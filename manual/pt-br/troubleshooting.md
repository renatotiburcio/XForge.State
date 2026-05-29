> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 16 — Troubleshooting

## Componente não re-renderiza

**Causa:** Subscription não foi criada ou StateHasChanged não é chamado.

**Solução:** Use Subscribe(_ => InvokeAsync(StateHasChanged)) no OnInitialized.

## Memory leak

**Causa:** Subscription não foi disposed.

**Solução:** Implemente IDisposable e dispose a subscription.

## Stack overflow com DerivedState

**Causa:** DerivedState circular (A deriva de B, B deriva de A).

**Solução:** Evite ciclos de dependência.

---

**Navegação:** ← [Performance](./performance.md) | → [FAQ](./faq.md)
