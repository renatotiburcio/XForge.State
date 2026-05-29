> ⚠️ **Aviso:** Este pacote está em fase de implantação e evolução contínua.

---

# 06 — Quick Start

## Estado simples

`csharp
var name = new State<string>("Visitante");

// Subscribe recebe o valor atual imediatamente
using var subscription = name.Subscribe(value =>
    Console.WriteLine($"Olá, {value}!"));

name.Update("Maria"); // Output: "Olá, Maria!"
`

## Estado derivado

`csharp
var firstName = new State<string>("João");
var lastName = new State<string>("Silva");

var fullName = new DerivedState<string, string>(
    firstName,
    first => $"{first} {lastName.Value}");

Console.WriteLine(fullName.Value); // "João Silva"
firstName.Update("Pedro");
Console.WriteLine(fullName.Value); // "Pedro Silva"
`

## StateStore

`csharp
var store = new StateStore();

var counter = store.GetOrCreate("counter", 0);
counter.Update(prev => prev + 1);

var found = store.Find<int>("counter");
Console.WriteLine(found?.Value); // 1
`

---

**Navegação:** ← [Instalação](installation.md) | → [Configuração](configuration.md)
