namespace TxtItNow;

internal sealed class SyntaxColorPalette
{
    public static SyntaxColorPalette Light { get; } = new()
    {
        PlainText = Color.FromArgb(31, 31, 31),
        Keyword = Color.FromArgb(0, 0, 180),
        StringLiteral = Color.FromArgb(163, 21, 21),
        Comment = Color.FromArgb(0, 128, 0),
        Number = Color.FromArgb(128, 0, 128),
        Preprocessor = Color.FromArgb(0, 128, 128),
        TypeName = Color.FromArgb(43, 145, 175),
        FunctionName = Color.FromArgb(121, 94, 38),
        Operator = Color.FromArgb(64, 64, 64)
    };

    public static SyntaxColorPalette Dark { get; } = new()
    {
        PlainText = Color.FromArgb(212, 212, 212),
        Keyword = Color.FromArgb(86, 156, 214),
        StringLiteral = Color.FromArgb(206, 145, 120),
        Comment = Color.FromArgb(106, 153, 85),
        Number = Color.FromArgb(181, 206, 168),
        Preprocessor = Color.FromArgb(78, 201, 176),
        TypeName = Color.FromArgb(78, 201, 176),
        FunctionName = Color.FromArgb(220, 220, 170),
        Operator = Color.FromArgb(180, 180, 180)
    };

    public required Color PlainText { get; init; }

    public required Color Keyword { get; init; }

    public required Color StringLiteral { get; init; }

    public required Color Comment { get; init; }

    public required Color Number { get; init; }

    public required Color Preprocessor { get; init; }

    public required Color TypeName { get; init; }

    public required Color FunctionName { get; init; }

    public required Color Operator { get; init; }

    public static SyntaxColorPalette ForTheme(EditorThemeMode themeMode)
    {
        return themeMode switch
        {
            EditorThemeMode.Dark => Dark,
            _ => Light
        };
    }

    public Color GetColor(SyntaxTokenRole role)
    {
        return role switch
        {
            SyntaxTokenRole.Keyword => Keyword,
            SyntaxTokenRole.StringLiteral => StringLiteral,
            SyntaxTokenRole.Comment => Comment,
            SyntaxTokenRole.Number => Number,
            SyntaxTokenRole.Preprocessor => Preprocessor,
            SyntaxTokenRole.TypeName => TypeName,
            SyntaxTokenRole.FunctionName => FunctionName,
            SyntaxTokenRole.Operator => Operator,
            _ => PlainText
        };
    }
}
