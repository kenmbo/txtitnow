# TxtItNow

TxtItNow is a small Windows Notepad-like text editor built with C#, .NET 10, and WinForms.

## Installation

```bash
git clone https://github.com/kenmbo/txtitnow.git
cd txtitnow/
dotnet build TxtItNow/TxtItNow.csproj
dotnet run --project TxtItNow/TxtItNow.csproj
# Wait around a minute, then a notepad application will appear.
```

## Version 1 goals

- New file
- Open `.txt` files
- Edit text
- Save
- Save As
- Detect unsaved changes
- Prompt before closing with unsaved changes
- Basic menu bar and keyboard shortcuts

## Future ideas

- Syntax highlighting
- Find and replace
- Status bar with line/column
- Recent files
- Font settings
- Encoding options

## Non-goals for v1

- Tabs
- Rich text formatting
- Plugin system
- Syntax highlighting
- Cloud sync
