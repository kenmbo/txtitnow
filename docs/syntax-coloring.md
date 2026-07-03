# Syntax Coloring Design Notes

## Goal

Syntax coloring should make source files easier to scan while keeping TxtItNow simple and maintainable. The first implementation should support one prototype language, then leave room for more languages, dark mode, and future editor features.

## Dark Mode Relationship

Syntax coloring must acknowledge the Dark Mode TODO section from the start. The highlighter should not hardcode light-mode colors. Instead, it should identify semantic token roles, and the active editor theme should map those roles to colors.

The intended flow is:

```text
source text -> tokenizer/highlighter -> syntax token roles -> active theme palette -> RichTextBox formatting
```

This means the same tokenizer can work in light mode and dark mode. Switching themes should only swap palettes and reapply formatting.

## Editor Control Requirement

Syntax coloring requires a `RichTextBox` because plain `TextBox` controls cannot color individual spans of text. The editor is now based on `EditorRichTextBox`, a small `RichTextBox` subclass that keeps viewport-change notifications for the line-number gutter.

Future syntax-coloring work should preserve existing behavior:

- File open, save, and save-as workflows
- Dirty-state detection
- Undo, cut, copy, paste, select all
- Find and replace
- Word wrap
- Font selection
- Status bar line/column updates
- Line-number gutter behavior
- Recent files behavior

## Theme-Independent Token Roles

The highlighter should emit roles, not colors. Suggested initial roles:

- `PlainText`
- `Keyword`
- `StringLiteral`
- `Comment`
- `Number`
- `Preprocessor`
- `TypeName`
- `FunctionName`
- `Operator`

The theme layer should own the colors for each role. Example shape:

```csharp
internal enum SyntaxTokenRole
{
    PlainText,
    Keyword,
    StringLiteral,
    Comment,
    Number,
    Preprocessor,
    TypeName,
    FunctionName,
    Operator
}
```

```csharp
internal sealed class SyntaxColorPalette
{
    public Color PlainText { get; init; }
    public Color Keyword { get; init; }
    public Color StringLiteral { get; init; }
    public Color Comment { get; init; }
    public Color Number { get; init; }
    public Color Preprocessor { get; init; }
    public Color TypeName { get; init; }
    public Color FunctionName { get; init; }
    public Color Operator { get; init; }
}
```

## Suggested Palettes

Use restrained, readable colors with enough contrast against each editor background.

Light mode:

- Plain text: near black
- Keywords: medium blue
- Strings: dark red
- Comments: muted green
- Numbers: purple
- Preprocessor: teal
- Types: dark cyan
- Functions: dark gold/brown
- Operators: dark gray

Dark mode:

- Plain text: light gray
- Keywords: soft blue
- Strings: warm orange
- Comments: soft green
- Numbers: lavender
- Preprocessor: cyan
- Types: light teal
- Functions: pale yellow
- Operators: medium gray

These values should be adjusted visually when Dark Mode is implemented.

## Prototype Language

C is a good first prototype because the current manual examples already include C-like files. Initial C support can recognize:

- Preprocessor lines beginning with `#`
- Keywords such as `int`, `return`, `if`, `else`, `for`, `while`, `void`, and `include`
- String literals in double quotes
- Line comments using `//`
- Block comments using `/* ... */`
- Numeric literals

The prototype does not need full compiler-level parsing. A small tokenizer is enough for the first pass.

## Implementation Notes

Keep the tokenizer separate from WinForms UI code. A useful shape is:

```text
ISyntaxHighlighter
  -> returns syntax spans with start, length, and SyntaxTokenRole

SyntaxColorPalette
  -> maps SyntaxTokenRole to Color for the current light/dark mode

RichTextBox formatting code
  -> applies spans to the editor without changing text content
```

When applying formatting, preserve:

- Current selection
- Scroll position where practical
- Dirty-state behavior
- Undo behavior as much as WinForms allows

Syntax coloring should be reapplied when:

- Text changes
- A file is opened
- The current file extension changes
- The active theme changes
- Font changes, if rendering needs recalculation

## Future Codex Guidance

Do not mix token recognition and theme colors in the same class. If a highlighter emits `Color.Blue` directly, it will make Dark Mode harder. Emit roles such as `Keyword` and let the active theme palette decide what `Keyword` looks like.

Before implementing syntax coloring, refactor to `RichTextBox` as its own task and verify the existing editor behaviors still work.

