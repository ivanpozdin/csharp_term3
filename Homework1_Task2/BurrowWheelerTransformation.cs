namespace Homework1_Task2;

public static class BurrowWheelerTransformation
{
    public static KeyValuePair<string, int> DoBurrowsWheelerTransformation(string text)
    {
        var n = text.Length;
        var suffixArray = SuffixArray.CreateSuffixArray(text);
        var bwtString = "";
        var positionOfGivenString = 0;
        for (var i = 0; i < n; i++)
        {
            bwtString += text[(suffixArray[i] + n - 1) % n];
            if (suffixArray[i] == 0) positionOfGivenString = i;
        }

        return new KeyValuePair<string, int>(bwtString, positionOfGivenString);
    }

    public static string ReverseBurrowsWheelerTransformation(string textBWT, int index)
    {
        if (textBWT == string.Empty) return string.Empty;
        var listOfStringRotations = new List<string>();
        foreach (var letter in textBWT) listOfStringRotations.Add(letter.ToString());
        listOfStringRotations.Sort();

        while (listOfStringRotations[0].Length < textBWT.Length)
        {
            for (var i = 0; i < textBWT.Length; i++) listOfStringRotations[i] = textBWT[i] + listOfStringRotations[i];
            listOfStringRotations.Sort();
        }

        return listOfStringRotations[index];
    }
    
}