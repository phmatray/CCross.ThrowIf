using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    internal static class Helper
    {
        internal static Metadata<T> GetMetadata<T>(this Expression<Func<T>> expression)
        {
            return new Metadata<T>(expression);
        }

        public static TException CreateException<TException>(string message)
            where TException : Exception, new()
        {
            var args = CreateArgsException<TException>(message);
            return (TException)Activator.CreateInstance(typeof(TException), args);
        }

        public static TException CreateException<TException>(string message, string name, object checkedValue = null)
            where TException : Exception, new()
        {
            var args = CreateArgsException<TException>(message, name, checkedValue);
            return (TException)Activator.CreateInstance(typeof(TException), args);
        }

        private static object[] CreateArgsException<TException>(string message, string paramName = null, object actualValue = null)
        {
            switch (typeof (TException).Name)
            {
                case nameof(Exception):
                    return new object[] {message};
                case nameof(ArgumentNullException):
                    return new object[] {paramName, message};
                case nameof(ArgumentOutOfRangeException):
                    return new object[] {paramName, actualValue, message};
                default:
                    return new object[] {};
            }
        }
    }
}