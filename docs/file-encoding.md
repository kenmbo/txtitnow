# File Encoding Detection Notes

## Goal

TxtItNow should open common Unicode text files without corrupting their contents and should make encoding behavior predictable. Encoding detection should remain separate from filename-based language detection: a `.c` file can use any supported text encoding.

These notes define a future approach only. They do not add encoding selection or change the current file-reading and file-writing code.

## Current Behavior

TxtItNow currently reads files with `File.ReadAllText(path)`. .NET detects UTF-8, UTF-16 little-endian, UTF-16 big-endian, UTF-32 little-endian, and UTF-32 big-endian when the file begins with the corresponding byte-order mark (BOM). Without a recognized BOM, the overload uses UTF-8.

TxtItNow currently saves with `File.WriteAllText(path, text)`, which writes UTF-8 without a BOM. Consequently, opening a BOM-marked UTF-16 or UTF-32 file and saving it currently converts it to UTF-8 without a BOM.

## Proposed Detection Order

When encoding support is implemented, file loading should return both decoded text and an encoding description. Detection should proceed in this order:

1. Inspect the beginning of the file for a BOM, checking longer signatures before shorter overlapping signatures:
   - UTF-32 big-endian: `00 00 FE FF`
   - UTF-32 little-endian: `FF FE 00 00`
   - UTF-8: `EF BB BF`
   - UTF-16 big-endian: `FE FF`
   - UTF-16 little-endian: `FF FE`
2. If there is no BOM, try strict UTF-8 decoding with invalid-byte detection enabled.
3. If strict UTF-8 decoding fails, do not silently guess a legacy code page. Report that the encoding could not be determined and allow the user to choose an encoding once the encoding-selection UI exists.

Checking UTF-32 little-endian before UTF-16 little-endian is important because both signatures begin with `FF FE`.

## Document Encoding State

The document state should eventually retain:

- The selected `Encoding` used to decode and encode the file
- Whether the original file included a BOM
- A short display name suitable for a future status-bar or dialog indicator

New untitled documents should default to UTF-8 without a BOM. Opening a file should set the document encoding state from detection. `Save` should preserve that state, while a future `Save As` encoding choice may replace it.

## Saving and Error Handling

- Preserve the detected or user-selected encoding during a normal Save.
- Preserve the BOM choice separately from the encoding where the encoding supports a preamble.
- Use strict decoder and encoder fallbacks so invalid input or unrepresentable characters cause a user-facing error instead of silent replacement.
- Do not modify the current document encoding state when opening or saving fails.
- Keep newline handling independent from encoding handling.

## Scope of the First Encoding Implementation

The first implementation should support the Unicode formats detectable by BOM plus BOM-less UTF-8. Support for legacy Windows code pages can be considered later, but it should require an explicit user choice rather than an unreliable content heuristic.

The next roadmap item, optional encoding selection for open/save operations, should decide:

- Which encodings appear in the UI
- Whether encoding is selected in the existing dialogs or a separate dialog
- Whether the selected encoding is shown in the status bar
- How the app handles a BOM-less file that is not valid UTF-8

## Manual Test Cases for Future Implementation

- Open UTF-8 files with and without a BOM.
- Open UTF-16 little-endian and big-endian files with BOMs.
- Open UTF-32 little-endian and big-endian files with BOMs.
- Save each opened file and confirm its encoding and BOM choice are preserved.
- Open a BOM-less file containing invalid UTF-8 and confirm the app does not silently replace characters.
- Save text containing characters that the selected encoding cannot represent and confirm a useful error is shown.

## References

- [File.ReadAllText documentation](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltext?view=net-10.0)
- [File.WriteAllText documentation](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-10.0)
- [StreamReader encoding detection documentation](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader.-ctor?view=net-10.0)
