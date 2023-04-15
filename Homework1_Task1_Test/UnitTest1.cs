using Homework1_Task1;

namespace Homework1_Task1_Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var array1 = new int[100];
        var array2 = new int[100];
        var random = new Random();
        for (var i = 0; i < 100; i++)
        {
            var randomNumber = random.Next(1000);
            array1[i] = randomNumber;
            array2[i] = randomNumber;
        }

        Program.SortArray(array1);
        Array.Sort(array2);
        Assert.True(array1.SequenceEqual(array2));
    }
}