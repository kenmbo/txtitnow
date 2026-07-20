namespace TxtItNow;

internal interface ISyntaxHighlighter
{
    IReadOnlyList<SyntaxTokenSpan> GetSyntaxSpans(string text);
}

