namespace My_Lazy;

public class ThreadSafeLazy<T> : ILazy<T>
{
    private readonly object _lock = new();
    private readonly Func<T?> _supplier;
    private bool _isValueCalculated;
    private T? _value;

    public ThreadSafeLazy(Func<T> supplier)
    {
        _supplier = supplier;
    }

    /// <summary>
    ///     Thread safe function that does lazy calculation.
    /// </summary>
    /// <returns>Result of lazy calculation with corresponding type.</returns>
    public T? Get()
    {
        if (!_isValueCalculated)
            lock (_lock)
            {
                if (!_isValueCalculated)
                {
                    _value = _supplier();
                    _isValueCalculated = true;
                }
            }

        return _value;
    }
}