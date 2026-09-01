namespace TxtItNow;

internal interface ISyntaxHighlighter
{
    IReadOnlyList<SyntaxSpan> Highlight(string text);
}
