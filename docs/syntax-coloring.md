# Syntax Coloring Design Notes

## Documentation Ownership

This document is the authoritative shared architecture document for syntax coloring. It owns theme-independent token roles, palettes, shared highlighter and formatting architecture, and editor-integration requirements.

`syntax-languages.md` is the authoritative document for filename-based language detection, language display names, and per-language scanner contracts. `file-encoding.md` is authoritative only for text encoding and byte-order-mark behavior. The root `README.md` documents implemented, user-facing features, while `AGENTS.md` contains mandatory constraints and the development workflow.

`TxtItNow/TxtItNow.csproj` is the authoritative source for the target framework.

## Scope and Current State

Syntax coloring makes source text easier to scan without changing document text. The shared pipeline is:

```text
file path -> one language decision -> highlighter and status-bar display name
decoded editor text -> ISyntaxHighlighter -> SyntaxSpan token roles
token roles + active theme -> SyntaxColorPalette -> RichTextBox formatting
```

The language decision must be made once and used both to select the highlighter and to choose the status-bar name. The current C prototype derives both outcomes from the same `IsCurrentFileCSource` predicate; the future language registry must replace those repeated checks with one resolved decision.

`ISyntaxHighlighter` accepts a .NET `string` and returns `SyntaxSpan` values. A highlighter recognizes semantic roles only. `SyntaxColorPalette` owns the conversion from those roles to `Color`, and the RichTextBox applicator performs the formatting. No scanner may reference `Color`, `EditorThemeMode`, light-mode values, or dark-mode values.

## C Regression Baseline

`CSyntaxHighlighter` is the existing compatibility baseline. It remains supported while the shared multi-language architecture is introduced. A regression in these behaviors requires correction before expanding language support:

- `.c` and `.h` files are recognized case-insensitively as C and display `C` in the status bar.
- Preprocessor directives beginning with `#` after only leading whitespace are colored as `Preprocessor`, including directives continued by a trailing backslash.
- `//` line comments and `/* ... */` block comments are colored as `Comment`; unterminated block comments continue to the end of the source.
- Escaped single- and double-quoted literals are colored as `StringLiteral`; an unterminated literal stops at its line boundary or the end of the source.
- Decimal, hexadecimal, and binary numeric forms, along with their current C suffix handling, are colored as `Number`.
- C keywords, built-in type names, and identifiers followed by `(` are colored as `Keyword`, `TypeName`, and `FunctionName`, respectively.
- The currently recognized C operator characters are colored as `Operator`.

This is a behavioral regression baseline, not a requirement to turn the C scanner into a compiler. Its detailed language contract belongs in [`syntax-languages.md`](syntax-languages.md).

