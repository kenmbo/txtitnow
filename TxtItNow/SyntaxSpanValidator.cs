namespace TxtItNow;

internal static class SyntaxSpanValidator
{
    public static void Validate(int sourceLength, IReadOnlyList<SyntaxSpan> spans)
    {
        int previousEnd = 0;

        foreach (SyntaxSpan span in spans)
        {
            if (span.Length <= 0
                || span.Start < 0
                || span.Start > sourceLength - span.Length)
            {
                throw new InvalidOperationException(
                    "Syntax spans must have positive lengths and stay within the source.");
            }

            int end = span.Start + span.Length;
            ValidateNext(sourceLength, previousEnd, span.Start, end);
            previousEnd = end;
        }
    }

    public static void ValidateNext(int sourceLength, int previousEnd, int start, int end)
    {
        if (start < 0 || end <= start || end > sourceLength || start < previousEnd)
        {
            throw new InvalidOperationException(
                "Syntax spans must have positive lengths, stay within the source, and be ordered without overlap.");
        }
    }
}
