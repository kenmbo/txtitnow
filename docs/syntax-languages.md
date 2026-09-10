# Syntax Languages and Scanner Contracts

## Documentation Ownership

This document is the authoritative source for filename-based language detection, language display names, and per-language scanner contracts. It will define which highlighter is used for each recognized language and the token-recognition responsibilities of that highlighter.

[`syntax-languages.md`](syntax-languages.md) is the authoritative document for filename-based language detection, language display names, and per-language scanner contracts. [`file-encoding.md`](file-encoding.md) is authoritative only for text encoding and byte-order-mark behavior. The root [`README.md`](../README.md) documents implemented, user-facing feaatures.

## Language Identification

Language detection is filename-extension based. Match extensions case-insensitively with ordinal comparison; `.C`, `.CPP`, and `.Md` must resolve the same as their lowercase spellings. Do not infer a language from file contents, encoding, or a partial filename.

The resolver returns one language decision that supplies both the status-bar display name and the optional highlighter. An unknown extension, a missing file path, or a path with no extension resolves to `Plain Text`, which has no highlighter and displays `Plain Text` in the status bar.

The initial ambiguous-header policy is deliberately conservative: `.h` continues to resolve to C, even after C++ support is introduced. A future explicit per-document language override may revisit that choice; automatic content heuristics must not do so.

Do not inspect shebangs yet. An extensionless file beginning with `#!` remains `Plain Text`, including Bash and Python scripts, until the corresponding highlighter exists and a later task explicitly adds shebang detection.
