using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    public static class ThrowIf
    {
        public static class Argument
        {
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value == default(T))
                    throw new ArgumentException(message ?? $"{name} is equal to its default value.");
            }

            #endregion

            #region Object

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNull(Expression<Func<object>> expression, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value == null)
                {
                    throw message != null
                        ? new ArgumentNullException(name, message)
                        : new ArgumentNullException(name);
                }
            }

            #endregion

            #region String

            /// <summary>
            ///     Throws whether a specified string is null, empty, or consists only of white-space characters.
            /// </summary>
            /// <param name="expression">The expression property.</param>
            /// <param name="message">The message.</param>
            public static void IsNullOrWhiteSpace(Expression<Func<string>> expression, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw message != null
                        ? new ArgumentNullException(name, message)
                        : new ArgumentNullException(name);
                }
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value.Ticks <= 0L)
                    throw new ArgumentOutOfRangeException(message ?? $"{name} is lower or equal to zero.");
            }

            #endregion

            #region DateTime

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsInThePast(Expression<Func<DateTime>> expression, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value < DateTime.Now)
                    throw new ArgumentException(message ?? $"{name} is in the past.");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsInTheFuture(Expression<Func<DateTime>> expression, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value > DateTime.Now)
                    throw new ArgumentException(message ?? $"{name} is in the future.");
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value)
                    throw new ArgumentException(message ?? $"{name} is true.");
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (!value)
                    throw new ArgumentException(message ?? $"{name} is false.");
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value > 0;
                IntBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<int>> expression, string message = null)
            {
                // Wrapper IntBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value < 0;
                IntBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<int>> expression, string message = null)
            {
                // Wrapper IntBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value >= 0;
                IntBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<int>> expression, string message = null)
            {
                // Wrapper IntBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value <= 0;
                IntBase(condition, name, message);
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value > 0L;
                LongBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<long>> expression, string message = null)
            {
                // Wrapper LongBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value < 0L;
                LongBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<long>> expression, string message = null)
            {
                // Wrapper LongBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value >= 0L;
                LongBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<long>> expression, string message = null)
            {
                // Wrapper LongBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value <= 0L;
                LongBase(condition, name, message);
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

            #region Decimal

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositive(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value > 0m;
                DecimalBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value < 0m;
                DecimalBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value >= 0m;
                DecimalBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<decimal>> expression, string message = null)
            {
                // Wrapper DecimalBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value <= 0m;
                DecimalBase(condition, name, message);
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
        }

        //public static class Collection { }
        //public static class Value { }
        //public static class ArrayIndex { }
    }
}