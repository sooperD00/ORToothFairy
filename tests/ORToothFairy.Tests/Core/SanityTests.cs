using Xunit;

namespace ORToothFairy.Tests.Core;

public class SanityTests
{
    [Fact]
    public void Math_Should_Work()
    {
        // Arrange
        int a = 2;
        int b = 2;

        // Act
        int result = a + b;

        // Assert
        Assert.Equal(4, result);
    }
}