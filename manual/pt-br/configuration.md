> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 07 — Configuração

## Registro via DI

`csharp
builder.Services.AddXForgeState();
`

Para Blazor:
`csharp
builder.Services.AddXForgeStateBlazor();
`

O que é registrado:

| Serviço | Lifetime | Descrição |
|---------|----------|-----------|
| IStateStore | Singleton | Store centralizado |

## Criando estados

`csharp
// Via DI (usando StateStore)
var counter = stateStore.GetOrCreate("counter", 0);

// Diretamente (sem DI)
var counter = new State<int>(0);
`

---

**Navegação:** ← [Quick Start](quick-start.md) | → [Arquitetura](architecture.md)
