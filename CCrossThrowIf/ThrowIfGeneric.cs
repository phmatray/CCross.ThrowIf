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

            if (string.IsNullOrWhiteSpace(metadata.Value))
            {
                var args = typeof (TException) == typeof (ArgumentNullException) && message != null
                    ? new object[] {metadata.Name, message}
                    : new object[] {metadata.Name};

                var instance = (TException) Activator.CreateInstance(typeof (TException), args);
                throw instance;
            }
        }

        /// <summary>
        ///     Throws whether a specified string is null or an Empty string.
        /// </summary>
        /// <param name="expression">The expression property.</param>
        /// <param name="message">The message.</param>
        public static void IsNullOrEmpty(Expression<Func<string>> expression, string message = null)
        {
            var metadata = expression.GetMetadata();

            if (string.IsNullOrEmpty(metadata.Value))
            {
                var args = typeof (TException) == typeof (ArgumentNullException) && message != null
                    ? new object[] {metadata.Name, message}
                    : new object[] {metadata.Name};

                var instance = (TException) Activator.CreateInstance(typeof (TException), args);
                throw instance;
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
            var metadata = expression.GetMetadata();

            if (message == null)
                message = $"{metadata.Name} is equal to its default value.";

            if (metadata.Value == null)
            {
                var args = new object[] {message};
                var instance = (TException) Activator.CreateInstance(typeof (TException), args);
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
            var metadata = expression.GetMetadata();

            if (message == null)
                message = $"{metadata.Name} is null.";

            if (metadata.Value == null)
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
            var metadata = expression.GetMetadata();

            if (message == null)
                message = $"{metadata.Name} is not equal to {testValue}";

            if (metadata.Value.Equals(testValue))
            {
                var args = typeof (TException) == typeof (ArgumentOutOfRangeException)
                    ? new object[] {metadata.Name, metadata.Value, message}
                    : new object[] {message};

                var instance = (TException) Activator.CreateInstance(typeof (TException), args);
                throw instance;
            }
        }

        #endregion
    }
}