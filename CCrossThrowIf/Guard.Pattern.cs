using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace CCrossThrowIf;

public static partial class Guard
{
    /// <summary>
    /// Pattern-based validation using modern C# patterns
    /// </summary>
    public static class Pattern
    {
        /// <summary>
        /// Validates email format using pattern matching
        /// </summary>
        [return: NotNull]
        public static string Email(
            [NotNull] string? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(value, paramName);
            
            const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(value, emailPattern, RegexOptions.IgnoreCase))
            {
                throw new ArgumentException(
                    $"Value '{paramName}' is not a valid email address",
                    paramName);
            }
            
            return value;
        }

        /// <summary>
        /// Validates URL format
        /// </summary>
        [return: NotNull]
        public static string Url(
            [NotNull] string? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(value, paramName);
            
            if (!Uri.TryCreate(value, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException(
                    $"Value '{paramName}' is not a valid HTTP(S) URL",
                    paramName);
            }
            
            return value;
        }

        /// <summary>
        /// Validates phone number format (simple pattern)
        /// </summary>
        [return: NotNull]
        public static string PhoneNumber(
            [NotNull] string? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(value, paramName);
            
            const string phonePattern = @"^[\+]?[\d(][\d\s\-\.\(\)]{6,}[\d]$";
            if (!Regex.IsMatch(value, phonePattern))
            {
                throw new ArgumentException(
                    $"Value '{paramName}' is not a valid phone number",
                    paramName);
            }
            
            return value;
        }

        /// <summary>
        /// Validates against a custom regex pattern
        /// </summary>
        [return: NotNull]
        public static string Matches(
            [NotNull] string? value,
            [StringSyntax(StringSyntaxAttribute.Regex)] string pattern,
            [CallerArgumentExpression(nameof(value))] string? paramName = null,
            string? message = null)
        {
            ArgumentNullException.ThrowIfNull(value, paramName);
            
            if (!Regex.IsMatch(value, pattern))
            {
                throw new ArgumentException(
                    message ?? $"Value '{paramName}' does not match pattern '{pattern}'",
                    paramName);
            }
            
            return value;
        }

        /// <summary>
        /// Validates that a string has a specific length
        /// </summary>
        [return: NotNull]
        public static string Length(
            [NotNull] string? value,
            int minLength,
            int maxLength,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(value, paramName);
            
            if (value.Length < minLength || value.Length > maxLength)
            {
                throw new ArgumentException(
                    $"Value '{paramName}' length must be between {minLength} and {maxLength} characters",
                    paramName);
            }
            
            return value;
        }

        /// <summary>
        /// Validates UUID/GUID format
        /// </summary>
        [return: NotNull]
        public static string Guid(
            [NotNull] string? value,
            [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(value, paramName);
            
            if (!System.Guid.TryParse(value, out _))
            {
                throw new ArgumentException(
                    $"Value '{paramName}' is not a valid GUID",
                    paramName);
            }
            
            return value;
        }
    }
}