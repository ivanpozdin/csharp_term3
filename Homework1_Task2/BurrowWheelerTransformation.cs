namespace Homework1_Task2;

public class BurrowWheelerTransformation
{
    private static string GetNextStringRotation(string str)
    {
        var startElement = str[0];
        return str.Remove(0, 1) + startElement;
    }

    public static KeyValuePair<string, int> DoBurrowsWheelerTransformation(string str)
    {
        var listOfStringRotations = new List<string>();
        var newStr = string.Copy(str);
        for (var i = 0; i < str.Length; i++)
        {
            newStr = GetNextStringRotation(newStr);
            listOfStringRotations.Add(newStr);
        }

        listOfStringRotations = listOfStringRotations.OrderBy(q => q).ToList();
        newStr = string.Empty;
        var strIndex = 0;
        foreach (var stringRotation in listOfStringRotations)
        {
            newStr += stringRotation[^1];
            if (string.CompareOrdinal(str, stringRotation) == 0) strIndex = newStr.Length - 1;
        }

        return new KeyValuePair<string, int>(newStr, strIndex);
    }

    public static string ReverseBurrowsWheelerTransformation(string strBWT, int index)
    {
        if (strBWT == string.Empty) return string.Empty;
        var listOfStringRotations = new List<string>();
        foreach (var letter in strBWT) listOfStringRotations.Add(letter.ToString());
        listOfStringRotations.Sort();

        while (listOfStringRotations[0].Length < strBWT.Length)
        {
            for (var i = 0; i < strBWT.Length; i++) listOfStringRotations[i] = strBWT[i] + listOfStringRotations[i];
            listOfStringRotations.Sort();
        }

        return listOfStringRotations[index];
    }
}