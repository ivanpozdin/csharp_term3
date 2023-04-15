// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

int[] GenerateArray(int size)
{
    var array = new int[size];
    var random = new Random();
    for (var i = 0; i < size; i++) array[i] = random.Next(0, 1000);
    return array;
}


const int threadsNumber = 8;

for (var length = 1000; length <= 10000000; length *= 10)
{
    var arrayForSingleThreadSort = GenerateArray(length);
    var arrayForMultiThreadSort = new int[length];
    arrayForSingleThreadSort.CopyTo(arrayForMultiThreadSort, 0);

    var singleThreadStopWatch = new Stopwatch();
    singleThreadStopWatch.Start();
    QuickSort.QuickSort.SingleThreadSort(arrayForSingleThreadSort, 0, arrayForSingleThreadSort.Length - 1);
    singleThreadStopWatch.Stop();


    var multiThreadStopwatch = new Stopwatch();
    multiThreadStopwatch.Start();
    QuickSort.QuickSort.MultiThreadSort(arrayForSingleThreadSort, 0, arrayForSingleThreadSort.Length - 1,
        threadsNumber);
    multiThreadStopwatch.Stop();

    Console.WriteLine(
        $"Single thread quick sort work time for {length} elements random array: {singleThreadStopWatch.ElapsedMilliseconds } ms");
    Console.WriteLine(
        $"Multi thread quick sort work time: for {length} elements random array with {threadsNumber} threads: {multiThreadStopwatch.ElapsedMilliseconds} ms");
}