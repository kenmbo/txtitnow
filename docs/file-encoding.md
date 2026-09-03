# File Encoding Detection Notes

## Goal

TxtItNow should open common Unicode text files without corrupting their contents and should make encoding behavior predictable. Encoding detection should remain separate from filename-based language detection: a `.c` file can use any supported text encoding.

The initial encoding-selection implementation follows the approach described here.

## Implemented Behavior

Open and Recent Files show an encoding dialog after a file is selected. The default is `Auto-detect`, or the user can explicitly select one of the supported Unicode encodings.

Automatic detection recognizes UTF-8, UTF-16 little-endian, UTF-16 big-endian, UTF-32 little-endian, and UTF-32 big-endian when the file begins with the corresponding byte-order mark (BOM). A file without a recognized BOM is decoded as strict UTF-8. Invalid byte sequences produce a user-facing error instead of replacement characters.

Save As shows an encoding dialog after a path is selected. A normal Save preserves the document's current encoding and BOM choice. New documents default to UTF-8 without a BOM, and the active encoding appears in the status bar.

## Detection Order

File loading returns both decoded text and its encoding description. Automatic detection proceeds in this order:

1. Inspect the beginning of the file for a BOM, checking longer signatures before shorter overlapping signatures:
   - UTF-32 big-endian: `00 00 FE FF`
   - UTF-32 little-endian: `FF FE 00 00`
   - UTF-8: `EF BB BF`
   - UTF-16 big-endian: `FE FF`
   - UTF-16 little-endian: `FF FE`
2. If there is no BOM, try strict UTF-8 decoding with invalid-byte detection enabled.
3. If strict UTF-8 decoding fails, do not silently guess a legacy code page. Report the decoding error so the user can open the file again with an explicit encoding.

Checking UTF-32 little-endian before UTF-16 little-endian is important because both signatures begin with `FF FE`.

## Document Encoding State

The document state retains:

- The selected strict `Encoding` used to decode and encode the file
- Whether saves should include a BOM
- A short display name shown in the status bar and encoding dialog

New untitled documents default to UTF-8 without a BOM. Opening a file sets the document encoding state from detection or the explicit selection. `Save` preserves that state, while `Save As` can replace it.

## Saving and Error Handling

- Normal Save preserves the detected or user-selected encoding.
- The BOM choice is preserved separately from character encoding behavior.
- Strict decoder and encoder fallbacks make invalid input or unrepresentable characters produce a user-facing error instead of silent replacement.
- Failed open and save operations do not modify the current document encoding state.
- Newline handling remains independent from encoding handling.

## Scope of the First Encoding Implementation

The first implementation supports Unicode formats detectable by BOM plus BOM-less UTF-8. The same Unicode formats can be selected explicitly, which also permits opening BOM-less UTF-16 and UTF-32 files when their byte order is known.

Support for legacy Windows code pages can be considered later, but it should require an explicit user choice rather than an unreliable content heuristic. Remaining decisions include:

- Whether legacy code-page support is useful for this project
- Whether encoding selection should eventually be integrated into a custom file dialog
- Whether an automatic decoding failure should immediately reopen the encoding dialog

## Manual Test Cases

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
