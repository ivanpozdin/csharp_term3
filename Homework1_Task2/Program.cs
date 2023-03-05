namespace Homework1_Task2;

public static class Program
{
    private static void Main()
    {
        // Console.WriteLine("Enter string:");
        // var input = Console.ReadLine();
        // while (input == null) input = Console.ReadLine();
        // var result = BurrowWheelerTransformation.DoBurrowsWheelerTransformation(input);
        // Console.WriteLine(result.Key);
        // Console.Write(result.Value);
        // Console.WriteLine();
        // Console.WriteLine(BurrowWheelerTransformation.ReverseBurrowsWheelerTransformation(result.Key, result.Value));
        
        var inputString = "ABACABA";
        var expectedBwtString = "BCABAAA";
        var expectedSourceLineNumber = 2;
        var result = BurrowWheelerTransformation.DoBurrowsWheelerTransformation(inputString);
        Console.WriteLine(result.Key);
        Console.WriteLine(result.Value);
    }
}