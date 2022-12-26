namespace Homework2_Lazy;

public class OneThreadLazy<T> : ILazy<T>
{
    private readonly Func<T?> _supplier;
    private bool _isValueCalculated;
    private T? _value;

    public OneThreadLazy(Func<T?> supplier)
    {
        _supplier = supplier;
    }

    public T? Get()
    {
        if (!_isValueCalculated)
        {
            _value = _supplier();
            _isValueCalculated = true;
        }

        return _value;
    }
}