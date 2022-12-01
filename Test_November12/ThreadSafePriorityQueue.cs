namespace Test_November12;

public class ThreadSafePriorityQueue<TValue, TPriority> where TPriority : IComparable
{
    private readonly SortedList<TPriority, Queue<TValue>> _priorityQueue = new SortedList<TPriority, Queue<TValue>>();
    private int _size;
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

            _size++;
            if (_size == 1)
            {
                Monitor.PulseAll(_priorityQueue);
            }
        }
    }
    public TValue Dequeue()
    {
        lock (_priorityQueue)
        {
            while (_size == 0)
            {
                Monitor.Wait(_priorityQueue);
            }

            var returnValue = _priorityQueue.Values[0].Dequeue();
            if (_priorityQueue.Values[0].Count == 0)
            {
                _priorityQueue.Remove(_priorityQueue.Keys[0]);
            }

            _size--;
            return returnValue;
        }
    }

    public int Size()
    {
        return _size;
    }
}