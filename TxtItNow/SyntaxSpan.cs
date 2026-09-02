namespace TxtItNow;

internal readonly record struct SyntaxSpan(
    int Start,
    int Length,
    SyntaxTokenRole Role);
