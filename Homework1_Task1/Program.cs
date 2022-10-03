// See https://aka.ms/new-console-template for more information

static void sortArray(int[] array)
{
    for (var i = 0; i < array.Length; i++)
    for (var j = i; j > 0 && array[j] < array[j - 1]; j--)
        (array[j], array[j - 1]) = (array[j - 1], array[j]);
}

Console.Write("Write size of array: ");
var size = Convert.ToInt32(Console.ReadLine());
while (size < 1)
{
    Console.Write("Size of array must be positive, try again: ");
    size = Convert.ToInt32(Console.ReadLine());
}

Console.WriteLine("Write elements of array one in a line: ");
var array = new int[size];
for (var i = 0; i < size; i++) array[i] = Convert.ToInt32(Console.ReadLine());
sortArray(array);
Console.WriteLine("Sorted array:");
foreach (var t in array) Console.WriteLine(t);