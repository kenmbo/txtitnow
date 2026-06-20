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
- [ ] Add basic manual test notes to the README
- [ ] (Github) Release 1.0

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
- [ ] Detect and indicate the language in the status bar checking the filename extension
- [ ] Add file encoding detection notes
- [ ] Add optional encoding selection for open/save operations


### Dark mode

- [ ] Add a `View` menu if one does not already exist
- [ ] Add `View > Dark Mode`
- [ ] Store the current theme mode in app state
- [ ] Apply a dark color palette to the main form
- [ ] Apply dark mode colors to the editor control
- [ ] Apply dark mode colors to menus and status UI where practical
- [ ] Toggle back to the default light theme
- [ ] Update the `Dark Mode` menu item checked state when toggled
- [ ] Preserve the dark mode setting during the app session
- [ ] Add manual test notes for switching between light mode and dark mode

### Tabs

- [ ] Add a tab container to the main window
- [ ] Move the editor control into the first tab
- [ ] Create a document state model for each tab
- [ ] Track file path separately for each tab
- [ ] Track dirty/unsaved state separately for each tab
- [ ] Update the active tab title to show the file name
- [ ] Update the active tab title to show an unsaved-change marker
- [ ] Add `File > New Tab`
- [ ] Create a new blank editor tab from `File > New Tab`
- [ ] Add a close button or close command for the active tab
- [ ] Prompt to save when closing a dirty tab
- [ ] Close only the selected tab when multiple tabs are open
- [ ] Close the application when closing the only remaining tab
- [ ] Prompt to save before closing the application if the only remaining tab has unsaved content
- [ ] Update `File > Open` to load the selected file into the active tab
- [ ] Update `File > Save` to save only the active tab
- [ ] Update `File > Save As` to save only the active tab
- [ ] Update the main window title based on the active tab
- [ ] Update status bar information based on the active tab
- [ ] Add keyboard shortcut for creating a new tab
- [ ] Add keyboard shortcut for closing the active tab
- [ ] Add keyboard shortcuts for switching between tabs
- [ ] Add manual test notes for creating, saving, switching, and closing tabs
