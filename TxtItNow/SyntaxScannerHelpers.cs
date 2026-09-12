namespace TxtItNow;

internal static class SyntaxScannerHelpers
{
    public static bool IsIdentifierStart(char character)
    {
        return character == '_' || char.IsLetter(character);
    }

    public static bool IsIdentifierPart(char character)
    {
        return character == '_' || char.IsLetterOrDigit(character);
    }

    public static bool IsAtLineStartAfterWhitespace(string text, int index)
    {
        for (int previousIndex = index - 1; previousIndex >= 0; previousIndex--)
        {
            char character = text[previousIndex];

            if (character is '\r' or '\n')
            {
                return true;
            }

            if (!char.IsWhiteSpace(character))
            {
                return false;
            }
        }

        return true;
    }

    public static int ScanEscapedDelimitedText(string text, int index, char delimiter)
    {
        index++;

        while (index < text.Length)
        {
            if (text[index] == '\\')
            {
                index = Math.Min(text.Length, index + 2);
                continue;
            }

            if (text[index] == delimiter)
            {
                return index + 1;
            }

            if (text[index] is '\r' or '\n')
            {
                break;
            }

            index++;
        }

        return index;
    }

    public static int SkipNewLine(string text, int index)
    {
        if (text[index] == '\r' && index + 1 < text.Length && text[index + 1] == '\n')
        {
            return index + 2;
        }

        return index + 1;
    }

    public static bool StartsWith(string text, int index, string value)
    {
        return index + value.Length <= text.Length
            && text.AsSpan(index, value.Length).SequenceEqual(value);
    }

    public static bool StartsWithIgnoreCase(string text, int index, string value)
    {
        return index + value.Length <= text.Length
            && text.AsSpan(index, value.Length).Equals(value, StringComparison.OrdinalIgnoreCase);
    }

    public static int FindLongestMatchLength(
        string text,
        int index,
        IReadOnlyList<string> candidates)
    {
        int matchLength = 0;

        foreach (string candidate in candidates)
        {
            if (candidate.Length > matchLength && StartsWith(text, index, candidate))
            {
                matchLength = candidate.Length;
            }
        }

        return matchLength;
    }
}
