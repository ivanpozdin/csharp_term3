namespace My_Lazy;

public class OneThreadLazy<T> : ILazy<T>
{
    private readonly Func<T?> _supplier;
    private bool _isValueCalculated;
    private T? _value;

    public OneThreadLazy(Func<T?> supplier)
    {
        _supplier = supplier;
    }

    /// <summary>
    ///     Function does lazy calculation. Can be used only by one thread!
    /// </summary>
    /// <returns>Result of lazy calculation with corresponding type.</returns>
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