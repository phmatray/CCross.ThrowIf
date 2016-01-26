using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    public static class ThrowIf<TException>
        where TException : Exception, new()
    {
        #region String

        /// <summary>
        ///     Throws whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="expression">The expression property.</param>
        /// <param name="message">The message.</param>
        public static void IsNullOrWhiteSpace(Expression<Func<string>> expression, string message = null)
        {
            var metadata = expression.GetMetadata();
            if (!string.IsNullOrWhiteSpace(metadata.Value))
                return;

            throw Helper.CreateException<TException>(
                message, metadata.Name);
        }

        /// <summary>
        ///     Throws whether a specified string is null or an Empty string.
        /// </summary>
        /// <param name="expression">The expression property.</param>
        /// <param name="message">The message.</param>
        public static void IsNullOrEmpty(Expression<Func<string>> expression, string message = null)
        {
            var metadata = expression.GetMetadata();
            if (!string.IsNullOrEmpty(metadata.Value))
                return;
                
            throw Helper.CreateException<TException>(
                message, metadata.Name);
        }

        #endregion

        #region TimeSpan

        /// <summary>
        ///     Throws an <see cref="ArgumentOutOfRangeException" /> exception of the specified <paramref name="expression" /> is
        ///     less than zero.
        /// </summary>
        /// <param name="expression">The expression property to validate.</param>
        /// <param name="message">The exception message whether an exception is thrown.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="expression" /> is not greater than zero.
        /// </exception>
        public static void IsNegativeOrZero(Expression<Func<TimeSpan>> expression, string message = null)
        {
            var metadata = expression.GetMetadata();
            if (metadata.Value.Ticks > 0L)
                return;

            throw Helper.CreateException<TException>(
                message ?? $"{metadata.Name} is lower or equal to zero.");
        }

        #endregion

        #region Generic

        /// <summary>
        ///     Throws whether the value is equal to its default value.
        /// </summary>
        /// <typeparam name="T">A class type.</typeparam>
        /// <param name="expression">The expression property.</param>
        /// <param name="message">The message.</param>
        public static void IsDefault<T>(Expression<Func<T>> expression, string message = null)
            where T : class
        {
            var metadata = expression.GetMetadata();
            if (metadata.Value != null)
                return;

            throw Helper.CreateException<TException>(
                message ?? $"{metadata.Name} is equal to its default value.");
        }

        /// <summary>
        ///     Throws whether the value is null.
        /// </summary>
        /// <typeparam name="T">A class type.</typeparam>
        /// <param name="expression">The expression property.</param>
        /// <param name="message">The message.</param>
        public static void IsNull<T>(Expression<Func<T>> expression, string message = null)
            where T : class
        {
            var metadata = expression.GetMetadata();
            if (metadata.Value != null)
                return;

            throw Helper.CreateException<TException>(
                message ?? $"{metadata.Name} is null.");
        }

        /// <summary>
        ///     Throws whether the value is equal to an other specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">The expression property.</param>
        /// <param name="checkedValue">The specified value.</param>
        /// <param name="message">The message.</param>
        public static void IsEqualTo<T>(Expression<Func<T>> expression, T checkedValue = default(T), string message = null)
        {
            var metadata = expression.GetMetadata();
            if (!metadata.Value.Equals(checkedValue))
                return;

            throw Helper.CreateException<TException>(
                message ?? $"{metadata.Name} is not equal to {checkedValue}",
                metadata.Name, checkedValue);
        }

        #endregion
    }
}