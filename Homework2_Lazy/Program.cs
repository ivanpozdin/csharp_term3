// See https://aka.ms/new-console-template for more information
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
Console.WriteLine(Fibonacci(40));