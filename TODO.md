# MyNotepad Roadmap


## Milestone 1: Basic editor window

- [x] Set the main form title to `TxtItNow`
- [x] Set a reasonable default main window size
- [x] Add a central multiline editor control
- [x] Make the editor fill the available client area
- [ ] Enable vertical scrolling in the editor
- [ ] Enable horizontal scrolling or word-wrap behavior explicitly
- [ ] Add a menu bar
- [ ] Add a `File` menu
- [ ] Add `File > New`
- [ ] Add `File > Open`
- [ ] Add `File > Save`
- [ ] Add `File > Save As`
- [ ] Add `File > Exit`
- [ ] Wire `File > Exit` to close the application
- [ ] Add placeholder event handlers for File menu items that are not implemented yet

## Milestone 2: File state handling

- [ ] Add a field/property for the current file path
- [ ] Add a field/property for dirty/unsaved state
- [ ] Mark the document as dirty when editor text changes
- [ ] Mark the document as clean after a successful save
- [ ] Update the window title to show the app name
- [ ] Update the window title to show the current file name when a file is open
- [ ] Update the window title to show an asterisk when the document has unsaved changes
- [ ] Implement `File > New` to clear the editor
- [ ] Reset the current file path when creating a new document
- [ ] Reset dirty state after creating a new blank document
- [ ] Prompt before `File > New` if there are unsaved changes
- [ ] Implement `File > Open` with `OpenFileDialog`
- [ ] Load selected file contents into the editor
- [ ] Store the opened file path as the current file path
- [ ] Mark the document as clean after opening a file
- [ ] Prompt before `File > Open` if there are unsaved changes
- [ ] Implement `File > Save As` with `SaveFileDialog`
- [ ] Write editor contents to the selected file path
- [ ] Store the saved path as the current file path
- [ ] Mark the document as clean after `Save As`
- [ ] Implement `File > Save` for files that already have a current file path
- [ ] Make `File > Save` fall back to `Save As` when no current file path exists
- [ ] Prompt before application exit if there are unsaved changes
- [ ] Handle canceling an unsaved-changes prompt without losing editor content
- [ ] Handle basic file read/write errors with a user-facing message box

## Milestone 3: Editing features

- [ ] Add an `Edit` menu
- [ ] Add `Edit > Undo`
- [ ] Wire `Edit > Undo` to the editor undo command
- [ ] Add `Edit > Cut`
- [ ] Wire `Edit > Cut` to the editor cut command
- [ ] Add `Edit > Copy`
- [ ] Wire `Edit > Copy` to the editor copy command
- [ ] Add `Edit > Paste`
- [ ] Wire `Edit > Paste` to the editor paste command
- [ ] Add `Edit > Select All`
- [ ] Wire `Edit > Select All` to select all editor text
- [ ] Add keyboard shortcut for `New`
- [ ] Add keyboard shortcut for `Open`
- [ ] Add keyboard shortcut for `Save`
- [ ] Add keyboard shortcut for `Save As`
- [ ] Add keyboard shortcut for `Exit`
- [ ] Add keyboard shortcut for `Undo`
- [ ] Add keyboard shortcuts for `Cut`, `Copy`, and `Paste`
- [ ] Add keyboard shortcut for `Select All`
- [ ] Enable or disable `Undo` based on whether undo is available
- [ ] Enable or disable `Cut` and `Copy` based on whether text is selected

## Milestone 4: Polish

- [ ] Add a status bar
- [ ] Show current line number in the status bar
- [ ] Show current column number in the status bar
- [ ] Update line and column when the caret moves
- [ ] Add a `Format` menu
- [ ] Add `Format > Word Wrap`
- [ ] Wire `Word Wrap` to toggle editor wrapping
- [ ] Persist the current word-wrap setting during the app session
- [ ] Add `Format > Font`
- [ ] Implement font selection with `FontDialog`
- [ ] Apply the selected font to the editor
- [ ] Add a basic application icon
- [ ] Set the form icon to the application icon
- [ ] Set the executable icon to the application icon
- [ ] Add an `About` dialog
- [ ] Add basic manual test notes to the README

## Future

- [ ] Add `Edit > Find`
- [ ] Implement basic find-in-document behavior
- [ ] Add `Edit > Replace`
- [ ] Implement basic replace behavior
- [ ] Add a recent files list
- [ ] Persist recent files between app launches
- [ ] Add syntax coloring design notes
- [ ] Choose a syntax-highlighting approach
- [ ] Add syntax coloring for one language as a prototype
- [ ] Add file encoding detection notes
- [ ] Add optional encoding selection for open/save operations
