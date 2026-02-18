using System.Numerics;
using CCrossThrowIf;

namespace CCrossThrowIf.Demo;

public class InnovativeGuardDemo
{
    public async Task DemonstrateAllFeatures()
    {
        Console.WriteLine("=== CCross.ThrowIf - Innovative Guard Clauses Demo ===\n");
        
        // 1. Pattern-based validation
        Console.WriteLine("1. Pattern-based validation:");
        var email = Guard.Pattern.Email("user@example.com");
        var url = Guard.Pattern.Url("https://github.com");
        var phone = Guard.Pattern.PhoneNumber("+1-555-123-4567");
        var guid = Guard.Pattern.Guid("550e8400-e29b-41d4-a716-446655440000");
        var customPattern = Guard.Pattern.Matches("ABC123", @"^[A-Z]{3}\d{3}$");
        Console.WriteLine($"✓ All patterns validated successfully\n");

        // 2. Ensure conditions (compile-time optimized)
        Console.WriteLine("2. Ensure conditions:");
        var numbers = new[] { 2, 4, 6, 8 };
        Guard.Ensure.All<int[], int>(numbers, n => n % 2 == 0);
        Guard.Ensure.Any<int[], int>(numbers, n => n > 5);
        Guard.Ensure.Satisfies(42, n => n > 0 && n < 100);
        Console.WriteLine($"✓ All conditions ensured\n");

        // 3. Multiple value validation
        Console.WriteLine("3. Multiple value validation:");
        var (repo, logger) = Guard.Multiple.NotNull<IRepository, ILogger>(
            new Repository(), 
            new ConsoleLogger()
        );
        var (firstName, lastName) = Guard.Multiple.NotNullOrEmpty("John", "Doe");
        var allValid = Guard.Multiple.AllNotNull("a", "b", "c", "d");
        Console.WriteLine($"✓ Multiple values validated in single call\n");

        // 4. Async validation
        Console.WriteLine("4. Async validation:");
        var task = Task.FromResult(42);
        var result = await Guard.Async.NotFaulted(task);
        var timedResult = await Guard.Async.CompletesWithin(
            DelayedTask(), 
            TimeSpan.FromSeconds(2)
        );
        await foreach (var item in Guard.Async.NotEmpty(GenerateAsync()))
        {
            Console.WriteLine($"  Async item: {item}");
        }
        Console.WriteLine($"✓ Async operations validated\n");

        // 5. Conditional validation
        Console.WriteLine("5. Conditional validation:");
        var config = new Config { Mode = "Production" };
        Guard.When.Condition(
            config.Mode, 
            config.Mode == "Production",
            mode => Guard.Against.NullOrWhiteSpace(mode)
        );
        
        var (primary, fallback) = Guard.When.ExclusiveOr<string, string>(
            "primary", 
            null
        );
        Console.WriteLine($"✓ Conditional validations passed\n");

        // 6. Fluent builder pattern
        Console.WriteLine("6. Fluent validation builder:");
        var validatedAge = 25
            .Validate()
            .Must(age => age >= 18, "Must be an adult")
            .Must(age => age <= 100, "Invalid age")
            .When(true, age => age < 65, "Must be under retirement age")
            .Transform(age => $"Age: {age}")
            .Validate();
        
        Console.WriteLine($"  {validatedAge}");
        
        // Try validation with result
        var validationResult = "test@example.com"
            .Validate()
            .Must(s => s.Contains('@'), "Must be an email")
            .TryValidate();
        
        if (validationResult.TryGetValue(out var validEmail))
        {
            Console.WriteLine($"  Valid email: {validEmail}");
        }
        Console.WriteLine($"✓ Fluent validations completed\n");

        // 7. High-performance validation
        Console.WriteLine("7. High-performance validation:");
        
        // Fast null check with aggressive inlining
        var obj = Guard.Performance.NotNullFast(new object());
        
        // Power of 2 validation using bit manipulation
        var powerOfTwo = Guard.Performance.PowerOfTwo(16);
        
        // Stack-allocated batch validation
        Span<int> values = stackalloc int[] { 1, 2, 3, 4, 5 };
        Guard.Performance.ValidateAll<int>(values, n => n > 0);
        
        // Fast email validation without regex
        var isValidEmail = Guard.Performance.IsEmailFast("user@example.com");
        
        Console.WriteLine($"✓ Performance validations completed\n");

        // 8. Span and Memory validation
        Console.WriteLine("8. Span/Memory validation:");
        ReadOnlySpan<int> span = new[] { 1, 2, 3, 4, 5 };
        var validSpan = Guard.Span.NotEmpty(span);
        var minLengthSpan = Guard.Span.MinLength(span, 3);
        var allPositive = Guard.Span.All(span, n => n > 0);
        Console.WriteLine($"✓ Span validations completed\n");

        // 9. Complex real-world example
        Console.WriteLine("9. Real-world example - User registration:");
        var registration = await ValidateUserRegistration(
            new UserRegistration
            {
                Email = "john.doe@example.com",
                Password = "SecurePass123!",
                Age = 25,
                PhoneNumber = "+1-555-123-4567",
                AcceptedTerms = true,
                ReferralCode = "FRIEND2023"
            }
        );
        Console.WriteLine($"✓ User registration validated: {registration.Email}\n");

        Console.WriteLine("=== All demonstrations completed successfully! ===");
    }

    private async Task<UserRegistration> ValidateUserRegistration(UserRegistration registration)
    {
        // Validate all fields
        var email = Guard.Against.NullOrEmpty(registration.Email);
        var password = Guard.Against.NullOrEmpty(registration.Password);
        var phone = Guard.Against.NullOrEmpty(registration.PhoneNumber);

        // Pattern validation
        Guard.Pattern.Email(email);
        Guard.Pattern.PhoneNumber(phone);
        Guard.Pattern.Length(password, 8, 128);

        // Business rules
        registration.Age
            .Validate()
            .InRange(18, 120)
            .Must(age => age >= 21 || registration.ParentalConsent, "Parental consent required")
            .Validate();

        // Conditional validation
        Guard.When.Condition(
            registration.ReferralCode,
            !string.IsNullOrEmpty(registration.ReferralCode),
            code => Guard.Pattern.Matches(code!, @"^[A-Z]+\d{4}$")
        );

        // Ensure terms accepted
        Guard.Ensure.That(
            registration.AcceptedTerms,
            "Terms and conditions must be accepted"
        );

        // Simulate async validation (e.g., checking if email already exists)
        await Task.Delay(100);
        
        return registration;
    }

    // Helper classes
    private async Task<int> DelayedTask()
    {
        await Task.Delay(1000);
        return 42;
    }

    private async IAsyncEnumerable<int> GenerateAsync()
    {
        for (int i = 1; i <= 3; i++)
        {
            await Task.Delay(100);
            yield return i;
        }
    }

    private record UserRegistration
    {
        public string Email { get; init; } = "";
        public string Password { get; init; } = "";
        public string PhoneNumber { get; init; } = "";
        public int Age { get; init; }
        public bool AcceptedTerms { get; init; }
        public string? ReferralCode { get; init; }
        public bool ParentalConsent { get; init; }
    }

    private record Config
    {
        public string Mode { get; init; } = "Development";
    }

    private interface IRepository { }
    private interface ILogger { }
    private class Repository : IRepository { }
    private class ConsoleLogger : ILogger { }
}