namespace Homework1_Task1;

public static class Program
{
    public static void SortArray(int[] array)
    {
        for (var i = 0; i < array.Length; i++)
        for (var j = i; j > 0 && array[j] < array[j - 1]; j--)
            (array[j], array[j - 1]) = (array[j - 1], array[j]);
    }

    private static void Main()
    {
        Console.Write("Write size of array: ");

        var size = Convert.ToInt32(Console.ReadLine());
        while (size < 1)
        {
            Console.Write("Size of array must be positive, try again: ");
            size = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Write elements of array one in a line: ");

        var array = new int[size];
        for (var i = 0;
             i < size;
             i++) array[i] = Convert.ToInt32(Console.ReadLine());
        SortArray(array);
        Console.WriteLine("Sorted array:");
        foreach (var t in array) Console.WriteLine(t);
    }
}