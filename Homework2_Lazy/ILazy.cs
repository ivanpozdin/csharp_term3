namespace Homework2_Lazy;

public interface ILazy<out T>
{
    T? Get();
}