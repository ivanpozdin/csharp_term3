namespace Homework1_Task2;

public static class Program
{

    private static string GetNextStringRotation(string str)
    {
        var startElement = str[0];
        return str.Remove(0, 1) + startElement;
    }

    private static void InputStringToList(string str, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (String.CompareOrdinal(str, list[i]) == -1)
            {
                list.Insert(i, str);
                return;
            }
        }
        list.Add(str);
    }

    private static KeyValuePair<string, int> DoBurrowsWheelerTransformation(string str)
    {
        var listOfStringRotations = new List<string>();
        var newStr = string.Copy(str);
        for (int i = 0; i < str.Length; i++)
        {
            newStr = GetNextStringRotation(newStr);
            Console.WriteLine(newStr);
            InputStringToList(string.Copy(newStr), listOfStringRotations);
        }
        newStr = string.Empty;
        var strIndex = 0;
        foreach (var stringRotation in listOfStringRotations)
        {
            newStr += stringRotation[^1];
            if (String.CompareOrdinal(str, stringRotation) == 0)
            {
                strIndex = newStr.Length - 1;
            }
            
        }

        return new KeyValuePair<string, int>(newStr, strIndex);
    }

    
    
    private static string ReverseBurrowsWheelerTransformation(string strBWT, int index)
    {
        var listOfStringRotations = new List<string>();
        foreach (var letter in strBWT)
        {
            listOfStringRotations.Add(letter.ToString());
        }
        listOfStringRotations.Sort();

        while (listOfStringRotations[0].Length < strBWT.Length)
        {
            for (int i = 0; i < strBWT.Length; i++)
            {
                listOfStringRotations[i] = strBWT[i] + listOfStringRotations[i];
            }
            listOfStringRotations.Sort();
        }

        return listOfStringRotations[index];
    }
    
    private static void Main()
    {
        Console.WriteLine("Enter string:");
        var input = Console.ReadLine();
        while (input == null)
        {
            input = Console.ReadLine();
        }
        var result = DoBurrowsWheelerTransformation(input);
        Console.WriteLine(result.Key);
        Console.Write(result.Value);
        Console.WriteLine(ReverseBurrowsWheelerTransformation(result.Key, result.Value));
    }
}