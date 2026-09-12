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

    private static readonly IReadOnlyList<string> OperatorTokens =
    [
        "+", "-", "*", "/", "%", "=", "!", "<", ">", "&", "|", "^", "~", "?", ":", ".", "#"
    ];

    public IReadOnlyList<SyntaxSpan> Highlight(string text)
    {
        SyntaxSpanBuilder spans = new(text.Length);
        int index = 0;

        while (index < text.Length)
        {
            int tokenStart = index;

            if (text[index] == '#'
                && SyntaxScannerHelpers.IsAtLineStartAfterWhitespace(text, index))
            {
                index = ScanPreprocessorDirective(text, index);
                spans.Add(tokenStart, index, SyntaxTokenRole.Preprocessor);
                continue;
            }

            if (SyntaxScannerHelpers.StartsWith(text, index, "//"))
            {
                index = ScanLineComment(text, index);
                spans.Add(tokenStart, index, SyntaxTokenRole.Comment);
                continue;
            }

            if (SyntaxScannerHelpers.StartsWith(text, index, "/*"))
            {
                index = ScanBlockComment(text, index);
                spans.Add(tokenStart, index, SyntaxTokenRole.Comment);
                continue;
            }

            if (text[index] is '"' or '\'')
            {
                index = SyntaxScannerHelpers.ScanEscapedDelimitedText(text, index, text[index]);
                spans.Add(tokenStart, index, SyntaxTokenRole.StringLiteral);
                continue;
            }

            if (char.IsDigit(text[index]) || (text[index] == '.' && HasNextDigit(text, index)))
            {
                index = ScanNumber(text, index);
                spans.Add(tokenStart, index, SyntaxTokenRole.Number);
                continue;
            }

            if (SyntaxScannerHelpers.IsIdentifierStart(text[index]))
            {
                index = ScanIdentifier(text, index);
                string identifier = text[tokenStart..index];
                SyntaxTokenRole? role = GetIdentifierRole(text, identifier, index);

                if (role.HasValue)
                {
                    spans.Add(tokenStart, index, role.Value);
                }

                continue;
            }

            int operatorLength = SyntaxScannerHelpers.FindLongestMatchLength(
                text,
                index,
                OperatorTokens);

            if (operatorLength > 0)
            {
                index += operatorLength;
                spans.Add(tokenStart, index, SyntaxTokenRole.Operator);
                continue;
            }

            index++;
        }

        return spans.Build();
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

            index = SyntaxScannerHelpers.SkipNewLine(text, index);
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
            if (SyntaxScannerHelpers.StartsWith(text, index, "*/"))
            {
                return index + 2;
            }

            index++;
        }

        return index;
    }

    private static int ScanNumber(string text, int index)
    {
        if (SyntaxScannerHelpers.StartsWithIgnoreCase(text, index, "0x"))
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

        if (SyntaxScannerHelpers.StartsWithIgnoreCase(text, index, "0b"))
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

        while (index < text.Length && SyntaxScannerHelpers.IsIdentifierPart(text[index]))
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

    private static bool HasNextDigit(string text, int index)
    {
        return index + 1 < text.Length && char.IsDigit(text[index + 1]);
    }

    private static bool IsHexDigit(char character)
    {
        return char.IsDigit(character)
            || character is >= 'a' and <= 'f'
            || character is >= 'A' and <= 'F';
    }

}
