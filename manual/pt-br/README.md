# XForge.State — Manual Oficial

<p align=""center"">
  <img src=""./icon.png"" alt=""XForge.State"" width=""128"" height=""128"" />
</p>

<p align=""center"">
  <strong>Gerenciamento de estado reativo para .NET com suporte a Blazor</strong>
</p>

<p align=""center"">
  <img src=""https://img.shields.io/nuget/v/XForge.State.svg"" alt=""NuGet"" />
  <img src=""https://img.shields.io/badge/version-0.4.0-blue"" alt=""Version"" />
  <img src=""https://img.shields.io/badge/status-Published-green"" alt=""Status"" />
  <img src=""https://img.shields.io/badge/license-MIT-blue"" alt=""License"" />
  <img src=""https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-purple"" alt="".NET"" />
</p>

<p align=""center"">
  <a href=""https://www.nuget.org/packages/XForge.State/"">NuGet</a> ·
  <a href=""https://github.com/renatotiburcio/XForge.State"">GitHub</a>
</p>

---

> ✅ **Release Estável:** v0.4.0 — APIs seguem Semantic Versioning.

---

## Sumário

| # | Capítulo | Arquivo |
|---|----------|---------|
| 01–04 | Capa, Introdução, Status, Features | [README.md](README.md) |
| 05 | Instalação | [installation.md](installation.md) |
| 06 | Quick Start | [quick-start.md](quick-start.md) |
| 07 | Configuração | [configuration.md](configuration.md) |
| 08 | Arquitetura | [architecture.md](architecture.md) |
| 09 | Uso Básico | [basic-usage.md](basic-usage.md) |
| 10 | Uso Intermediário | [intermediate-usage.md](intermediate-usage.md) |
| 11 | Uso Avançado | [advanced-usage.md](advanced-usage.md) |
| 12 | Boas Práticas Enterprise | [enterprise-best-practices.md](enterprise-best-practices.md) |
| 13 | Exemplos de Integração | [integration-examples.md](integration-examples.md) |
| 14 | Testing | [testing.md](testing.md) |
| 15 | Performance | [performance.md](performance.md) |
| 16 | Troubleshooting | [troubleshooting.md](troubleshooting.md) |
| 17 | FAQ | [faq.md](faq.md) |
| 18 | Roadmap | [roadmap.md](roadmap.md) |
| 19 | Changelog | [changelog.md](changelog.md) |
| 20 | Referência da API | [api-reference.md](api-reference.md) |
| 21 | Comparação | [package-comparison.md](package-comparison.md) |
| 22 | Guia de Migração | [migration-guide.md](migration-guide.md) |
| 23 | Contribuindo | [contributing.md](contributing.md) |
| 24 | Licença | [license.md](license.md) |
| 25 | Notas Finais | [final-notes.md](final-notes.md) |
| 26 | Extensibilidade | [extensibility.md](extensibility.md) |
| 27 | Integração Blazor | [blazor-integration.md](blazor-integration.md) |
| 28 | Compatibilidade Multi-TFM | [multi-tfm-compatibility.md](multi-tfm-compatibility.md) |
| 29 | Segurança | [security.md](security.md) |
| 30 | Suporte & Comunidade | [support-community.md](support-community.md) |

---

## 02 — Introdução

### O que é

XForge.State é uma biblioteca .NET para gerenciamento de estado reativo. Ela fornece IState<T> com notificações de mudança, DerivedState para estado computado e StateStore para gerenciamento centralizado.

`csharp
var counter = new State<int>(0);
counter.Subscribe(value => Console.WriteLine($"Contador: {value}"));
counter.Update(1); // Notifica: "Contador: 1"
counter.Update(prev => prev + 1); // Notifica: "Contador: 2"
`

### Por que existe

- **Blazor StateHasChanged** — Re-renderização manual é verbosa e propensa a erros.
- **Flux/Redux .NET** — Complexidade excessiva para estados simples.
- **Implementações manuais** — Event-based state sem thread-safety.

### APIs principais

| Interface | Descrição |
|-----------|-----------|
| IState<T> | Container de estado reativo com Subscribe |
| IDerivedState<T> | Estado derivado que recalcula automaticamente |
| IStateStore | Store centralizado para múltiplos estados |
| State<T> | Implementação thread-safe de IState<T> |
| DerivedState<TIn,TOut> | Implementação de estado computado |
| StateStore | Implementação baseada em ConcurrentDictionary |

---

<div align=""center"">

**Próximo:** [Instalação →](installation.md)

</div>
