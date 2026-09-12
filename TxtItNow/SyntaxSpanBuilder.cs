namespace TxtItNow;

internal sealed class SyntaxSpanBuilder
{
    private readonly int sourceLength;
    private readonly List<SyntaxSpan> spans = [];

    public SyntaxSpanBuilder(int sourceLength)
    {
        sourceLength = Math.Max(0, sourceLength);
        this.sourceLength = sourceLength;
    }

    public void Add(int start, int end, SyntaxTokenRole role)
    {
        int previousEnd = spans.Count == 0
            ? 0
            : spans[^1].Start + spans[^1].Length;

        SyntaxSpanValidator.ValidateNext(sourceLength, previousEnd, start, end);
        spans.Add(new SyntaxSpan(start, end - start, role));
    }

    public IReadOnlyList<SyntaxSpan> Build()
    {
        SyntaxSpanValidator.Validate(sourceLength, spans);
        return spans;
    }
}
