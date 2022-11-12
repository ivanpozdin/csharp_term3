using System;
using System.Collections;
using Test_November12;

public class SamplesSortedList2
{
    public static void Main()
    {
        var Queue = new ThreadSafePriorityQueue<string, int>();
        Queue.Enqueue("hello", 1);
        Queue.Enqueue("hello", 1);
        Queue.Enqueue("lamp", 2);
        Queue.Enqueue("filter", 0);
        Queue.Enqueue("hello", 1);
        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine(Queue.Size());
            Console.WriteLine(Queue.Dequeue());
        }
    }

}