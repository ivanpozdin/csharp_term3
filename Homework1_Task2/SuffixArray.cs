namespace Homework1_Task2;

public static class SuffixArray
{
    public static int[] CreateSuffixArray(string text)
    {
        var n = text.Length;
        var (positions, arrayForFastCompare) = GetStartingArraysPositionsAndArrayForFastCompare(text);

        for (var h = 0; 1 << h < n; ++h)
        {
            var pairs = new (int, int)[n];
            var newPositions = Enumerable.Range(0, text.Length).ToArray();
            for (var i = 0; i < n; i++) pairs[i] = (arrayForFastCompare[i], arrayForFastCompare[(i + (1 << h)) % n]);

            Array.Sort(pairs, newPositions);
            positions = newPositions;
            var rating = 0;
            arrayForFastCompare[positions[0]] = rating;

            for (var i = 1; i < n; i++)
            {
                if (pairs[i] != pairs[i - 1]) rating++;
                arrayForFastCompare[positions[i]] = rating;
            }

            if (rating == n - 1)
                break;
        }

        return positions;
    }

    private static (int[], int[]) GetStartingArraysPositionsAndArrayForFastCompare(string text)
    {
        var chars = text.ToCharArray();
        var positions = Enumerable.Range(0, text.Length).ToArray();
        Array.Sort(chars, positions);
        var arrayForFastCompare = new int[text.Length];

        var rating = 0;
        arrayForFastCompare[positions[0]] = rating;
        for (var i = 1; i < text.Length; i++)
        {
            if (chars[i] != chars[i - 1]) rating++;
            arrayForFastCompare[positions[i]] = rating;
        }

        return (positions, arrayForFastCompare);
    }
}