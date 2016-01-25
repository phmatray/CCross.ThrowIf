using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    internal static class Helper
    {
        internal static Metadata<T> GetMetadata<T>(this Expression<Func<T>> expression)
        {
            return new Metadata<T>(expression);
        }

        internal static TException CreateException<TException>(object[] args = null)
            where TException : Exception, new()
        {
            return (TException)Activator.CreateInstance(typeof(TException), args);
        }
    }
}