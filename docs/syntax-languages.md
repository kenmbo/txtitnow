# Syntax Languages and Scanner Contracts

## Documentation Ownership

This document is the authoritative source for language identification, display names, extension mappings, implementation status, and per-language scanner contracts. It defines which highlighter is selected for a recognized language and the token-recognition responsibilities of that highlighter.

Shared token roles, palettes, spans, RichTextBox integration, and recoloring policy belong in [`syntax-coloring.md`](syntax-coloring.md). Text decoding, encoding selection, and byte-order-mark behavior belong in [`file-encoding.md`](file-encoding.md). The root [`README.md`](../README.md) describes implemented user-facing support, and contributors must follow [`AGENTS.md`](../AGENTS.md).

## Organization

Keep all language specifications in this document initially so language-identification rules and scanner contracts are easy to compare. Do not create per-language specification documents unless this document becomes difficult to navigate; if that threshold is reached, retain this document as the index and authoritative location for shared language-detection rules.

## Language Identification

Language detection is filename-extension based. Match extensions case-insensitively with ordinal comparison; `.C`, `.CPP`, and `.Md` must resolve the same as their lowercase spellings. Do not infer a language from file contents, encoding, or a partial filename.

The resolver returns one language decision that supplies both the status-bar display name and the optional highlighter. An unknown extension, a missing file path, or a path with no extension resolves to `Plain Text`, which has no highlighter and displays `Plain Text` in the status bar.

The initial ambiguous-header policy is deliberately conservative: `.h` continues to resolve to C, even after C++ support is introduced. A future explicit per-document language override may revisit that choice; automatic content heuristics must not do so.

Do not inspect shebangs yet. An extensionless file beginning with `#!` remains `Plain Text`, including Bash and Python scripts, until the corresponding highlighter exists and a later task explicitly adds shebang detection.

## Implementation Status and Intended Mappings

This table is the maintained status record for implemented languages and the planned registration record for the next supported languages. An intended extension must not be added to the runtime registry until its highlighter exists.

| Language | Extensions and display name | Status | Supported constructs | Deferred constructs | Manual-test coverage |
| --- | --- | --- | --- | --- | --- |
| C | `.c`, `.h` → `C` | Implemented: `CSyntaxHighlighter` | Directives, comments, quoted literals, decimal/hexadecimal/binary numbers, keywords, built-in types, simple function-name heuristic, and operator characters | Compiler parsing, macro expansion, semantic symbols, and complete C lexical coverage | Required regression: open, edit, save, reopen, and Save As C files; cover directives, comments, literals, numbers, and uppercase extensions. |
| C++ | `.cpp`, `.cc`, `.cxx`, `.c++`, `.hpp`, `.hh`, `.hxx`, `.h++` → `C++` | Planned; no registry entry or highlighter before Milestone 8 | Defined in the C++23 first-pass contract below | Defined in the C++23 first-pass contract below | Not yet available; Milestone 8 defines the required coverage. |
| Markdown | `.md`, `.markdown` → `Markdown` | Planned; no registry entry or highlighter before Milestone 9 | Defined in the pragmatic Markdown contract below | Defined in the pragmatic Markdown contract below | Not yet available; Milestone 9 defines the required coverage. |
| C# | `.cs` → `C#` | Planned; no registry entry or highlighter before Milestone 10 | Defined in the C# 14 first-pass contract below | Defined in the C# 14 first-pass contract below | Not yet available; Milestone 10 defines the required coverage. |

Java, Bash, Python, CSS, JSON, HTML/XML, JavaScript, SQL, and YAML are planned languages only. They have no runtime registry entry, display-name selection, extension mapping, or highlighter until an implementation milestone defines all four together.

## C++23 First-Pass Contract

### Scope and detection

