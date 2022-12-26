using Homework2_Lazy;
namespace Homework2_Lazy_Test;

public class SafeThreadLazyTest
{
    [Fact]
    public void Test1()
    {
        ThreadSafeLazy<int> lazy = new ThreadSafeLazy<int>(() => 100);
        Assert.Equal(100, lazy.Get());
    }
    
    [Fact]
    public void Test2()
    {
        var random = new Random();
        ThreadSafeLazy<int> lazy = new ThreadSafeLazy<int>(() => random.Next(0, 10000));
        var firstCall = lazy.Get();
        var secondCall = lazy.Get();
        Assert.Equal(firstCall, secondCall);
    }

    int Fibonacci(int n)
    {
        if (n == 1)
        {
            return 0;
        }

        if (n == 2)
        {
            return 1;
        }

        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }
    [Fact]
    public void Test3()
    {
        var lazy = LazyFactory<int>.CreateThreadSafeLazy(() => Fibonacci(42));
        int firstThreadValue = 0;
        int secondThreadValue = 1;
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
        Assert.Equal(firstThreadValue, secondThreadValue);
    }
    
}