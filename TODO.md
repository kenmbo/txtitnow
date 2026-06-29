# MyNotepad Roadmap


## Milestone 1: Basic editor window

- [x] Set the main form title to `TxtItNow`
- [x] Set a reasonable default main window size
- [x] Add a central multiline editor control
- [x] Make the editor fill the available client area
- [x] Enable vertical scrolling in the editor
- [x] Enable horizontal scrolling or word-wrap behavior explicitly
- [x] Add a menu bar
- [x] Add a `File` menu
- [x] Add `File > New`
- [x] Add `File > Open`
- [x] Add `File > Save`
- [x] Add `File > Save As`
- [x] Add `File > Exit`
- [x] Wire `File > Exit` to close the application
- [x] Add placeholder event handlers for File menu items that are not implemented yet

## Milestone 2: File state handling

- [x] Add a field/property for the current file path
- [x] Add a field/property for dirty/unsaved state
- [x] Mark the document as dirty when editor text changes
- [x] Mark the document as clean after a successful save
- [x] Update the window title to show the app name
- [x] Update the window title to show the current file name when a file is open
- [x] Update the window title to show an asterisk when the document has unsaved changes
- [x] Implement `File > New` to clear the editor
- [x] Reset the current file path when creating a new document
- [x] Reset dirty state after creating a new blank document
- [x] Prompt before `File > New` if there are unsaved changes
- [x] Implement `File > Open` with `OpenFileDialog`
- [x] Load selected file contents into the editor
- [x] Store the opened file path as the current file path
- [x] Mark the document as clean after opening a file
- [x] Prompt before `File > Open` if there are unsaved changes
- [x] Implement `File > Save As` with `SaveFileDialog`
- [x] Write editor contents to the selected file path
- [x] Store the saved path as the current file path
- [x] Mark the document as clean after `Save As`
- [x] Implement `File > Save` for files that already have a current file path
- [x] Make `File > Save` fall back to `Save As` when no current file path exists
- [x] Prompt before application exit if there are unsaved changes
- [x] Handle canceling an unsaved-changes prompt without losing editor content
- [x] Handle basic file read/write errors with a user-facing message box
- [x] Create workflow to automate Windows build testing

## Milestone 3: Editing features

- [x] Add an `Edit` menu
- [x] Add `Edit > Undo`
- [x] Wire `Edit > Undo` to the editor undo command
- [x] Add `Edit > Cut`
- [x] Wire `Edit > Cut` to the editor cut command
- [x] Add `Edit > Copy`
- [x] Wire `Edit > Copy` to the editor copy command
- [x] Add `Edit > Paste`
- [x] Wire `Edit > Paste` to the editor paste command
- [x] Add `Edit > Select All`
- [x] Wire `Edit > Select All` to select all editor text
- [x] Add keyboard shortcut for `New`
- [x] Add keyboard shortcut for `Open`
- [x] Add keyboard shortcut for `Save`
- [x] Add keyboard shortcut for `Save As`
- [x] Add keyboard shortcut for `Exit`
- [x] Add keyboard shortcut for `Undo`
- [x] Add keyboard shortcuts for `Cut`, `Copy`, and `Paste`
- [x] Add keyboard shortcut for `Select All`
- [x] Enable or disable `Undo` based on whether undo is available
- [x] Enable or disable `Cut` and `Copy` based on whether text is selected

## Milestone 4: Polish

- [x] Add a status bar
- [x] Show current line number in the status bar
- [x] Show current column number in the status bar
- [x] Update line and column when the caret moves
- [x] Add a `Format` menu
- [x] Add `Format > Word Wrap`
- [x] Wire `Word Wrap` to toggle editor wrapping
- [x] Persist the current word-wrap setting during the app session
- [x] Add `Format > Font`
- [x] Implement font selection with `FontDialog`
- [x] Apply the selected font to the editor
- [x] Add a basic application icon
- [x] Set the form icon to the application icon
- [x] Set the executable icon to the application icon
- [x] Bug fix: Horizontal scroll-bar not visible on overflowed line, despite word wrap disabled
- [x] Bug fix: New lines from source isn't preserved when pasting, despite new lines being preserved when pasting in other text editors
- [x] Add an `About` dialog
- [x] Add basic manual test notes to the README
- [ ] Github: Release 1.0

## Future

- [x] Add `Edit > Find`
- [x] Implement basic find-in-document behavior
- [x] Add `Edit > Replace`
- [x] Implement basic replace behavior
- [x] Add a recent files list
- [x] Implement recent files list for current session
- [x] Enable/Disable Line Numbers
- [x] Bold current line number (when line numbers is enabled)
- [x] Add syntax coloring design notes
- [ ] Choose a syntax-highlighting approach
- [ ] Add syntax coloring for one language as a prototype
- [ ] Add file encoding detection notes
- [ ] Add optional encoding selection for open/save operations
- [ ] Add `Format > Smart Indent`
- [ ] Copy previous line indentation when pressing Enter
- [ ] Increase indentation after lines ending with `{`
- [ ] Insert configured indentation when pressing Tab
- [ ] Persist recent files between app launches
- [ ] Add version number to the about to the `About` dialog
