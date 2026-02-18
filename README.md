# CCross.ThrowIf — Fluent guard clauses for .NET

[![NuGet](https://img.shields.io/nuget/v/CCrossThrowIf)](https://www.nuget.org/packages/CCrossThrowIf)
[![License](https://img.shields.io/github/license/phmatray/CCross.ThrowIf)](LICENSE)
[![Stars](https://img.shields.io/github/stars/phmatray/CCross.ThrowIf?style=social)](https://github.com/phmatray/CCross.ThrowIf)

**CCross.ThrowIf** is a lightweight C# library that provides expressive, fluent guard clauses for argument validation and exception throwing. Stop writing boilerplate `if (...) throw new ...` blocks — express your preconditions cleanly in one line.

## ✨ Features

- **Fluent guard clauses** — `ThrowIf.Argument.IsNull`, `IsNullOrEmpty`, `IsGreaterThan`, and more
- **Strongly-typed expressions** — uses `Expression<Func<T>>` to automatically capture the argument name (no more `nameof(...)`)
- **Multiple type support** — works with `string`, `int`, `long`, `float`, `double`, `decimal`, `bool`, `DateTime`, `TimeSpan`
- **Generic overloads** — `ThrowIf<TException>` lets you specify the exception type to throw
- **Clean API** — organized into logical categories: `Argument`, `Collection`, `Value`, `ArrayIndex`
- **Zero dependencies** — pure .NET Portable Class Library

## 📦 Installation

```bash
dotnet add package CCrossThrowIf
```

Or via the NuGet Package Manager:

```
Install-Package CCrossThrowIf
```

> **Note:** Targets .NET Portable (Profile 7 / .NET 4.5+). Compatible with .NET Standard projects.

## 🚀 Quick Start

```csharp
using CCrossThrowIf;

public class OrderService
{
    public void PlaceOrder(string customerId, int quantity, decimal amount)
    {
        // Throws ArgumentNullException with auto-captured argument name
        ThrowIf.Argument.IsNullOrWhiteSpace(() => customerId);

        // Throws ArgumentOutOfRangeException if quantity <= 0
        ThrowIf.Argument.IsNegativeOrZero(() => quantity);

        // Throws ArgumentOutOfRangeException if amount > 10000
        ThrowIf.Argument.IsGreaterThan(() => amount, limit: 10000m, "Order amount exceeds maximum.");

        // Process the order...
    }
}
```

### Generic exception type

```csharp
// Throw any exception type with the same fluent API
ThrowIf<InvalidOperationException>.IsNull(() => myService);
ThrowIf<ArgumentOutOfRangeException>.IsNullOrEmpty(() => configValue, "Config must be set.");
```

### DateTime validation

```csharp
ThrowIf.Argument.IsInThePast(() => scheduledDate);
ThrowIf.Argument.IsInTheFuture(() => auditTimestamp);
```

### Bool validation

```csharp
ThrowIf.Argument.IsTrue(() => isLocked, "Resource is locked and cannot be modified.");
ThrowIf.Argument.IsFalse(() => isInitialized);
```

## 📖 Documentation

Full API reference and examples available at the [project website](http://phmatray.github.io/CCross.ThrowIf/).

## 🤝 Contributing

Pull requests welcome. See [CONTRIBUTING.md](CONTRIBUTING.md) if available, or open an issue to discuss your changes.

## 📄 License

MIT — see [LICENSE](LICENSE)
