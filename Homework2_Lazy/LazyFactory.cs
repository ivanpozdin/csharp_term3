namespace My_Lazy;

public abstract class LazyFactory<T>
{
    /// <summary>
    ///     Creates simple lazy calculation.
    /// </summary>
    /// <param name="func">
    ///     Method with no arguments and result type T?.
    /// </param>
    /// <returns>
    ///     Lazy calculation.
    /// </returns>
    public static OneThreadLazy<T> CreateOneThreadLazy(Func<T?> func)
    {
        return new OneThreadLazy<T>(func);
    }

    /// <summary>
    ///     Creates thread safe lazy calculation.
    /// </summary>
    /// <param name="func">
    ///     Method with no arguments and result type T?.
    /// </param>
    /// <returns>
    ///     Thead safe lazy calculation.
    /// </returns>
    public static ThreadSafeLazy<T> CreateThreadSafeLazy(Func<T> func)
    {
        return new ThreadSafeLazy<T>(func);
    }
}