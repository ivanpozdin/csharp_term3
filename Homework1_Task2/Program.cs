namespace Homework1_Task2;

public static class Program
{
    private static void Main()
    {
        Console.WriteLine("Enter string:");
        var text = Console.ReadLine();
        while (text == null) text = Console.ReadLine();
        var result = BurrowWheelerTransformation.DoBurrowsWheelerTransformation(text);
        Console.WriteLine(result.Key);
        Console.Write(result.Value);
        Console.WriteLine();
        Console.WriteLine(BurrowWheelerTransformation.ReverseBurrowsWheelerTransformation(result.Key, result.Value));
    }
}