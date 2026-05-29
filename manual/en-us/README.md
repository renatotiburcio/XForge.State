# XForge.State — Official Manual

<p align=""center"">
  <img src=""./icon.png"" alt=""XForge.State"" width=""128"" height=""128"" />
</p>

<p align=""center"">
  <strong>Reactive state management for .NET with Blazor support</strong>
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

> ✅ **Stable Release:** v0.4.0 — APIs follow Semantic Versioning.

---

## Table of Contents

| # | Chapter | File |
|---|---------|------|
| 01-04 | Cover, Introduction, Status, Features | [README.md](README.md) |
| 05 | Installation | [installation.md](installation.md) |
| 06 | Quick Start | [quick-start.md](quick-start.md) |
| 07 | Configuration | [configuration.md](configuration.md) |
| 08 | Architecture | [architecture.md](architecture.md) |
| 09 | Basic Usage | [basic-usage.md](basic-usage.md) |
| 10 | Intermediate Usage | [intermediate-usage.md](intermediate-usage.md) |
| 11 | Advanced Usage | [advanced-usage.md](advanced-usage.md) |
| 12 | Enterprise Best Practices | [enterprise-best-practices.md](enterprise-best-practices.md) |
| 13 | Integration Examples | [integration-examples.md](integration-examples.md) |
| 14 | Testing | [testing.md](testing.md) |
| 15 | Performance | [performance.md](performance.md) |
| 16 | Troubleshooting | [troubleshooting.md](troubleshooting.md) |
| 17 | FAQ | [faq.md](faq.md) |
| 18 | Roadmap | [roadmap.md](roadmap.md) |
| 19 | Changelog | [changelog.md](changelog.md) |
| 20 | API Reference | [api-reference.md](api-reference.md) |
| 21 | Competitor Comparison | [package-comparison.md](package-comparison.md) |
| 22 | Migration Guide | [migration-guide.md](migration-guide.md) |
| 23 | Contributing | [contributing.md](contributing.md) |
| 24 | License | [license.md](license.md) |
| 25 | Final Notes | [final-notes.md](final-notes.md) |
| 26 | Extensibility | [extensibility.md](extensibility.md) |
| 27 | Blazor Integration | [blazor-integration.md](blazor-integration.md) |
| 28 | Multi-TFM Compatibility | [multi-tfm-compatibility.md](multi-tfm-compatibility.md) |
| 29 | Security | [security.md](security.md) |
| 30 | Support & Community | [support-community.md](support-community.md) |

---

## 02 — Introduction

### What It Is

XForge.State is a .NET library for reactive state management. It provides IState<T> with change notifications, DerivedState for computed state, and StateStore for centralized management.

`csharp
var counter = new State<int>(0);
counter.Subscribe(value => Console.WriteLine($"Counter: {value}"));
counter.Update(1); // Notifies: "Counter: 1"
`

### Main APIs

| Interface | Description |
|-----------|-------------|
| IState<T> | Reactive state container with Subscribe |
| IDerivedState<T> | Derived state that auto-recalculates |
| IStateStore | Centralized store for multiple states |
| State<T> | Thread-safe IState<T> implementation |
| DerivedState<TIn,TOut> | Computed state implementation |
| StateStore | ConcurrentDictionary-based store |

---

<div align=""center"">

**Next:** [Installation →](installation.md)

</div>
