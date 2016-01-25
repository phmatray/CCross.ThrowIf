using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    internal static class MetadataHelper
    {
        internal static Metadata<T> GetMetadata<T>(this Expression<Func<T>> expression)
        {
            return new Metadata<T>(expression);
        }
    }
}