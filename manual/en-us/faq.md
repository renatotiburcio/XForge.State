> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 17 — FAQ

### 1. State<T> é thread-safe?

**Sim.** Todas as operações são protegidas por lock.

### 2. Posso usar fora de Blazor?

**Sim.** XForge.State é independente de Blazor. Use em console apps, APIs, workers.

### 3. Como evito memory leaks?

Sempre dispose subscriptions. Implemente IDisposable em componentes Blazor.

### 4. Qual a diferença para Redux/Flux?

XForge.State é mais simples — sem reducers, actions, middleware. Ideal para estados locais e compartilhados simples.

---

**Navegação:** ← [Troubleshooting](./troubleshooting.md) | → [Roadmap](./roadmap.md)
