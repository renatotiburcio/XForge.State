# XForge.State — Manual Enterprise

> ✅ **Estável:** v0.4.0

---

## 08 — Arquitetura

`
┌──────────────────────────────────────┐
│         Consumidor (Aplicação)       │
└──────────────┬───────────────────────┘
               │
               ▼
┌──────────────────────────────────────┐
│           XForge.State               │
│  ┌────────────────────────────────┐  │
│  │ IState<T>       │ State<T>     │  │
│  │ IDerivedState<T>│ DerivedState │  │
│  │ IStateStore     │ StateStore   │  │
│  │ ReactiveState<T>│ Extensions   │  │
│  └────────────────────────────────┘  │
└──────────────────────────────────────┘
`

### Thread-safety

State<T> usa lock para todas as operações:
- Value — Leitura protegida
- Update — Escrita protegida + notificação
- Subscribe — Adição de subscriber protegida

### Equality check

State<T> usa EqualityComparer<T>.Default para evitar notificações desnecessárias quando o valor não muda.

---

**Navegação:** ← [Configuração](./configuration.md) | → [Uso Básico](./basic-usage.md)
