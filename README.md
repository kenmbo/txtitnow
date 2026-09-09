# TxtItNow

TxtItNow is a Windows Notepad-like text editor built with C# and WinForms.

## Installation

```bash
git clone https://github.com/kenmbo/txtitnow.git
cd txtitnow/
dotnet build TxtItNow/TxtItNow.csproj
dotnet run --project TxtItNow/TxtItNow.csproj
# Wait around a minute, then a notepad application will appear.
```

## Implemented features

- New file
- Open and save plain-text files
- Choose or automatically detect supported Unicode encodings when opening files
- Preserve the selected encoding and byte-order-mark choice when saving
- Edit text
- Detect unsaved changes
- Prompt before closing with unsaved changes
- File, Edit, Format, View, and Help menus with common keyboard shortcuts
- Undo, cut, copy, paste, select all, find, and replace
- Recent files for the current app session
- Word wrap, smart indentation, and font selection
- Line numbers and line/column status information
- C syntax coloring

## Documentation

- [Syntax-coloring architecture](docs/syntax-coloring.md) explains shared token, palette, and editor-formatting design.
- [Language identification and scanner contracts](docs/syntax-languages.md) owns language detection and per-language scanner specifications.
- [File encoding behavior](docs/file-encoding.md) owns encoding selection, decoding, saving, and BOM behavior.

## Manual test notes

Run the app:

```bash
dotnet run --project TxtItNow/TxtItNow.csproj
```

Basic file workflow:

- Create a new note, type text, and confirm the title shows an unsaved-change marker.
- Use `File > Save As` to save a `.txt` file, then confirm the title updates to the file name.
- Edit the saved file, use `File > Save`, close and reopen it, and confirm the latest text was saved.
- With unsaved changes, try `File > New`, `File > Open`, and closing the window; each should prompt before discarding changes.

Editing and formatting:

- Confirm `Edit > Undo`, `Cut`, `Copy`, `Paste`, and `Select All` work from both the menu and keyboard shortcuts.
- Copy multiline text from another editor and paste it with `Ctrl+V`; new lines should be preserved.
- Toggle `Format > Word Wrap` off, type or paste a long line, and confirm the horizontal scrollbar appears.
- Toggle word wrap back on and confirm long lines wrap in the editor.
- Use `Format > Font` to select a different font or size and confirm the editor updates.

Polish checks:

- Move the caret with the mouse and arrow keys; the status bar should update line and column.
- Confirm the window and executable use the TxtItNow icon.
- Open `Help > About TxtItNow` and confirm the About dialog appears.
