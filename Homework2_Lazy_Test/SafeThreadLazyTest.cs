using My_Lazy;

namespace Homework2_Lazy_Test;

public class SafeThreadLazyTest
{
    [Fact]
    public void TestSimpleCalculation()
    {
        var lazy = LazyFactory<int>.CreateThreadSafeLazy(() => 100);
        Assert.Equal(100, lazy.Get());
    }

    [Fact]
    public void TestTwoGetFromOneLazy()
    {
        var random = new Random();
        var lazy = LazyFactory<int>.CreateThreadSafeLazy(() => random.Next(0, 10000));
        var firstCall = lazy.Get();
        var secondCall = lazy.Get();
        Assert.Equal(firstCall, secondCall);
    }

    private int Fibonacci(int n)
    {
        if (n == 1) return 0;

        if (n == 2) return 1;

        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    [Fact]
    public void TestRaces()
    {
        var isTestSucceed = true;
        for (var i = 0; i < 10; i++)
        {
            var lazy = LazyFactory<int>.CreateThreadSafeLazy(() => Fibonacci(new Random().Next(35, 43)));
            var firstThreadValue = 0;
            var secondThreadValue = 1;
            var firstThread = new Thread(() =>
            {
                var value = lazy.Get();
                firstThreadValue = value;
            });
            var secondThread = new Thread(() =>
            {
                var value = lazy.Get();
                secondThreadValue = value;
            });
            firstThread.Start();
            secondThread.Start();
            firstThread.Join();
            secondThread.Join();
            isTestSucceed = firstThreadValue == secondThreadValue;
            if (!isTestSucceed)
                break;
        }

        Assert.True(isTestSucceed);
    }
}