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
            // Handle different expression types
            switch (expression.Body)
            {
                case MemberExpression memberExpression:
                    return memberExpression.Member.Name;
                    
                case ConstantExpression constantExpression:
                    // For const values, we need to parse the debug view
                    // The DebugView property contains the original source code
                    var debugViewProperty = expression.GetType().GetProperty("DebugView", 
                        System.Reflection.BindingFlags.Instance | 
                        System.Reflection.BindingFlags.NonPublic);
                    
                    if (debugViewProperty != null)
                    {
                        var debugView = debugViewProperty.GetValue(expression) as string;
                        if (!string.IsNullOrEmpty(debugView))
                        {
                            // Debug view typically looks like: ".Lambda #Lambda1<System.Func`1[System.Int32]>() => testValue"
                            var arrowIndex = debugView.LastIndexOf("=> ", StringComparison.Ordinal);
                            if (arrowIndex >= 0)
                            {
                                var name = debugView.Substring(arrowIndex + 3).Trim();
                                // Handle string literals which appear as "value"
                                if (name.StartsWith("\"") && name.EndsWith("\""))
                                {
                                    // This is a string literal, we need to find the actual variable name
                                    // Try to get from the full expression string
                                    var exprString = expression.ToString();
                                    arrowIndex = exprString.IndexOf("=> ", StringComparison.Ordinal);
                                    if (arrowIndex >= 0)
                                    {
                                        name = exprString.Substring(arrowIndex + 3).Trim();
                                    }
                                }
                                return name;
                            }
                        }
                    }
                    
                    // Fallback: try to parse from expression string
                    var expressionStr = expression.ToString();
                    var lambdaIndex = expressionStr.IndexOf("=> ", StringComparison.Ordinal);
                    if (lambdaIndex >= 0)
                    {
                        return expressionStr.Substring(lambdaIndex + 3).Trim();
                    }
                    return "value";
                    
                case UnaryExpression unaryExpression when unaryExpression.Operand is MemberExpression memberOperand:
                    // Handle conversions/casts
                    return memberOperand.Member.Name;
                    
                default:
                    // For other expression types, return a generic name
                    return "value";
            }
        }

        private static T GetValue<T>(Expression<Func<T>> expression)
        {
            var argumentValue = expression.Compile().Invoke();
            return argumentValue;
        }
    }
}