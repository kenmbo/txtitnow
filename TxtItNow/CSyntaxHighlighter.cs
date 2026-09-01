namespace TxtItNow;

internal sealed class CSyntaxHighlighter : ISyntaxHighlighter
{
    private static readonly HashSet<string> Keywords =
    [
        "auto", "break", "case", "const", "continue", "default", "do", "else",
        "enum", "extern", "for", "goto", "if", "inline", "register", "restrict",
        "return", "sizeof", "static", "struct", "switch", "typedef", "union",
        "volatile", "while", "_Alignas", "_Alignof", "_Atomic", "_Generic",
        "_Noreturn", "_Static_assert", "_Thread_local"
    ];

    private static readonly HashSet<string> TypeNames =
    [
        "void", "char", "short", "int", "long", "float", "double", "signed",
        "unsigned", "_Bool", "_Complex", "_Imaginary"
    ];

    private const string OperatorCharacters = "+-*/%=!<>&|^~?:.#";

    public IReadOnlyList<SyntaxSpan> Highlight(string text)
    {
        List<SyntaxSpan> spans = new();
        int index = 0;

        while (index < text.Length)
        {
            int tokenStart = index;

            if (text[index] == '#' && IsAtPreprocessorStart(text, index))
            {
                index = ScanPreprocessorDirective(text, index);
                AddSpan(spans, tokenStart, index, SyntaxTokenRole.Preprocessor);
                continue;
            }

            if (StartsWith(text, index, "//"))
            {
                index = ScanLineComment(text, index);
                AddSpan(spans, tokenStart, index, SyntaxTokenRole.Comment);
                continue;
            }

            if (StartsWith(text, index, "/*"))
            {
                index = ScanBlockComment(text, index);
                AddSpan(spans, tokenStart, index, SyntaxTokenRole.Comment);
                continue;
            }

            if (text[index] is '"' or '\'')
            {
                index = ScanQuotedLiteral(text, index, text[index]);
                AddSpan(spans, tokenStart, index, SyntaxTokenRole.StringLiteral);
                continue;
            }

            if (char.IsDigit(text[index]) || (text[index] == '.' && HasNextDigit(text, index)))
            {
                index = ScanNumber(text, index);
                AddSpan(spans, tokenStart, index, SyntaxTokenRole.Number);
                continue;
            }

            if (IsIdentifierStart(text[index]))
            {
                index = ScanIdentifier(text, index);
                string identifier = text[tokenStart..index];
                SyntaxTokenRole? role = GetIdentifierRole(text, identifier, index);

                if (role.HasValue)
                {
                    AddSpan(spans, tokenStart, index, role.Value);
                }

                continue;
            }

            if (OperatorCharacters.Contains(text[index]))
            {
                index = ScanOperator(text, index);
                AddSpan(spans, tokenStart, index, SyntaxTokenRole.Operator);
                continue;
            }

            index++;
        }

        return spans;
    }

    private static bool IsAtPreprocessorStart(string text, int index)
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

    private static int ScanPreprocessorDirective(string text, int index)
    {
        index++;

        while (index < text.Length)
        {
            if (text[index] is not ('\r' or '\n'))
            {
                index++;
                continue;
            }

            int previousIndex = index - 1;
            bool continuesOnNextLine = previousIndex >= 0 && text[previousIndex] == '\\';

            if (!continuesOnNextLine)
            {
                break;
            }

            index = SkipNewLine(text, index);
        }

        return index;
    }

    private static int ScanLineComment(string text, int index)
    {
        index += 2;

        while (index < text.Length && text[index] is not ('\r' or '\n'))
        {
            index++;
        }

        return index;
    }

    private static int ScanBlockComment(string text, int index)
    {
        index += 2;

        while (index < text.Length)
        {
            if (StartsWith(text, index, "*/"))
            {
                return index + 2;
            }

            index++;
        }

        return index;
    }

