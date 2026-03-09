using CCrossThrowIf;

namespace CCrossThrowIfTests;

public class GuardPatternTests
{
    #region Email Tests

    [Theory]
    [InlineData("user@example.com")]
    [InlineData("test.user@domain.co.uk")]
    [InlineData("admin+tag@company.org")]
    public void Email_WithValidEmail_ReturnsValue(string email)
    {
        var result = Guard.Pattern.Email(email);
        
        Assert.Equal(email, result);
    }

    [Fact]
    public void Email_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Guard.Pattern.Email(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("@example.com")]
    [InlineData("user@")]
    [InlineData("user@domain")]
    [InlineData("user @domain.com")]
    public void Email_WithInvalidEmail_ThrowsArgumentException(string email)
    {
        Assert.Throws<ArgumentException>(() =>
            Guard.Pattern.Email(email));
    }

    #endregion

    #region URL Tests

    [Theory]
    [InlineData("http://example.com")]
    [InlineData("https://www.github.com")]
    [InlineData("https://api.service.com/v1/endpoint")]
    public void Url_WithValidUrl_ReturnsValue(string url)
    {
        var result = Guard.Pattern.Url(url);
        
        Assert.Equal(url, result);
    }

    [Fact]
    public void Url_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Guard.Pattern.Url(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-url")]
    [InlineData("ftp://example.com")]
    [InlineData("//example.com")]
    [InlineData("http://")]
    public void Url_WithInvalidUrl_ThrowsArgumentException(string url)
    {
        Assert.Throws<ArgumentException>(() =>
            Guard.Pattern.Url(url));
    }

    #endregion

    #region Phone Number Tests

    [Theory]
    [InlineData("+1-555-123-4567")]
    [InlineData("555-123-4567")]
    [InlineData("(555) 123-4567")]
    [InlineData("5551234567")]
    [InlineData("+44 20 7123 4567")]
    public void PhoneNumber_WithValidPhone_ReturnsValue(string phone)
    {
        var result = Guard.Pattern.PhoneNumber(phone);
        
        Assert.Equal(phone, result);
    }

    [Fact]
    public void PhoneNumber_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Guard.Pattern.PhoneNumber(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("abc-def-ghij")]
    public void PhoneNumber_WithInvalidPhone_ThrowsArgumentException(string phone)
    {
        Assert.Throws<ArgumentException>(() =>
            Guard.Pattern.PhoneNumber(phone));
    }

    #endregion

    #region GUID Tests

    [Theory]
    [InlineData("550e8400-e29b-41d4-a716-446655440000")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("{550e8400-e29b-41d4-a716-446655440000}")]
    [InlineData("550e8400e29b41d4a716446655440000")]
    public void Guid_WithValidGuid_ReturnsValue(string guid)
    {
        var result = Guard.Pattern.Guid(guid);
        
        Assert.Equal(guid, result);
    }

    [Fact]
    public void Guid_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            Guard.Pattern.Guid(null));
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-a-guid")]
    [InlineData("550e8400-e29b-41d4-a716")]
    public void Guid_WithInvalidGuid_ThrowsArgumentException(string guid)
    {
        Assert.Throws<ArgumentException>(() =>
            Guard.Pattern.Guid(guid));
    }

    #endregion

    #region Custom Pattern Tests

    [Fact]
    public void Matches_WithMatchingPattern_ReturnsValue()
    {
        const string value = "ABC123";
        const string pattern = @"^[A-Z]{3}\d{3}$";
        
        var result = Guard.Pattern.Matches(value, pattern);
        
        Assert.Equal(value, result);
    }

    [Fact]
    public void Matches_WithNonMatchingPattern_ThrowsArgumentException()
    {
        const string value = "ABC";
        const string pattern = @"^[A-Z]{3}\d{3}$";
        
        Assert.Throws<ArgumentException>(() => 
            Guard.Pattern.Matches(value, pattern));
    }

    [Fact]
    public void Matches_WithCustomMessage_UsesCustomMessage()
    {
        const string value = "invalid";
        const string pattern = @"^\d+$";
        const string message = "Must be numeric";
        
        var exception = Assert.Throws<ArgumentException>(() => 
            Guard.Pattern.Matches(value, pattern, message: message));
        
        Assert.Contains(message, exception.Message);
    }

    #endregion

    #region Length Tests

    [Fact]
    public void Length_WithValidLength_ReturnsValue()
    {
        const string value = "hello";
        
        var result = Guard.Pattern.Length(value, 3, 10);
        
        Assert.Equal(value, result);
    }

    [Theory]
    [InlineData("hi", 3, 10)]      // Too short
    [InlineData("hello world!", 3, 10)] // Too long
    public void Length_WithInvalidLength_ThrowsArgumentException(string value, int min, int max)
    {
        Assert.Throws<ArgumentException>(() => 
            Guard.Pattern.Length(value, min, max));
    }

    #endregion
}