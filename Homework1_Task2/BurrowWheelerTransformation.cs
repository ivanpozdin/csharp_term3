namespace Homework1_Task2;

public static class BurrowWheelerTransformation
{
    /// <summary>
    /// Apply Burrows-Wheeler Transformation to given string.
    /// </summary>
    /// <param name="text"> The text to be transformed. </param>
    /// <returns>Pair of the transformed BWT string and
    /// the row that the original string had in the sorted list of cyclic shifts.</returns>
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

    /// <summary>
    /// Reverse Burrows-Wheeler Transformation of the given text.
    /// </summary>
    /// <param name="text">Given text.</param>
    /// <param name="index">Row of the original string in the sorted list of cyclic shifts.</param>
    /// <returns>Original string.</returns>
    public static string ReverseBurrowsWheelerTransformation(string text, int index)
    {
        var sortedText = text.OrderBy(c => c).ToArray();

        var occurrences = new Dictionary<char, int>();
        for (var i = 0; i < text.Length; i++)
            if (i == 0 || sortedText[i] != sortedText[i - 1])
                occurrences.Add(sortedText[i], i);

        var transitions = text.Select(c => occurrences[c]++).ToArray();
        var originalText = "";
        for (var i = index; originalText.Length != text.Length; i = transitions[i])
            originalText = text[i] + originalText;

        return originalText;
    }
}