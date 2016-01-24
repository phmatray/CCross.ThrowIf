using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    public static class ThrowIf
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

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<int>> expression, int limit = 0, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value > limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<int>> expression, int limit = 0, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value < limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is lower than {limit}");
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

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<long>> expression, long limit = 0L, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value > limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<long>> expression, long limit = 0L, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value < limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is lower than {limit}");
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value > 0F;
                FloatBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<float>> expression, string message = null)
            {
                // Wrapper FloatBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value < 0F;
                FloatBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<float>> expression, string message = null)
            {
                // Wrapper FloatBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value >= 0F;
                FloatBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<float>> expression, string message = null)
            {
                // Wrapper FloatBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value <= 0F;
                FloatBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<float>> expression, float limit = 0F, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value > limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<float>> expression, float limit = 0F, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value < limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is lower than {limit}");
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
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value > 0D;
                DoubleBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegative(Expression<Func<double>> expression, string message = null)
            {
                // Wrapper DoubleBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value < 0D;
                DoubleBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsPositiveOrZero(Expression<Func<double>> expression, string message = null)
            {
                // Wrapper DoubleBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value >= 0D;
                DoubleBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNegativeOrZero(Expression<Func<double>> expression, string message = null)
            {
                // Wrapper DoubleBase
                var name = expression.GetMemberName();
                var value = expression.GetValue();
                var condition = value <= 0D;
                DoubleBase(condition, name, message);
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<double>> expression, double limit = 0D, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value > limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<double>> expression, double limit = 0D, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value < limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is lower than {limit}");
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

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsGreaterThan(Expression<Func<decimal>> expression, decimal limit = 0m, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value > limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is greater than {limit}");
            }

            /// <summary>
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="limit"></param>
            /// <param name="message"></param>
            public static void IsLowerThan(Expression<Func<decimal>> expression, decimal limit = 0m, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (value < limit)
                    throw new ArgumentOutOfRangeException(name, value, message ?? $"{name} is lower than {limit}");
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

        //public static class Collection { }
        //public static class Value { }
        //public static class ArrayIndex { }
    }

    public static class ThrowIf<TException>
        where TException : Exception, new()
    {
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

            if (message == null)
                message = $"{name} is equal to its default value.";

            if (value == null)
            {
                var args = new object[] { message };
                var instance = (TException)Activator.CreateInstance(typeof(TException), args);
                throw instance;
            }
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
            var name = expression.GetMemberName();
            var value = expression.GetValue();

            if (message == null)
                message = $"{name} is null.";

            if (value == null)
            {
                var args = new object[] {message};
                var instance = (TException) Activator.CreateInstance(typeof (TException), args);
                throw instance;
            }
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
            var name = expression.GetMemberName();
            var value = expression.GetValue();

            if (message == null)
                message = $"{name} is not equal to {testValue}";

            if (value.Equals(testValue))
            {
                var args = typeof(TException) == typeof(ArgumentOutOfRangeException)
                    ? new object[] { name, value, message}
                    : new object[] { message };

                var instance = (TException)Activator.CreateInstance(typeof(TException), args);
                throw instance;
            }
        }
    }
}