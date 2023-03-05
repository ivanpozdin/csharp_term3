namespace Homework1_Task2;

public static class SuffixArray
{
    public static int[] CreateSuffixArray(string s)
    {
        var n = s.Length;
        var (positions, arrayForFastCompare) = GetStartingArraysPositionsAndArrayForFastCompare(s);

        for (var h = 0; 1 << h < n; ++h)
        {
            var pairs = new (int, int)[n];
            var newPositions = new int[n];
            for (var i = 0; i < n; i++)
            {
                pairs[i] = (arrayForFastCompare[i], arrayForFastCompare[(i + (1 << h)) % n]);
                newPositions[i] = i;
            }

            Array.Sort(pairs, newPositions);
            positions = newPositions;
            var rating = 0;
            arrayForFastCompare[positions[0]] = rating;

            for (var i = 1; i < n; i++)
            {
                if (pairs[i] != pairs[i - 1]) rating++;
                arrayForFastCompare[positions[i]] = rating;
            }
        }

        return positions;
    }

    private static (int[], int[]) GetStartingArraysPositionsAndArrayForFastCompare(string text)
    {
        var n = text.Length;
        var chars = text.ToCharArray();
        var positions = new int[n];
        for (var i = 0; i < n; i++) positions[i] = i;
        Array.Sort(chars, positions);
        var arrayForFastCompare = new int[n];

        var rating = 0;
        arrayForFastCompare[positions[0]] = rating;
        for (var i = 1; i < n; i++)
        {
            if (chars[i] != chars[i - 1]) rating++;
            arrayForFastCompare[positions[i]] = rating;
        }

        return (positions, arrayForFastCompare);
    }
}