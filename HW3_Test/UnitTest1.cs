using Homework3;

namespace HW3_Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var pool = new MyThreadPool(4);
        var task1 = pool.Submit(() => 2 * 2);
        var task1Result = task1.Result;
        pool.Shutdown();
        Assert.Equal(4, task1Result);
    }
}