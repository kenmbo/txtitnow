# Syntax Languages and Scanner Contracts

## Documentation Ownership

This document is the authoritative source for filename-based language detection, language display names, and per-language scanner contracts. It will define which highlighter is used for each recognized language and the token-recognition responsibilities of that highlighter.

Shared syntax-coloring architecture, token roles, palettes, and RichTextBox formatting behavior belong in [`syntax-coloring.md`](syntax-coloring.md). Text encoding and byte-order-mark behavior belong only in [`file-encoding.md`](file-encoding.md). The root [`README.md`](../README.md) describes implemented, user-facing language support, and contributors must follow [`AGENTS.md`](../AGENTS.md).

## Organization

Keep all language specifications in this document initially so language-identification rules and scanner contracts are easy to compare. Do not create per-language specification documents unless this document becomes difficult to navigate; if that threshold is reached, retain this document as the index and authoritative location for shared language-detection rules.

## Current Scope

The detailed extension mapping, implementation status, and individual language scanner contracts will be added in the remaining Milestone 6 language-scanner documentation tasks. Until then, this document establishes their single ownership location without redefining shared architecture or encoding behavior.
