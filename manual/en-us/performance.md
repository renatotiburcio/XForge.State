> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 15 — Performance

- State<T> usa lock para thread-safety — overhead mínimo
- EqualityComparer<T>.Default evita notificações desnecessárias
- Snapshot de subscribers antes de notificar (evita problemas de concorrência)
- DerivedState recalcula apenas quando fonte muda

---

**Navegação:** ← [Testing](./testing.md) | → [Troubleshooting](./troubleshooting.md)
