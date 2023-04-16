namespace QuickSort_Test;

public class Tests
{
    private int[] GenerateArray(int size)
    {
        var array = new int[size];
        var random = new Random(size);
        for (var i = 0; i < size; i++) array[i] = random.Next(0, 1000);
        return array;
    }

    [SetUp]
    public void Setup()
    {
    }


    [Test]
    [TestCase(10)]
    [TestCase(101)]
    [TestCase(1012)]
    [TestCase(100000)]
    [TestCase(10124314)]
    [TestCase(1)]
    public void SingleThreadSortTest(int size)
    {
        var array = GenerateArray(size);
        var copiedArray = new int[size];
        array.CopyTo(copiedArray, 0);
        Array.Sort(array);
        QuickSort.QuickSort.SingleThreadSort(copiedArray, 0, size - 1);
        Assert.That(array, Is.EquivalentTo(copiedArray));
    }

    [Test]
    [TestCase(10)]
    [TestCase(101)]
    [TestCase(1012)]
    [TestCase(100000)]
    [TestCase(10124314)]
    [TestCase(1)]
    public void MultiThreadSortTest(int size)
    {
        var array = GenerateArray(size);
        var copiedArray = new int[size];
        array.CopyTo(copiedArray, 0);
        Array.Sort(array);
        QuickSort.QuickSort.SingleThreadSort(copiedArray, 0, size - 1);
        Assert.That(array, Is.EquivalentTo(copiedArray));
    }

    [Test]
    [TestCase(100, 200, 200)]
    [TestCase(100, -200, 99)]
    [TestCase(100, -1, 100)]
    [TestCase(100, 99, 0)]
    public void SingleThreadSortOutOfRangeExceptionTest(int size, int left, int right)
    {
        var array = GenerateArray(size);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            QuickSort.QuickSort.SingleThreadSort(array, left, right));
    }

    [Test]
    [TestCase(100, 200, 200)]
    [TestCase(100, -200, 99)]
    [TestCase(100, -1, 100)]
    [TestCase(100, 99, 0)]
    public void MultiThreadSortOutOfRangeExceptionTest(int size, int left, int right)
    {
        var array = GenerateArray(size);
        Assert.Throws<ArgumentOutOfRangeException>(delegate
        {
            QuickSort.QuickSort.MultiThreadSort(array, left, right, 8);
        });
    }
}