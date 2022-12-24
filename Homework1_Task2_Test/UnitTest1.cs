using Homework1_Task2;

namespace Homework1_Task2_Test;

public class UnitTest1
{
    private static readonly Random Random = new();

    private static string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    [Fact]
    public void Test1()
    {
        var inputString = "ABACABA";
        var expectedBwtString = "BCABAAA";
        var expectedSourceLineNumber = 2;
        var result = BurrowWheelerTransformation.DoBurrowsWheelerTransformation(inputString);
        Assert.Equal(result.Key, expectedBwtString);
        Assert.Equal(result.Value, expectedSourceLineNumber);
    }

    [Fact]
    public void Test2()
    {
        var expectedString = "ABACABA";
        var inputBwtString = "BCABAAA";
        var sourceLineNumber = 2;
        var result = BurrowWheelerTransformation.ReverseBurrowsWheelerTransformation(inputBwtString, sourceLineNumber);
        Assert.Equal(result, expectedString);
    }

    [Fact]
    public void Test3()
    {
        var lengthOfString = Random.Next(0, 1000);
        var inputString = GetRandomString(lengthOfString);
        var resultBwt = BurrowWheelerTransformation.DoBurrowsWheelerTransformation(inputString);
        var resultUnDoBwt =
            BurrowWheelerTransformation.ReverseBurrowsWheelerTransformation(resultBwt.Key, resultBwt.Value);
        Assert.Equal(inputString, resultUnDoBwt);
    }
}