The C++ scanner is planned for Milestone 8 as a separate `CppSyntaxHighlighter`. Its contract covers a pragmatic [C++23 lexical surface](https://www.open-std.org/jtc1/sc22/wg21/docs/papers/2023/n4950.pdf), rather than a compiler or preprocessor implementation. It will resolve the C++ extensions listed in the status table; `.c` and `.h` remain C.

### Supported lexical recognition

The first pass must recognize the following without changing source text:

- C-compatible line comments, block comments, identifiers, character literals, string literals, numbers, and operators.
- Preprocessor directives only when `#` is the first non-whitespace character on a logical line, including backslash-continued directive lines.
- Ordinary, wide, UTF-8, UTF-16, and UTF-32 character and string literal prefixes, plus raw string literals with a valid custom delimiter and matching terminator.
- Unterminated block comments, quoted literals, raw strings, and continued directives deterministically through the end of input.
- Binary, octal, decimal, and hexadecimal integer literals; decimal and hexadecimal floating-point literals; digit separators; standard suffixes; and user-defined literal suffixes.
- Multi-character operators before their shorter prefixes. Treat `::`, template angle brackets, pointer/reference punctuation, and other punctuation lexically, without semantic parsing.
- The documented C++ keyword set and common built-in and standard type names as `Keyword` and `TypeName`.
- Conservative type-name heuristics after `class`, `struct`, `union`, `enum`, `using`, and `typedef`, plus a conservative function-name heuristic for an identifier followed by a call or declaration parenthesis.

Uncertain namespace-qualified names, template arguments, macro names, aliases, and declarations remain plain identifiers. The scanner explicitly defers macro expansion, translation-phase emulation, semantic template parsing, namespace and scope resolution, contextual type inference, and nested-language highlighting. It must not claim compiler-level C++23 conformance.

## Pragmatic Markdown First-Pass Contract

### Scope and detection

The Markdown scanner is planned for Milestone 9 as a deterministic, Markdown-aware scanner, not whole-document regular-expression passes or a full CommonMark parser. It will resolve `.md` and `.markdown` as `Markdown` and will add `Heading`, `Emphasis`, `Link`, `Code`, and `Quote` roles with explicit palette mappings when implemented.

This contract is a deliberately conservative subset of [CommonMark 0.31.2](https://spec.commonmark.org/0.31.2/). It favors stable, non-overlapping spans for live editing over complete parsing of every valid or ambiguous CommonMark construct.

### Supported block constructs

The first pass must recognize:

- ATX headings with up to three leading spaces, one through six `#` markers, a required following space or end of line, and optional closing markers.
- Setext headings using `=` or `-` underline lines after eligible nonblank text. When that context is absent, a thematic-break interpretation wins where the line qualifies; the scanner must apply this ambiguity rule consistently.
- Backtick and tilde fenced code blocks with at least three opening markers; a closing fence must use the same marker and at least the opening length. An unclosed fence remains code through end of input, and its info string does not select another highlighter.
- One or more block-quote markers, unordered and ordered list markers after permitted indentation, and thematic breaks without confusing them with list markers or Setext underlines.
- A conservative protected subset of HTML comments, declarations, and common block-tag regions. These regions remain plain text and prevent inline Markdown recognition inside them.

### Supported inline constructs

Inside unclaimed, non-protected text, the first pass must recognize:

- Variable-length inline backtick code spans only when closed by a run of the same length, without normalizing source text or span positions.
- Inline links and images with labels and destinations, including one balanced parenthesis level in a destination and escaped delimiters.
- Full and collapsed reference-link syntax without semantic resolution of reference definitions.
- Same-line, non-nested emphasis and strong emphasis with `*` and `_`, selecting strong emphasis before single emphasis when both begin at the same position.
- Escaped punctuation as literal text for these supported rules.

Do not classify intraword underscores, unmatched delimiters, or ambiguous punctuation as emphasis. Fenced code and protected HTML blocks take precedence over other block rules; inline code takes precedence over links, images, emphasis, and strong emphasis.

The first pass explicitly defers nested syntax coloring in fenced code, complete CommonMark delimiter-stack behavior and deeply nested emphasis, semantic reference-link resolution, autolinks, tables, task lists, footnotes, other flavor-specific extensions, complete HTML-block classification, and nested HTML syntax coloring.

## C# 14 First-Pass Contract

### Scope and detection

The C# scanner is planned for Milestone 10 as a separate `CSharpSyntaxHighlighter`. It will resolve `.cs` as `C#` and add an `Annotation` token role for attribute names when implemented.

`TxtItNow.csproj` targets .NET 10 and does not set `LangVersion`. The compiler's [default C# language version for that target is C# 14](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-versioning), so this contract uses the C# 14 lexical surface. This describes lexical coloring only; it does not require a semantic C# parser.

### Supported lexical recognition

The first pass must recognize:

- Line comments and multiline block comments, including deterministic handling of an unterminated block comment.
- Preprocessor directives only when `#` is the first non-whitespace character on a line. Directive lines use `Preprocessor`; conditional-compilation expressions are not evaluated.
- Character literals and normal string literals with standard escape sequences.
- Verbatim strings with doubled-quote escaping, interpolated normal and verbatim strings in either valid prefix order, and single-line or multiline raw strings with three or more quote delimiters.
- Interpolated raw strings with multiple `$` prefixes and their corresponding brace-count rules. Each interpolated string is colored as a string in this pass; expressions in interpolation holes are not highlighted separately.
- Deterministic handling of incomplete normal, verbatim, interpolated, and raw strings without scanning past their permitted boundary.
- Decimal, hexadecimal, and binary integer literals; real literals; digit separators; exponents; and standard numeric suffixes.
- Reserved and documented contextual keywords using conservative lexical context, predefined C# types as `TypeName`, and conservative type-name heuristics after `class`, `struct`, `interface`, `record`, `enum`, and `delegate`.
- Conservative function-name heuristics for declarations and invocations while excluding control-flow keywords, attribute lists with attribute identifiers as `Annotation`, and longest-match operators.

Uncertain generic type arguments, LINQ contextual keywords, pattern variables, aliases, namespaces, and semantic symbol classifications remain plain identifiers. The scanner explicitly defers semantic parsing and symbol resolution, inactive preprocessor-region dimming, XML documentation parsing, and nested interpolation-expression highlighting.

## Contract Maintenance

When a language becomes implemented, update its row in the status table in the same change with its active extensions, display name, supported constructs, deferred constructs, and concrete manual-test coverage. Update the relevant scanner contract before changing its lexical behavior; do not register a partially documented or unsupported language.
