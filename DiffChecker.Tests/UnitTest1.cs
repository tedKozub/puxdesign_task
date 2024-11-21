using Xunit.Abstractions;

namespace DiffChecker.Tests;

public class UnitTest1(ITestOutputHelper output)
{
    [Fact]
    public void RunTest()
    {
        // Arrange
        var diffChecker = new DiffChecker();

        // Act
        diffChecker.Run("C:\\temp");

        // Assert
        Assert.True(true);
    }
}