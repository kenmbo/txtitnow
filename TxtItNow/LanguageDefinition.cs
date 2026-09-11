namespace TxtItNow;

internal sealed record LanguageDefinition(
    SupportedLanguage Language,
    string StatusBarDisplayName,
    IReadOnlyList<string> SupportedExtensions,
    ISyntaxHighlighter? SyntaxHighlighter);

