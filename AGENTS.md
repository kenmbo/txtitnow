# txtitnow Agent Instructions

## Project goal

Build a small Windows Notepad-like text editor in C# using WinForms.

## Release Goals

Release 1.0 should have:
- Create new notes
- Open plain text files
- Edit text
- Save files
- Save As
- Unsaved-change detection
- Basic keyboard shortcuts
- Simple menu bar similar to Windows Notepad

Future releases may add:
- Syntax highlighting
- Recently opened files
- Find/replace
- Status bar
- Line/column indicator
- Font customization
- Encoding options

## Technical constraints

- Use C# and .NET 10.
- Use WinForms.
- Keep the first version simple and maintainable.
- Prefer standard .NET libraries before adding external packages.
- Do not add syntax highlighting in the first milestone unless explicitly requested.
- Avoid over-engineering. This is a toy learning project.

## Coding style

- Keep UI code readable.
- Use descriptive method names.
- Separate file operations from UI event handlers when practical.
- Add comments only where they clarify non-obvious behavior.
- Do not make large architectural changes without explaining why.

## Development workflow

When asked to implement a milestone, do not implement the whole milestone unless explicitly requested.

Before making changes:
1. Briefly summarize the current project structure.
2. Identify the exact TODO.md bullet point(s) being implemented.
3. Propose a small implementation plan.
4. Make focused changes.
5. Explain how to run and test the app.
6. Suggest the next step and indicate if it is a bullet point or a new milestone.

Use the checklist in the milestones of TODO.md instead of attempting the whole application at once.
