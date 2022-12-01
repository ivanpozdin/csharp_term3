namespace Test_November12;

public class ThreadSafePriorityQueue<TValue, TPriority> where TPriority : IComparable
{
    private readonly SortedList<TPriority, Queue<TValue>> _priorityQueue = new();

    /**
     * Amount of elements in the queue.
     */
    public int Size;

    /**
     * Method enqueues value with certain priority to the queue.
     */
    public void Enqueue(TValue value, TPriority priority)
    {
        lock (_priorityQueue)
        {
            if (_priorityQueue.ContainsKey(priority))
            {
                _priorityQueue[priority].Enqueue(value);
            }
            else
            {
                var tmpQueue = new Queue<TValue>();
                tmpQueue.Enqueue(value);
                _priorityQueue.Add(priority, tmpQueue);
            }

            Size++;
            if (Size == 1) Monitor.PulseAll(_priorityQueue);
        }
    }

    /**
     * Method dequeues value with maximum priority.
     */
    public TValue Dequeue()
    {
        lock (_priorityQueue)
        {
            while (Size == 0) Monitor.Wait(_priorityQueue);

            var indexOfMaxPriority = _priorityQueue.Count - 1;
            var returnValue = _priorityQueue.Values[indexOfMaxPriority].Dequeue();
            if (_priorityQueue.Values[indexOfMaxPriority].Count == 0)
                _priorityQueue.Remove(_priorityQueue.Keys[indexOfMaxPriority]);
            Size--;
            return returnValue;
        }
    }
}