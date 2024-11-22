namespace DiffChecker.Tests;

public class UnitTest1()
{
    [Fact]
    public void RunTest()
    {
        DiffChecker.Run("C:\\temp");
        Assert.True(true);
    }
}