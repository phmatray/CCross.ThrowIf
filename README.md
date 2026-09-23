![CCross.ThrowIf banner](.github/banner.png)

# CCross.ThrowIf

<!-- portfolio-badges:start -->
<!-- Identity -->
[![phmatray - CCross.ThrowIf](https://img.shields.io/static/v1?label=phmatray&message=CCross.ThrowIf&color=blue&logo=github)](https://github.com/phmatray/CCross.ThrowIf)
![Top language](https://img.shields.io/github/languages/top/phmatray/CCross.ThrowIf)
[![Stars](https://img.shields.io/github/stars/phmatray/CCross.ThrowIf?style=social)](https://github.com/phmatray/CCross.ThrowIf/stargazers)
[![Forks](https://img.shields.io/github/forks/phmatray/CCross.ThrowIf?style=social)](https://github.com/phmatray/CCross.ThrowIf/network/members)

<!-- Activity -->
[![Issues](https://img.shields.io/github/issues/phmatray/CCross.ThrowIf)](https://github.com/phmatray/CCross.ThrowIf/issues)
[![Pull requests](https://img.shields.io/github/issues-pr/phmatray/CCross.ThrowIf)](https://github.com/phmatray/CCross.ThrowIf/pulls)
[![Last commit](https://img.shields.io/github/last-commit/phmatray/CCross.ThrowIf)](https://github.com/phmatray/CCross.ThrowIf/commits)
<!-- portfolio-badges:end -->

<!-- portfolio-toc:start -->

## Table of Contents

- [Features](#features)
- [Usage](#usage)
- [Tech Stack](#tech-stack)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

<!-- portfolio-toc:end -->


Throw your exceptions easily.

Please check the [Website](http://phmatray.github.io/CCross.ThrowIf/)

---

## Features

- **Value-returning guards** – `Guard.Against.*` validates and returns the value in one expression (`var id = Guard.Against.NegativeOrZero(userId);`), so validation and assignment happen on the same line.
- **Rich validation surface** – guards for null/empty/whitespace strings, numeric ranges, zero/negative values, GUIDs, emails, URLs, phone numbers, regex patterns, and date/time ranges.
- **Generic math support** – numeric guards such as `Negative`, `Zero`, `OutOfRange`, and `PowerOfTwo` built on `INumber<T>` for C# 11 / .NET 7+ generic constraints.
- **Async & Task guards** – `Guard.Async` helpers like `CompletesWithin` and `NotFaulted` for validating asynchronous operations and cancellation.
- **Span & collection guards** – `Guard.Span` and collection guards (`NotEmpty`, `NoNulls`, `Length`, `MinLength`, `AllNotNull`) for zero-allocation, high-performance validation paths.
- **Fluent extension methods** – `GuardNullOrWhiteSpace()`, `GuardNullOrEmpty()`, `GuardNull()` extension methods for inline chaining without static calls.
- **Multi-value & pattern guards** – `Guard.Multiple` and `Guard.Pattern` for validating several values at once, exclusive-or checks, and `Satisfies`/`Condition` predicates.
- **Legacy expression-based API** – the original `ThrowIf.Argument` / `ThrowIf<TException>` expression-based API is retained for backward compatibility.

## Usage

Install from NuGet:

```bash
dotnet add package CCross.ThrowIf
```

Use `Guard.Against` to validate and use the value in a single expression:

```csharp
using CCrossThrowIf;

public class OrderService
{
    private readonly IRepository _repository;

    public OrderService(IRepository repository, string connectionString)
    {
        // Validates and assigns in one line — throws if invalid
        _repository = Guard.Against.Null(repository);
        var validConnectionString = Guard.Against.NullOrEmpty(connectionString);
    }

    public async Task<Order?> PlaceOrderAsync(int quantity, CancellationToken cancellationToken)
    {
        // Guard.Against returns the validated value so it can be used directly
        var validQuantity = Guard.Against.NegativeOrZero(quantity);

        return await _repository.CreateOrderAsync(validQuantity, cancellationToken);
    }
}
```

<!-- portfolio-techstack:start -->

## Tech Stack

- **.NET 9**
- BenchmarkDotNet
- xunit.v3
- xunit.runner.visualstudio

<!-- portfolio-techstack:end -->

## Roadmap

- [ ] Expand generic math guard coverage (additional range operators, more numeric predicates)
- [ ] Add source-generator based guards for compile-time validation
- [ ] Grow the benchmark suite with comparisons against other guard-clause libraries
- [ ] Publish versioned API reference alongside the existing docs site
- [ ] Add more collection and span guard overloads

See the [open issues](https://github.com/phmatray/CCross.ThrowIf/issues) for details and to propose new guards.

<!-- portfolio-sections:start -->

## Contributing

Contributions are welcome. Open an issue first to discuss any significant change.

1. Fork the repository and create your branch (`git checkout -b feat/my-feature`)
2. Commit your changes (`git commit -m 'feat: ...'`)
3. Push the branch and open a Pull Request

## License

No license has been declared for this repository yet. Until one is added, default copyright applies — see [choosealicense.com](https://choosealicense.com/) if you intend to open it up.

<!-- portfolio-sections:end -->
