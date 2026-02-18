using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCrossThrowIf;

public static partial class Guard
{
    /// <summary>
    /// Validates multiple values at once for better performance
    /// </summary>
    public static class Multiple
    {
        /// <summary>
        /// Validates multiple non-null values and returns them as a tuple
        /// </summary>
        public static (T1, T2) NotNull<T1, T2>(
            [NotNull] T1? value1,
            [NotNull] T2? value2,
            [CallerArgumentExpression(nameof(value1))] string? param1 = null,
            [CallerArgumentExpression(nameof(value2))] string? param2 = null)
            where T1 : class
            where T2 : class
        {
            ArgumentNullException.ThrowIfNull(value1, param1);
            ArgumentNullException.ThrowIfNull(value2, param2);
            return (value1, value2);
        }

        /// <summary>
        /// Validates three non-null values
        /// </summary>
        public static (T1, T2, T3) NotNull<T1, T2, T3>(
            [NotNull] T1? value1,
            [NotNull] T2? value2,
            [NotNull] T3? value3,
            [CallerArgumentExpression(nameof(value1))] string? param1 = null,
            [CallerArgumentExpression(nameof(value2))] string? param2 = null,
            [CallerArgumentExpression(nameof(value3))] string? param3 = null)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            ArgumentNullException.ThrowIfNull(value1, param1);
            ArgumentNullException.ThrowIfNull(value2, param2);
            ArgumentNullException.ThrowIfNull(value3, param3);
            return (value1, value2, value3);
        }

        /// <summary>
        /// Validates multiple strings are not null or empty
        /// </summary>
        public static (string, string) NotNullOrEmpty(
            [NotNull] string? value1,
            [NotNull] string? value2,
            [CallerArgumentExpression(nameof(value1))] string? param1 = null,
            [CallerArgumentExpression(nameof(value2))] string? param2 = null)
        {
            if (string.IsNullOrEmpty(value1))
                throw new ArgumentNullException(param1, $"Value '{param1}' cannot be null or empty");
            if (string.IsNullOrEmpty(value2))
                throw new ArgumentNullException(param2, $"Value '{param2}' cannot be null or empty");
            
            return (value1, value2);
        }

        /// <summary>
        /// Validates three strings are not null or empty
        /// </summary>
        public static (string, string, string) NotNullOrEmpty(
            [NotNull] string? value1,
            [NotNull] string? value2,
            [NotNull] string? value3,
            [CallerArgumentExpression(nameof(value1))] string? param1 = null,
            [CallerArgumentExpression(nameof(value2))] string? param2 = null,
            [CallerArgumentExpression(nameof(value3))] string? param3 = null)
        {
            if (string.IsNullOrEmpty(value1))
                throw new ArgumentNullException(param1, $"Value '{param1}' cannot be null or empty");
            if (string.IsNullOrEmpty(value2))
                throw new ArgumentNullException(param2, $"Value '{param2}' cannot be null or empty");
            if (string.IsNullOrEmpty(value3))
                throw new ArgumentNullException(param3, $"Value '{param3}' cannot be null or empty");
            
            return (value1, value2, value3);
        }

        /// <summary>
        /// Validates all values in a params array
        /// </summary>
        [return: NotNull]
        public static T[] AllNotNull<T>(
            [NotNull] params T?[]? values) where T : class
        {
            ArgumentNullException.ThrowIfNull(values, nameof(values));
            
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is null)
                {
                    throw new ArgumentNullException(
                        $"values[{i}]",
                        $"Value at index {i} cannot be null");
                }
            }
            
            return values!;
        }

        /// <summary>
        /// Validates all values satisfy their respective predicates
        /// </summary>
        public static (T1, T2) Satisfy<T1, T2>(
            T1 value1,
            T2 value2,
            Predicate<T1> predicate1,
            Predicate<T2> predicate2,
            [CallerArgumentExpression(nameof(value1))] string? param1 = null,
            [CallerArgumentExpression(nameof(value2))] string? param2 = null)
        {
            if (!predicate1(value1))
                throw new ArgumentException($"Value '{param1}' does not satisfy its predicate", param1);
            if (!predicate2(value2))
                throw new ArgumentException($"Value '{param2}' does not satisfy its predicate", param2);
            
            return (value1, value2);
        }
    }
}