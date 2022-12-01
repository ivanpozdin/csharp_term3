namespace Test_November12;

public static class SamplesSortedList2
{
    public static void Main()
    {
        var queue = new ThreadSafePriorityQueue<string, int>();
        queue.Enqueue("hello", 1);
        queue.Enqueue("hello", 1);
        queue.Enqueue("lamp", 2);
        queue.Enqueue("filter", 0);
        queue.Enqueue("hello", 1);
        for (var i = 0; i < 6; i++)
        {
            Console.WriteLine(queue.Size);
            Console.WriteLine(queue.Dequeue());
        }
    }
}