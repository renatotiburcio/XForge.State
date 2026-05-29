# 20 — Referência da API

## IState<T>

| Membro | Tipo | Descrição |
|--------|------|-----------|
| Value | T | Valor atual (read-only) |
| OnChange | event Action<T> | Evento de mudança |
| Update(T) | oid | Atualiza com valor direto |
| Update(Func<T,T>) | oid | Atualiza com função |
| Subscribe(Action<T>) | IDisposable | Inscreve (invoca imediatamente) |

## IDerivedState<T>

| Membro | Tipo | Descrição |
|--------|------|-----------|
| Recalculate() | oid | Força recálculo |

(herda IState<T>)

## IStateStore

| Membro | Tipo | Descrição |
|--------|------|-----------|
| GetOrCreate<T>(key, initial) | IState<T> | Get ou create |
| Find<T>(key) | IState<T>? | Find ou null |
| Remove(key) | ool | Remove state |
| Clear() | oid | Remove todos |

---

**Navegação:** ← [Changelog](./changelog.md) | → [Comparação](./package-comparison.md)
