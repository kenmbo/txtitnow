namespace TxtItNow;

internal static class LanguageRegistry
{
    public static LanguageDefinition PlainText { get; } = new(
        SupportedLanguage.PlainText,
        "Plain Text",
        Array.Empty<string>(),
        null);

    private static readonly IReadOnlyList<LanguageDefinition> Definitions =
    [
        PlainText,
        new(
            SupportedLanguage.C,
            "C",
            [".c", ".h"],
            new CSyntaxHighlighter()),
        new(
            SupportedLanguage.Cpp,
            "C++",
            [".cpp", ".cc", ".cxx", ".c++", ".hpp", ".hh", ".hxx", ".h++"],
            null),
        new(
            SupportedLanguage.Markdown,
            "Markdown",
            [".md", ".markdown"],
            null),
        new(
            SupportedLanguage.CSharp,
            "C#",
            [".cs"],
            null)
    ];

    private static readonly IReadOnlyDictionary<string, LanguageDefinition> DefinitionsByExtension =
        Definitions
            .SelectMany(definition => definition.SupportedExtensions.Select(extension =>
                new KeyValuePair<string, LanguageDefinition>(extension, definition)))
            .ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.OrdinalIgnoreCase);

    public static LanguageDefinition Resolve(string? filePath)
    {
        string extension = Path.GetExtension(filePath) ?? string.Empty;

        return DefinitionsByExtension.TryGetValue(extension, out LanguageDefinition? definition)
            ? definition
            : PlainText;
    }
}

