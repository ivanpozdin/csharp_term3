using Homework2_Lazy;

namespace Homework2_Lazy_Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        OneThreadLazy<int> lazy = LazyFactory<int>.CreateOneThreadLazy(() => 100);
        Assert.Equal(100, lazy.Get());
    }
    
    [Fact]
    public void Test2()
    {
        var random = new Random();
        OneThreadLazy<int> lazy = LazyFactory<int>.CreateOneThreadLazy(() => random.Next(0, 10000));
        var firstCall = lazy.Get();
        var secondCall = lazy.Get();
        Assert.Equal(firstCall, secondCall);
    }
}