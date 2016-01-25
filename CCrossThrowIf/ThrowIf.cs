using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    public static partial class ThrowIf
    {
        public static class Argument
        {
            #region String

            /// <summary>
            ///     Throws whether a specified string is null, empty, or consists only of white-space characters.
            /// </summary>
            /// <param name="expression">The expression property.</param>
            /// <param name="message">The message.</param>
            public static void IsNullOrWhiteSpace(Expression<Func<string>> expression, string message = null)
            {
                ThrowIf<ArgumentNullException>
                    .IsNullOrWhiteSpace(expression, message);
            }

            /// <summary>
            ///     Throws whether a specified string is null or an Empty string.
            /// </summary>
            /// <param name="expression">The expression property.</param>
            /// <param name="message">The message.</param>
            public static void IsNullOrEmpty(Expression<Func<string>> expression, string message = null)
            {
                ThrowIf<ArgumentNullException>
                    .IsNullOrEmpty(expression, message);
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

                if (message == null)
                    message = $"{metadata.Name} is lower or equal to zero.";

                if (metadata.Value.Ticks <= 0L)
                    throw new ArgumentOutOfRangeException(message);
            }

            #endregion

            #region DateTime

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsInThePast(Expression<Func<DateTime>> expression, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value < DateTime.Now)
                    throw new ArgumentException(message ?? $"{metadata.Name} is in the past.");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsInTheFuture(Expression<Func<DateTime>> expression, string message = null)
            {
                var metadata = expression.GetMetadata();

                if (metadata.Value > DateTime.Now)
                    throw new ArgumentException(message ?? $"{metadata.Name} is in the future.");
            }

            #endregion

            #region Bool

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            /// <exception cref="ArgumentException">
            ///     <paramref name="expression" /> is true.
            /// </exception>
            public static void IsTrue(Expression<Func<bool>> expression, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value)
                    throw new ArgumentException(message ?? $"{metadata.Name} is true.");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            /// <exception cref="ArgumentException">
            ///     <paramref name="expression" /> is false.
            /// </exception>
            public static void IsFalse(Expression<Func<bool>> expression, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (!metadata.Value)
                    throw new ArgumentException(message ?? $"{metadata.Name} is false.");
            }

            #endregion

            #region Int

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositive(Expression<Func<int>> expression, string message = null)
            {
                // Wrapper IntBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value > 0;
                IntBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<int>> expression, string message = null)
            {
                // Wrapper IntBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value < 0;
                IntBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<int>> expression, string message = null)
            {
                // Wrapper IntBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value >= 0;
                IntBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<int>> expression, string message = null)
            {
                // Wrapper IntBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value <= 0;
                IntBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<int>> expression, int limit = 0, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value > limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<int>> expression, int limit = 0, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value < limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is lower than {limit}");
            }

            private static void IntBase(bool condition, string name, string message)
            {
                if (condition)
                {
                    throw message != null
                        ? new ArgumentOutOfRangeException(name, message)
                        : new ArgumentOutOfRangeException(name);
                }
            }

            #endregion

            #region Long

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositive(Expression<Func<long>> expression, string message = null)
            {
                // Wrapper LongBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value > 0L;
                LongBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<long>> expression, string message = null)
            {
                // Wrapper LongBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value < 0L;
                LongBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<long>> expression, string message = null)
            {
                // Wrapper LongBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value >= 0L;
                LongBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<long>> expression, string message = null)
            {
                // Wrapper LongBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value <= 0L;
                LongBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<long>> expression, long limit = 0L, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value > limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<long>> expression, long limit = 0L, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value < limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is lower than {limit}");
            }

            private static void LongBase(bool condition, string name, string message)
            {
                if (condition)
                {
                    throw message != null
                        ? new ArgumentOutOfRangeException(name, message)
                        : new ArgumentOutOfRangeException(name);
                }
            }

            #endregion

            #region Float

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositive(Expression<Func<float>> expression, string message = null)
            {
                // Wrapper FloatBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value > 0F;
                FloatBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<float>> expression, string message = null)
            {
                // Wrapper FloatBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value < 0F;
                FloatBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<float>> expression, string message = null)
            {
                // Wrapper FloatBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value >= 0F;
                FloatBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<float>> expression, string message = null)
            {
                // Wrapper FloatBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value <= 0F;
                FloatBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<float>> expression, float limit = 0F, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value > limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<float>> expression, float limit = 0F, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value < limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is lower than {limit}");
            }

            private static void FloatBase(bool condition, string name, string message)
            {
                if (condition)
                {
                    throw message != null
                        ? new ArgumentOutOfRangeException(name, message)
                        : new ArgumentOutOfRangeException(name);
                }
            }

            #endregion

            #region Double

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositive(Expression<Func<double>> expression, string message = null)
            {
                // Wrapper DoubleBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value > 0D;
                DoubleBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<double>> expression, string message = null)
            {
                // Wrapper DoubleBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value < 0D;
                DoubleBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<double>> expression, string message = null)
            {
                // Wrapper DoubleBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value >= 0D;
                DoubleBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<double>> expression, string message = null)
            {
                // Wrapper DoubleBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value <= 0D;
                DoubleBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<double>> expression, double limit = 0D, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value > limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<double>> expression, double limit = 0D, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value < limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is lower than {limit}");
            }

            private static void DoubleBase(bool condition, string name, string message)
            {
                if (condition)
                {
                    throw message != null
                        ? new ArgumentOutOfRangeException(name, message)
                        : new ArgumentOutOfRangeException(name);
                }
            }

            #endregion

            #region Decimal

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositive(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value > 0m;
                DecimalBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value < 0m;
                DecimalBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value >= 0m;
                DecimalBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var metadata = expression.GetMetadata();
                var condition = metadata.Value <= 0m;
                DecimalBase(condition, metadata.Name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<decimal>> expression, decimal limit = 0m, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value > limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<decimal>> expression, decimal limit = 0m, string message = null)
            {
                var metadata = expression.GetMetadata();
                
                if (metadata.Value < limit)
                    throw new ArgumentOutOfRangeException(metadata.Name, metadata.Value, message ?? $"{metadata.Name} is lower than {limit}");
            }

            private static void DecimalBase(bool condition, string name, string message)
            {
                if (condition)
                {
                    throw message != null
                        ? new ArgumentOutOfRangeException(name, message)
                        : new ArgumentOutOfRangeException(name);
                }
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
                ThrowIf<ArgumentException>
                    .IsDefault(expression, message);
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
                ThrowIf<ArgumentException>
                    .IsNull(expression, message);
            }

            /// <summary>
            ///     Throws whether the value is equal to an other specified value.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="expression">The expression property.</param>
            /// <param name="testValue">The specified value.</param>
            /// <param name="message">The message.</param>
            public static void IsEqualTo<T>(Expression<Func<T>> expression, T testValue = default(T), string message = null)
            {
                ThrowIf<ArgumentOutOfRangeException>
                    .IsEqualTo(expression, testValue, message);
            }

            #endregion
        }
    }

    public static partial class ThrowIf
    {
        public static class Collection
        {
            
        }
    }

    public static partial class ThrowIf
    {
        public static class Value
        {
            
        }
    }

    public static partial class ThrowIf
    {
        public static class ArrayIndex
        {
            
        }
    }
}