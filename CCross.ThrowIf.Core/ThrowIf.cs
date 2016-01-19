using System;
using System.Linq.Expressions;

namespace CCross.ThrowIf.Core
{
    public static partial class ThrowIf
    {
        public static partial class Argument
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNull(Expression<Func<string>> expression, string message = null)
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

            /// <summary>
            /// 
            /// </summary>
            /// <param name="expression"></param>
            /// <param name="message"></param>
            public static void IsNullOrWhiteSpace(Expression<Func<string>> expression, string message = null)
            {
                var name = expression.GetMemberName();
                var value = expression.GetValue();

                if (String.IsNullOrWhiteSpace(value))
                {
                    throw message != null
                        ? new ArgumentNullException(name, message)
                        : new ArgumentNullException(name);
                }
            }

            /// <summary>
            /// 
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
            /// 
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

            /// <summary>
            /// 
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
            /// 
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
            /// 
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
            /// 
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
        }

        //public static class Collection { }
        //public static class Value { }
        //public static class ArrayIndex { }
    }
}
