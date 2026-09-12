# Syntax Coloring Design Notes

## Documentation Ownership

This document is the authoritative shared architecture document for syntax coloring. It owns theme-independent token roles, palettes, shared highlighter and formatting architecture, and editor-integration requirements.

`syntax-languages.md` is the authoritative document for filename-based language detection, language display names, and per-language scanner contracts. `file-encoding.md` is authoritative only for text encoding and byte-order-mark behavior. The root `README.md` documents implemented, user-facing features.

`TxtItNow/TxtItNow.csproj` is the authoritative source for the target framework.

## Scope and Current State

Syntax coloring makes source text easier to scan without changing document text. The shared pipeline is:

```text
file path -> one language decision -> highlighter and status-bar display name
decoded editor text -> ISyntaxHighlighter -> SyntaxSpan token roles
token roles + active theme -> SyntaxColorPalette -> RichTextBox formatting
```

The language decision must be made once and used both to select the highlighter and to choose the status-bar name. `LanguageRegistry` resolves one `LanguageDefinition` for the current file path; its optional `ISyntaxHighlighter` and status-bar display name are the single source for both outcomes. A definition without a highlighter receives plain-text formatting.

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

## Theme-Independent Token Roles and Palettes

Scanners emit only these `SyntaxTokenRole` values. A scanner may omit a span for ordinary text; the formatter supplies `PlainText` as the default. Every role must map explicitly in both palettes in the same change that introduces or changes the role.

| Role | Light palette | Dark palette |
| --- | --- | --- |
| `PlainText` | `#1F1F1F` | `#D4D4D4` |
| `Keyword` | `#0000B4` | `#569CD6` |
| `StringLiteral` | `#A31515` | `#CE9178` |
| `Comment` | `#008000` | `#6A9955` |
| `Number` | `#800080` | `#B5CEA8` |
| `Preprocessor` | `#008080` | `#4EC9B0` |
| `TypeName` | `#2B91AF` | `#4EC9B0` |
| `FunctionName` | `#795E26` | `#DCDCAA` |
| `Operator` | `#404040` | `#B4B4B4` |

`SyntaxColorPalette.Light` and `SyntaxColorPalette.Dark` implement this mapping. Its `GetColor` fallback is `PlainText`; a future role must not rely on that fallback instead of receiving both explicit palette entries.

## Syntax Span Contract

A `SyntaxSpan` contains `Start`, `Length`, and `SyntaxTokenRole`. `Start` and `Length` use .NET `string` positions: UTF-16 code units, not Unicode scalar values, grapheme clusters, UTF-8 bytes, or visual columns. Consequently, a character outside the Basic Multilingual Plane occupies two string positions.

For the source passed to `Highlight`, every returned span must satisfy all of these invariants:

- `Start` is at least zero.
- `Length` is positive.
- `Start + Length` is at most `text.Length`.
- Spans are returned in ascending `Start` order.
- Spans do not overlap; each span starts at or after the end of its predecessor.

`SyntaxSpanBuilder` is the common path for adding spans. It accepts source start and end positions, rejects non-positive, out-of-bounds, out-of-order, or overlapping requests before adding them, and validates the complete result when it is built. `SyntaxSpanValidator` makes those development-time failures explicit. Each scanner remains responsible for producing valid spans.

Scanners must always advance their source index, including for malformed input, so syntax coloring cannot loop indefinitely. Unterminated constructs must consume only the deterministic portion specified by their language contract; ambiguous text remains `PlainText` instead of triggering compiler-style recovery.

## Scanner Rules

Scanners favor deterministic lexical recognition over compiler-level parsing. Their precedence is:

1. Protect multiline constructs whose content must not be reinterpreted, such as block comments, multiline strings, or language-specific fenced regions.
2. Recognize comments and literals.
3. Recognize language-specific structures, such as directives or declarations.
4. Recognize identifiers.
5. Recognize numbers.
6. Recognize operators using longest-match rules.

The C baseline preserves its existing visible behavior; its numeric check occurs before its identifier check because their starting characters cannot conflict, and its current operator characters are emitted one at a time. New or revised scanners must use the shared precedence above, including longest-match handling where operator spelling overlaps.

When text is malformed, incomplete, or ambiguous, scanners must leave it as `PlainText` rather than guess aggressively. This keeps highlighting stable while a document is being edited and prevents a false classification from claiming unrelated text.

## Scanner Composition

Each language keeps a separate highlighter class; there is no shared C-like scanner base class. Language classes own their keywords, built-in types, literals, directives, declaration heuristics, and token precedence.

`SyntaxScannerHelpers` contains only mechanics that have already proven reusable: identifier-character classification, line-boundary checks, escaped-delimiter scanning, newline skipping, prefix matching, and longest-match selection. It does not decide which text belongs to a language token. The C highlighter keeps its existing language rules while using these helpers and the shared span builder.

## Formatting and Editor Preservation

Syntax formatting changes only `SelectionColor`; it must never replace document text. A formatting pass must:

1. Capture the selection start and length, including the zero-length caret case, and capture the current viewport position.
2. Guard against reentrant text-change handling and suspend redraw while applying the default `PlainText` color and individual token colors.
3. Restore the selection, caret color, and viewport after formatting, then re-enable redraw.
4. Avoid document text assignments, `SelectedText` changes, and undo-stack operations so dirty-state tracking and undo history are unchanged by coloring alone.

The existing applicator already guards reentrancy, suspends redraw, captures and restores selection, and resets an empty caret selection to `PlainText`. The shared integration must add explicit viewport preservation while retaining those safeguards. File open/save workflows, unsaved-change prompts, undo, cut, copy, paste, find, replace, word wrap, smart indentation, font selection, status updates, line-number gutter behavior, and recent-file behavior must continue to work exactly as they do without syntax coloring.


## Recoloring Policy and Performance

The current C prototype recolors the entire document immediately on text changes and when the file type or active theme changes. It is the behavioral baseline, but the initial shared multi-language policy is a full-document recoloring pass after a 150 ms text-change debounce:

- Restart the 150 ms timer on each text change; color only the latest document snapshot when it expires.
- Recolor immediately after a successful open, a current-file-path or language change, and a theme change.
- Keep all RichTextBox formatting on the UI thread and discard a stale scheduled request when a newer text change, document, language, or theme supersedes it.

Full-document recoloring is intentionally the initial strategy. Do not add visible-range or incremental highlighting merely because they are possible. First instrument representative documents and record document length in UTF-16 positions, scanner duration, formatting duration, total recoloring duration, and the elapsed time from the last edit to completed redraw.

Consider visible-range or incremental highlighting only when a 30-second sustained-edit sample on representative files shows either a 95th-percentile total recoloring duration above 50 ms, any repeatable recoloring duration above 100 ms, or a user-visible typing delay attributable to coloring. Measurements must identify whether scanning or RichTextBox formatting is the bottleneck before choosing an optimization.

## Implementation Boundaries

Keep scanners separate from WinForms UI code and favor small, deterministic scanners over broad regular-expression chains. A narrowly scoped regular expression is acceptable when it makes a scanner clearer, but scanners must not attempt full compiler parsing or recovery.

Language-specific recognition belongs in [`syntax-languages.md`](syntax-languages.md). Encoding is resolved before the editor receives text and belongs in [`file-encoding.md`](file-encoding.md); syntax spans always operate on the resulting .NET string.

