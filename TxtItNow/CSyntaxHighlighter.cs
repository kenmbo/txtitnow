namespace TxtItNow;

internal sealed class CSyntaxHighlighter : ISyntaxHighlighter
{
    private static readonly HashSet<string> Keywords = new(StringComparer.Ordinal)
    {
        "auto",
        "break",
        "case",
        "const",
        "continue",
        "default",
        "do",
        "else",
        "enum",
        "extern",
        "for",
        "goto",
        "if",
        "register",
        "return",
        "sizeof",
        "static",
        "struct",
        "switch",
        "typedef",
        "union",
        "volatile",
        "while"
    };

    private static readonly HashSet<string> TypeNames = new(StringComparer.Ordinal)
    {
        "bool",
        "char",
        "double",
        "FILE",
        "float",
        "int",
        "int8_t",
        "int16_t",
        "int32_t",
        "int64_t",
        "long",
        "short",
        "signed",
        "size_t",
        "uint8_t",
        "uint16_t",
        "uint32_t",
        "uint64_t",
        "unsigned",
        "void"
    };

    public IReadOnlyList<SyntaxTokenSpan> GetSyntaxSpans(string text)
    {
        List<SyntaxTokenSpan> spans = new();
        int index = 0;

        while (index < text.Length)
        {
            char current = text[index];

            if (IsPreprocessorStart(text, index))
            {
                int end = ScanToLineEnd(text, index);
                AddSpan(spans, index, end, SyntaxTokenRole.Preprocessor);
                index = end;
            }
            else if (StartsWith(text, index, "//"))
            {
                int end = ScanToLineEnd(text, index);
                AddSpan(spans, index, end, SyntaxTokenRole.Comment);
                index = end;
            }
            else if (StartsWith(text, index, "/*"))
            {
                int end = ScanBlockComment(text, index);
                AddSpan(spans, index, end, SyntaxTokenRole.Comment);
                index = end;
            }
            else if (current == '"' || current == '\'')
            {
                int end = ScanQuotedLiteral(text, index, current);
                AddSpan(spans, index, end, SyntaxTokenRole.StringLiteral);
                index = end;
            }
            else if (char.IsDigit(current))
            {
                int end = ScanNumber(text, index);
                AddSpan(spans, index, end, SyntaxTokenRole.Number);
                index = end;
            }
            else if (IsIdentifierStart(current))
            {
                int end = ScanIdentifier(text, index);
                string identifier = text[index..end];
                SyntaxTokenRole? role = GetIdentifierRole(text, identifier, end);

                if (role is not null)
                {
                    AddSpan(spans, index, end, role.Value);
                }

                index = end;
            }
            else if (IsOperator(current))
            {
                AddSpan(spans, index, index + 1, SyntaxTokenRole.Operator);
                index++;
            }
            else
            {
                index++;
            }
        }

        return spans;
    }

    private static SyntaxTokenRole? GetIdentifierRole(string text, string identifier, int end)
    {
        if (Keywords.Contains(identifier))
        {
            return SyntaxTokenRole.Keyword;
        }

        if (TypeNames.Contains(identifier))
        {
            return SyntaxTokenRole.TypeName;
        }

        int nextNonWhitespace = SkipWhitespace(text, end);
        return nextNonWhitespace < text.Length && text[nextNonWhitespace] == '('
            ? SyntaxTokenRole.FunctionName
            : null;
    }

    private static bool IsPreprocessorStart(string text, int index)
    {
        if (text[index] != '#')
        {
            return false;
        }

        int lineStart = index;

        while (lineStart > 0 && text[lineStart - 1] != '\n')
        {
            lineStart--;
        }

        for (int current = lineStart; current < index; current++)
        {
            if (!char.IsWhiteSpace(text[current]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool StartsWith(string text, int index, string value)
    {
        return index + value.Length <= text.Length
            && string.CompareOrdinal(text, index, value, 0, value.Length) == 0;
    }

    private static int ScanToLineEnd(string text, int start)
    {
        int index = start;

        while (index < text.Length && text[index] != '\n')
        {
            index++;
        }

        return index;
    }

    private static int ScanBlockComment(string text, int start)
    {
        int index = start + 2;

        while (index < text.Length - 1)
        {
            if (StartsWith(text, index, "*/"))
            {
                return index + 2;
            }

            index++;
        }

        return text.Length;
    }

    private static int ScanQuotedLiteral(string text, int start, char quote)
    {
        int index = start + 1;
        bool escaped = false;

        while (index < text.Length)
        {
            char current = text[index];

            if (escaped)
            {
                escaped = false;
            }
            else if (current == '\\')
            {
                escaped = true;
            }
            else if (current == quote)
            {
                return index + 1;
            }
            else if (current == '\n')
            {
                return index;
            }

            index++;
        }

        return text.Length;
    }

    private static int ScanNumber(string text, int start)
    {
        int index = start;

        while (index < text.Length && IsNumberPart(text[index]))
        {
            index++;
        }

        return index;
    }

    private static int ScanIdentifier(string text, int start)
    {
        int index = start + 1;

        while (index < text.Length && IsIdentifierPart(text[index]))
        {
            index++;
        }

        return index;
    }

    private static int SkipWhitespace(string text, int start)
    {
        int index = start;

        while (index < text.Length && char.IsWhiteSpace(text[index]))
        {
            index++;
        }

        return index;
    }

    private static void AddSpan(List<SyntaxTokenSpan> spans, int start, int end, SyntaxTokenRole role)
    {
        if (end > start)
        {
            spans.Add(new SyntaxTokenSpan(start, end - start, role));
        }
    }

    private static bool IsIdentifierStart(char value)
    {
        return char.IsLetter(value) || value == '_';
    }

    private static bool IsIdentifierPart(char value)
    {
        return char.IsLetterOrDigit(value) || value == '_';
    }

    private static bool IsNumberPart(char value)
    {
        return char.IsLetterOrDigit(value) || value is '.' or '_';
    }

    private static bool IsOperator(char value)
    {
        return value is '+' or '-' or '*' or '/' or '%' or '=' or '!' or '<' or '>'
            or '&' or '|' or '^' or '~' or '?' or ':' or ';' or ',' or '.'
            or '(' or ')' or '[' or ']' or '{' or '}';
    }
}

