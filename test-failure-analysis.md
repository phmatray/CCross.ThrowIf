# CCross.ThrowIf Test Failure Analysis

## Summary
- **Total Failed Tests**: 68 out of 270 tests
- **Total Passed Tests**: 202 
- **Test Duration**: 157ms

## Test Failure Breakdown by Class

| Test Class | Failed Tests |
|------------|--------------|
| ThrowIfArgumentTests | 45 |
| ThrowIfGenericTests | 13 |
| GuardPatternTests | 5 |
| MetadataTests | 4 |
| HelperTests | 1 |

## Primary Failure Patterns

### 1. Expression Parsing Error (61 failures)
**Error**: `System.ArgumentException : Invalid argument (Parameter 'expression')`

**Root Cause**: The `Metadata.GetMemberName()` method expects a `MemberExpression` but receives a `ConstantExpression` when using `const` variables in lambda expressions.

**Location**: `/Users/phmatray/Repositories/github-phm/CCross.ThrowIf/CCrossThrowIf/Metadata.cs:24`

```csharp
private static string GetMemberName<T>(Expression<Func<T>> expression)
{
    var body = expression.Body as MemberExpression;
    if (body == null)
        throw new ArgumentException("Invalid argument", nameof(expression));
    
    var argumentName = body.Member.Name;
    return argumentName;
}
```

**Affected Test Pattern**:
```csharp
const string testValue = "test string";  // const causes ConstantExpression
Expression<Func<string>> expression = () => testValue;
```

### 2. Exception Type Mismatch (26 failures)
**Error**: `Assert.Throws() Failure: Exception type was not an exact match`

**Common Patterns**:
1. **Expected ArgumentException but got ArgumentNullException**
   - Tests expecting `ArgumentException` for null values, but code throws `ArgumentNullException`
   - Affects: GuardPatternTests (Email, Url, Guid validation with null values)

2. **Expected InvalidOperationException but got ArgumentException**
   - Tests expecting custom exception types but getting ArgumentException due to expression parsing failure
   - Affects: ThrowIfGenericTests

## Specific Test Examples

### GuardPatternTests Failures
```
Failed: Email_WithInvalidEmail_ThrowsArgumentException(email: null)
Expected: typeof(System.ArgumentException)
Actual:   typeof(System.ArgumentNullException)
```

### ThrowIfArgumentTests Failures
```
Failed: IsEqualTo_WhenValueIsNotEqualToTestValue_DoesNotThrow
Error: System.ArgumentException : Invalid argument (Parameter 'expression')
```

## Recommended Fixes

### Fix 1: Update Metadata.GetMemberName to Handle ConstantExpression
The method needs to handle cases where the expression body is a `ConstantExpression` (when using `const` variables) or other expression types.

### Fix 2: Update Test Expectations
Some tests expect `ArgumentException` when the actual behavior throws more specific exceptions like `ArgumentNullException`. These test expectations should be updated to match the actual behavior.

### Fix 3: Use Non-Const Variables in Tests
As a workaround, tests could use regular variables instead of `const` to ensure `MemberExpression` is generated:
```csharp
string testValue = "test string";  // not const
Expression<Func<string>> expression = () => testValue;
```

## Impact Analysis
- 61 out of 68 failures (90%) are due to the expression parsing issue
- The remaining 7 failures are due to exception type mismatches
- All failures are in the test code expectations or the expression metadata extraction logic
- The core validation logic appears to be working correctly