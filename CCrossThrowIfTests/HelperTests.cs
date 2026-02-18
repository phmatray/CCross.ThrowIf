using System.Linq.Expressions;
using CCrossThrowIf;

namespace CCrossThrowIfTests;

public class HelperTests
{
    #region GetMetadata Tests

    [Fact]
    public void GetMetadata_WithValidExpression_ReturnsMetadata()
    {
        int testValue = 42;
        Expression<Func<int>> expression = () => testValue;

        var metadata = expression.GetMetadata();

        Assert.NotNull(metadata);
        Assert.Equal("testValue", metadata.Name);
        Assert.Equal(42, metadata.Value);
    }

    [Fact]
    public void GetMetadata_WithStringExpression_ReturnsMetadata()
    {
        const string testValue = "test string";
        Expression<Func<string>> expression = () => testValue;

        var metadata = expression.GetMetadata();

        Assert.NotNull(metadata);
        Assert.Equal("testValue", metadata.Name);
        Assert.Equal("test string", metadata.Value);
    }

    #endregion

    #region CreateException Tests

    [Fact]
    public void CreateException_WithExceptionType_CreatesExceptionWithMessage()
    {
        const string message = "Test exception message";

        var exception = Helper.CreateException<Exception>(message);

        Assert.NotNull(exception);
        Assert.IsType<Exception>(exception);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void CreateException_WithArgumentNullException_CreatesProperException()
    {
        const string message = "Parameter cannot be null";
        const string paramName = "testParam";

        var exception = Helper.CreateException<ArgumentNullException>(message, paramName);

        Assert.NotNull(exception);
        Assert.IsType<ArgumentNullException>(exception);
        Assert.Equal(paramName, exception.ParamName);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void CreateException_WithArgumentOutOfRangeException_CreatesProperException()
    {
        const string message = "Value out of range";
        const string paramName = "testParam";
        const int actualValue = 42;

        var exception = Helper.CreateException<ArgumentOutOfRangeException>(message, paramName, actualValue);

        Assert.NotNull(exception);
        Assert.IsType<ArgumentOutOfRangeException>(exception);
        Assert.Equal(paramName, exception.ParamName);
        Assert.Equal(actualValue, exception.ActualValue);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void CreateException_WithInvalidOperationException_CreatesProperException()
    {
        const string message = "Invalid operation";

        var exception = Helper.CreateException<InvalidOperationException>(message);

        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void CreateException_WithCustomExceptionNoParameters_CreatesEmptyException()
    {
        var exception = Helper.CreateException<CustomTestException>("message");

        Assert.NotNull(exception);
        Assert.IsType<CustomTestException>(exception);
    }

    [Fact]
    public void CreateException_WithArgumentException_CreatesProperException()
    {
        const string message = "Invalid argument";
        const string paramName = "testParam";

        var exception = Helper.CreateException<ArgumentException>(message, paramName);

        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
        Assert.Contains(message, exception.Message);
    }

    #endregion

    #region CreateArgsException Private Method Tests (via CreateException)

    [Fact]
    public void CreateException_ForException_UsesCorrectConstructorArgs()
    {
        const string message = "Test message";
        
        var exception = Helper.CreateException<Exception>(message);
        
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void CreateException_ForArgumentNullException_UsesCorrectConstructorArgs()
    {
        const string message = "Test message";
        const string paramName = "param";
        
        var exception = Helper.CreateException<ArgumentNullException>(message, paramName);
        
        Assert.Equal(paramName, exception.ParamName);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void CreateException_ForArgumentOutOfRangeException_UsesCorrectConstructorArgs()
    {
        const string message = "Test message";
        const string paramName = "param";
        object actualValue = 123;
        
        var exception = Helper.CreateException<ArgumentOutOfRangeException>(message, paramName, actualValue);
        
        Assert.Equal(paramName, exception.ParamName);
        Assert.Equal(actualValue, exception.ActualValue);
        Assert.Contains(message, exception.Message);
    }

    [Fact]
    public void CreateException_ForUnknownExceptionType_UsesEmptyConstructorArgs()
    {
        const string message = "Test message";
        
        var exception = Helper.CreateException<CustomTestException>(message);
        
        Assert.NotNull(exception);
        Assert.IsType<CustomTestException>(exception);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void CreateException_WithNullMessage_CreatesExceptionWithDefaultMessage()
    {
        string? message = null;

        var exception = Helper.CreateException<Exception>(message);

        Assert.NotNull(exception);
        // When Exception is created with default constructor, it has a default message
        Assert.NotNull(exception.Message);
    }

    [Fact]
    public void CreateException_WithEmptyMessage_CreatesExceptionWithEmptyMessage()
    {
        const string message = "";

        var exception = Helper.CreateException<Exception>(message);

        Assert.NotNull(exception);
        Assert.Equal("", exception.Message);
    }

    #endregion
}

public class CustomTestException : Exception
{
    public CustomTestException()
    {
    }
    
    public CustomTestException(string message) : base(message)
    {
    }
}