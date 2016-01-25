using System;
using System.Linq.Expressions;

namespace CCrossThrowIf
{
    internal class Metadata<TType>
    {
        public string Name { get; }
        public TType Value { get; }

        public Metadata(Expression<Func<TType>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            Name = GetMemberName(expression);
            Value = GetValue(expression);
        }

        private static string GetMemberName<T>(Expression<Func<T>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("Invalid argument", nameof(expression));

            var argumentName = body.Member.Name;
            return argumentName;
        }

        private static T GetValue<T>(Expression<Func<T>> expression)
        {
            var argumentValue = expression.Compile().Invoke();
            return argumentValue;
        }
    }
}