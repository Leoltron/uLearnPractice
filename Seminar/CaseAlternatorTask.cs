using System;
using System.Collections.Generic;

public class CaseAlternatorTask
{
    //Вызывать будут этот метод
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        return result;
    }

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
        if (startIndex == word.Length)
        {
            result.Add(new string(word));
        }
        else
        {
            if (char.ToUpper(word[startIndex]) != char.ToLower(word[startIndex]))
            {
                word[startIndex] = char.ToLower(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
                word[startIndex] = char.ToUpper(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
            }
            else
            {
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
}

