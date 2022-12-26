namespace Homework2_Lazy;

public class LazyFactory<T>
{
    public static OneThreadLazy<T> CreateOneThreadLazy(Func<T?> func)
    {
        return new OneThreadLazy<T>(func);
    }

    public static ThreadSafeLazy<T> CreateThreadSafeLazy(Func<T> func)
    {
        return new ThreadSafeLazy<T>(func);
    }
}