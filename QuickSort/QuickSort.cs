namespace QuickSort;

public static class QuickSort
{
    private static int Partition(int[] array, int left, int right)
    {
        var middle = array[(left + right) / 2];
        var (i, j) = (left, right);
        while (i <= j)
        {
            while (array[i] < middle) i++;
            while (array[j] > middle) j--;
            if (i >= j) break;
            (array[i], array[j]) = (array[j], array[i]);
            i++;
            j--;
        }

        return j;
    }


    private static void InsertSort(int[] array, int left, int right)
    {
        for (var i = left + 1; i <= right; i++)
        {
            var j = i - 1;
            while (j >= left && array[j] > array[j + 1])
            {
                (array[j], array[j + 1]) = (array[j + 1], array[j]);
                j--;
            }
        }
    }

    /// <summary>
    ///     Sorts given array using quick sort.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="left">
    ///     Left sorting border must be <= than right and >=0.">
    /// </param>
    /// <param name="right">Right sorting border must be less than the length of array.</param>
    public static void SingleThreadSort(int[] array, int left, int right)
    {
        if (left > right || right > array.Length || right < 0 || left > array.Length || left < 0)
            throw new ArgumentOutOfRangeException();

        if (right - left <= 20)
        {
            InsertSort(array, left, right);
        }
        else
        {
            var q = Partition(array, left, right);
            SingleThreadSort(array, left, q);
            SingleThreadSort(array, q + 1, right);
        }
    }

    /// <summary>
    ///     Sorts given array using multi-thread quick sort.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="left">
    ///     Left sorting border must be <= than right and >=0.">
    /// </param>
    /// <param name="right">Right sorting border must be less than the length of array.</param>
    /// <param name="freeThreadsNumber">Number of threads that algorithm uses. Should be even number.</param>
    public static void MultiThreadSort(int[] array, int left, int right, int freeThreadsNumber)
    {
        if (left > right || right > array.Length || right < 0 || left > array.Length || left < 0)
            throw new ArgumentOutOfRangeException();

        if (right - left <= 20)
        {
            InsertSort(array, left, right);
        }
        else if (freeThreadsNumber <= 1)
        {
            SingleThreadSort(array, left, right);
        }
        else
        {
            var q = Partition(array, left, right);
            var leftThread = new Thread(() => { MultiThreadSort(array, left, q, freeThreadsNumber - 2); });
            var rightThread = new Thread(() => { MultiThreadSort(array, q + 1, right, freeThreadsNumber - 2); });
            leftThread.Start();
            rightThread.Start();
            leftThread.Join();
            rightThread.Join();
        }
    }
}