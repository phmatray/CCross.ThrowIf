using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    internal static class Helper
    {
        internal static string GetMemberName<T>(this Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("Invalid argument", nameof(expression));

            var argumentName = body.Member.Name;
            return argumentName;
        }

        internal static T GetValue<T>(this Expression<Func<T>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var argumentValue = expression.Compile().Invoke();
            return argumentValue;
        }
    }
}