namespace My_Lazy;

public interface ILazy<out T>
{
    /// <summary>
    ///     Function does lazy calculation.
    /// </summary>
    /// <returns>Result of lazy calculation with corresponding type.</returns>
    T? Get();
}