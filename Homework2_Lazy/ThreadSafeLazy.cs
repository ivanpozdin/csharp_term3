namespace Homework2_Lazy;

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