    private static int ScanQuotedLiteral(string text, int index, char quote)
    {
        index++;

        while (index < text.Length)
        {
            if (text[index] == '\\')
            {
                index = Math.Min(text.Length, index + 2);
                continue;
            }

            if (text[index] == quote)
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

    private static int ScanNumber(string text, int index)
    {
        if (StartsWithIgnoreCase(text, index, "0x"))
        {
            index += 2;
            index = ScanDigits(text, index, IsHexDigit);

            if (index < text.Length && text[index] == '.')
            {
                index = ScanDigits(text, index + 1, IsHexDigit);
            }

            if (index < text.Length && text[index] is 'p' or 'P')
            {
                index = ScanExponent(text, index);
            }

            return ScanNumberSuffix(text, index);
        }

        if (StartsWithIgnoreCase(text, index, "0b"))
        {
            index += 2;

            while (index < text.Length && text[index] is '0' or '1')
            {
                index++;
            }

            return ScanNumberSuffix(text, index);
        }

        index = ScanDigits(text, index, char.IsDigit);

        if (index < text.Length && text[index] == '.')
        {
            index = ScanDigits(text, index + 1, char.IsDigit);
        }

        if (index < text.Length && text[index] is 'e' or 'E')
        {
            index = ScanExponent(text, index);
        }

        return ScanNumberSuffix(text, index);
    }

    private static int ScanDigits(string text, int index, Func<char, bool> isDigit)
    {
        while (index < text.Length && isDigit(text[index]))
        {
            index++;
        }

        return index;
    }

    private static int ScanExponent(string text, int index)
    {
        index++;

        if (index < text.Length && text[index] is '+' or '-')
        {
            index++;
        }

        return ScanDigits(text, index, char.IsDigit);
    }

    private static int ScanNumberSuffix(string text, int index)
    {
        while (index < text.Length && text[index] is 'u' or 'U' or 'l' or 'L' or 'f' or 'F')
        {
            index++;
        }

        return index;
    }

    private static int ScanIdentifier(string text, int index)
    {
        index++;

        while (index < text.Length && IsIdentifierPart(text[index]))
        {
            index++;
        }

        return index;
    }

    private static SyntaxTokenRole? GetIdentifierRole(string text, string identifier, int index)
    {
        if (Keywords.Contains(identifier))
        {
            return SyntaxTokenRole.Keyword;
        }

        if (TypeNames.Contains(identifier))
        {
            return SyntaxTokenRole.TypeName;
        }

        while (index < text.Length && char.IsWhiteSpace(text[index]))
        {
            index++;
        }

        return index < text.Length && text[index] == '('
            ? SyntaxTokenRole.FunctionName
            : null;
    }

    private static int ScanOperator(string text, int index)
    {
        return index + 1;
    }

    private static int SkipNewLine(string text, int index)
    {
        if (text[index] == '\r' && index + 1 < text.Length && text[index + 1] == '\n')
        {
            return index + 2;
        }

        return index + 1;
    }

    private static bool StartsWith(string text, int index, string value)
    {
        return index + value.Length <= text.Length
            && text.AsSpan(index, value.Length).SequenceEqual(value);
    }

    private static bool StartsWithIgnoreCase(string text, int index, string value)
    {
        return index + value.Length <= text.Length
            && text.AsSpan(index, value.Length).Equals(value, StringComparison.OrdinalIgnoreCase);
    }

    private static bool HasNextDigit(string text, int index)
    {
        return index + 1 < text.Length && char.IsDigit(text[index + 1]);
    }

    private static bool IsIdentifierStart(char character)
    {
        return character == '_' || char.IsLetter(character);
    }

    private static bool IsIdentifierPart(char character)
    {
        return character == '_' || char.IsLetterOrDigit(character);
    }

    private static bool IsHexDigit(char character)
    {
        return char.IsDigit(character)
            || character is >= 'a' and <= 'f'
            || character is >= 'A' and <= 'F';
    }

    private static void AddSpan(
        ICollection<SyntaxSpan> spans,
        int start,
        int end,
        SyntaxTokenRole role)
    {
        spans.Add(new SyntaxSpan(start, end - start, role));
    }
}
