using System;
using System.Linq.Expressions;
using System.Reflection;

class Program
{
    static void Main()
    {
        // Test with const int
        const int testValue = 42;
        Expression<Func<int>> expr1 = () => testValue;
        Console.WriteLine($"Expression Type: {expr1.Body.GetType().Name}");
        Console.WriteLine($"Expression ToString: {expr1.ToString()}");
        Console.WriteLine($"Body ToString: {expr1.Body.ToString()}");
        
        // Try to get DebugView
        var debugViewProperty = expr1.GetType().GetProperty("DebugView", 
            BindingFlags.Instance | BindingFlags.NonPublic);
        if (debugViewProperty != null)
        {
            var debugView = debugViewProperty.GetValue(expr1) as string;
            Console.WriteLine($"DebugView: {debugView}");
        }
        
        // Check if we can access the original source through other means
        var compilerGeneratedFields = expr1.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in compilerGeneratedFields)
        {
            Console.WriteLine($"Field: {field.Name} = {field.GetValue(expr1)}");
        }
        
        // Test with const string
        const string testString = "Hello World";
        Expression<Func<string>> expr2 = () => testString;
        Console.WriteLine($"\nString Expression ToString: {expr2.ToString()}");
        Console.WriteLine($"String Body ToString: {expr2.Body.ToString()}");
        
        // Test with non-const variable
        int regularValue = 42;
        Expression<Func<int>> expr3 = () => regularValue;
        Console.WriteLine($"\nNon-const Expression Type: {expr3.Body.GetType().Name}");
        Console.WriteLine($"Non-const Expression ToString: {expr3.ToString()}");
    }
}