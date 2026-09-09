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
- [ ] (Github) Release 1.0

## Milestone 5: Post-Release

- [x] Add `Edit > Find`
- [x] Implement basic find-in-document behavior
- [x] Add `Edit > Replace`
- [x] Implement basic replace behavior
- [x] Add a recent files list
- [x] Implement recent files list for current session
- [x] Enable/Disable Line Numbers
- [x] Bold current line number (when line numbers is enabled)
- [x] Add syntax coloring design notes
- [x] Refactor the editor from `TextBox`/`EditorTextBox` to `RichTextBox`
  - [x] Preserve existing file, edit, find/replace, word-wrap, font, status bar, and line-number behavior after the `RichTextBox` refactor
- [x] Define theme-independent syntax token roles and separate light/dark syntax color palettes
- [x] Choose a theme-independent syntax-highlighting approach
- [x] Add syntax coloring for one language as a prototype using token roles from the active theme palette
- [x] Reapply syntax coloring when editor text, current file type, or active theme changes
- [x] Detect and indicate the language in the status bar checking the filename extension
- [x] Add file encoding detection notes
- [x] Add optional encoding selection for open/save operations
- [x] Add `Format > Smart Indent`
- [x] Copy previous line indentation when pressing Enter
- [x] Increase indentation after lines ending with `{`
- [x] Insert configured indentation when pressing Tab
- [ ] Persist recent files between app launches
- [ ] Add version number to the about to the `About` dialog


## Milestone 6: Expanded syntax-coloring specification

Complete this documentation milestone before changing the multi-language implementation.

### Documentation ownership and organization

- [x] Keep `syntax-coloring.md` as the authoritative document for shared syntax-coloring architecture, token roles, palettes, span invariants, editor integration, and performance policy
- [x] Create `syntax-languages.md` as the authoritative document for language identification and per-language scanner contracts
- [x] Keep `file-encoding.md` focused on encoding detection, BOM handling, open/save choices, and encoding preservation
- [x] Keep `README.md` focused on user-visible features, supported languages, limitations, build instructions, and manual test entry points
- [x] Keep `AGENTS.md` focused on mandatory project constraints and the incremental implementation workflow
- [x] State that the target framework in `TxtItNow.csproj` is authoritative when documentation disagrees
- [ ] Cross-link the syntax-coloring, language, encoding, README, and contributor documentation where their responsibilities meet
- [ ] Keep the language specifications in one document initially; split them into per-language documents only if `syntax-languages.md` becomes difficult to navigate

### Shared syntax-coloring documentation

- [ ] Update `syntax-coloring.md` from a C-prototype description to the current shared architecture
- [ ] Document the existing C highlighter as the regression baseline that must remain supported
- [ ] Document that scanners emit theme-independent `SyntaxTokenRole` values and must not contain light- or dark-theme colors
- [ ] Document how every token role maps explicitly in both the light and dark palettes
- [ ] Document that source indices and lengths use .NET UTF-16 string positions
- [ ] Document that `SyntaxSpan` results must be in ascending order, within the source bounds, positive-length, and non-overlapping
- [ ] Document scanner precedence: protected multiline constructs first, then comments and literals, then language-specific structures, identifiers, numbers, and longest-match operators
- [ ] Document that uncertain text remains `PlainText` rather than being guessed aggressively
- [ ] Document that one language decision selects both the highlighter and the status-bar language name
- [ ] Document how syntax formatting preserves the caret, selection, viewport, dirty state, undo history, and existing editor workflows
- [ ] Document the initial full-document recoloring strategy and the short text-change debounce policy
- [ ] Define measurements that would justify later visible-range or incremental highlighting instead of adding it preemptively

### Language identification and scanner contracts

- [ ] Document extension matching as case-insensitive
- [ ] Document the initial ambiguity policy for `.h`: continue treating it as C unless a future explicit language override is added
- [ ] Document `Plain Text` as the fallback for unknown or missing extensions
- [ ] Defer shebang detection for extensionless Bash and Python files until the corresponding highlighters exist
- [ ] Document a C++ first-pass contract based on the C++23 lexical surface supported by the scanner, including explicit deferrals
- [ ] Document a pragmatic Markdown first-pass contract, including the supported block and inline constructs and explicit CommonMark limitations
- [ ] Document a C# first-pass contract based on C# 14 because the project targets .NET 10 and does not set another language version
- [ ] Record Java, Bash, Python, CSS, JSON, HTML/XML, JavaScript, SQL, and YAML as planned languages without registering unsupported highlighters
- [ ] For every implemented language, maintain an implementation-status table with extensions, display name, supported constructs, deferred constructs, and manual-test coverage

### Documentation consistency checks

- [ ] Reconcile stale application names, framework versions, syntax-coloring status, and supported-language claims across `README.md`, `AGENTS.md`, and the design documents
- [ ] Document that syntax spans operate on decoded text and that encoding detection occurs before highlighting
- [ ] Add a cross-feature test note for opening, editing, saving, and reopening a syntax-colored non-ASCII file without changing its encoding unexpectedly
- [ ] Update documentation in the same change whenever a language contract, extension mapping, token role, or known limitation changes

## Milestone 7: Multi-language syntax-coloring foundation

Keep this milestone limited to shared infrastructure and preservation of existing C behavior. Do not add C++, Markdown, or C# scanning here.

### Language model and registry

- [ ] Introduce a small `SupportedLanguage` value that includes `PlainText`, `C`, `Cpp`, `Markdown`, and `CSharp`
- [ ] Introduce a `LanguageDefinition` containing the language value, status-bar display name, supported extensions, and optional `ISyntaxHighlighter`
- [ ] Add one language registry as the authoritative extension-to-language mapping
- [ ] Make extension matching case-insensitive and include a `Plain Text` fallback definition
- [ ] Register only highlighters that have actually been implemented
- [ ] Keep `.c` and `.h` mapped to C and verify that existing C detection does not change
- [ ] Replace separate highlighter and status-name decisions with one resolved `LanguageDefinition`
- [ ] Re-resolve the language after Open, Save As, New, and any other current-file-path change

### Scanner composition and span correctness

- [ ] Keep separate highlighter classes for each language
- [ ] Prefer small composed scanner helpers over a shared C-like base class
- [ ] Extract only proven lexical helpers, such as identifier characters, line-boundary handling, escaped delimiters, and longest-match operators
- [ ] Keep language keyword, built-in type, literal, directive, and declaration rules inside the corresponding highlighter
- [ ] Define a common helper for adding spans only when they are valid and do not overlap an already-claimed region
- [ ] Add development-time validation for positive lengths, source bounds, ascending order, and non-overlap
- [ ] Ensure every scanner advances on malformed or incomplete input and cannot loop indefinitely
- [ ] Keep malformed and ambiguous source text deterministic and safe rather than attempting compiler-level recovery

### Token roles and palettes

- [ ] Add new token roles only when an implemented language needs them
- [ ] Require every new token role to be mapped explicitly in both light and dark palettes in the same change
- [ ] Define a deliberate fallback or failure behavior for an unmapped token role instead of silently using a language-specific color
- [ ] Verify that no highlighter or scanner references `Color` or an editor theme

### RichTextBox integration and performance baseline

- [ ] Route all implemented languages through the existing syntax-color application path
- [ ] Preserve the selection start, selection length, and caret position while recoloring
- [ ] Preserve the editor viewport or first visible line while recoloring
- [ ] Suppress redraw during a formatting pass and restore redraw in a `finally` path
- [ ] Ensure formatting-only changes do not mark the document dirty or replace the user's text undo history
- [ ] Ensure highlighting does not disrupt Open, Save, Save As, New, Find, Replace, word wrap, line numbers, or status information
- [ ] Add a short text-change debounce of approximately 100–200 ms while keeping file-type and theme changes immediate
- [ ] Cancel pending highlighting work safely when the form or editor is disposed
- [ ] Record highlighting time for representative small, medium, and large files before considering visible-range or incremental processing
- [ ] Defer cached lexical state, visible-range scanning, background scanning, and incremental highlighting until measurements show a practical need

### Foundation verification

- [ ] Build the project using the target framework declared in `TxtItNow.csproj`
- [ ] Add focused checks for registry resolution, unknown extensions, uppercase extensions, and `.h` files
- [ ] Add focused checks that sample C input still produces ordered, non-overlapping spans with the same intended roles
- [ ] Manually verify caret, selection, viewport, dirty state, undo, find/replace, line numbers, and status information after repeated recoloring
- [ ] Update `syntax-coloring.md`, `syntax-languages.md`, and `README.md` to match the implemented foundation

## Milestone 8: C++ syntax coloring

Implement only C++ in this milestone. Preserve the C scanner as a separate highlighter and share only the small scanner helpers proven useful by both languages.

### C++ language contract and detection

- [ ] Finalize the documented C++23 first-pass lexical contract before implementation
- [ ] Map `.cpp`, `.cc`, `.cxx`, `.c++`, `.hpp`, `.hh`, `.hxx`, and `.h++` to `C++`
- [ ] Keep `.c` and ambiguous `.h` files mapped to C
- [ ] Verify that Save As between C and C++ extensions updates both the active highlighter and the status-bar language name

### C++ scanner

- [ ] Add a dedicated `CppSyntaxHighlighter` that implements `ISyntaxHighlighter`
- [ ] Support C-compatible line comments, block comments, identifiers, character literals, string literals, numbers, and operators
- [ ] Support the documented C++ keyword set and common built-in type names
- [ ] Recognize preprocessor directives only when `#` is the first non-whitespace character on a logical line
- [ ] Keep backslash-continued preprocessor lines in the `Preprocessor` role
- [ ] Support ordinary, wide, UTF-8, UTF-16, and UTF-32 character and string literal prefixes
- [ ] Support C++ raw string literals with valid custom delimiters and termination by the matching delimiter
- [ ] Treat an unterminated block comment, quoted literal, raw string, or continued directive deterministically through end of input
- [ ] Support binary, octal, decimal, and hexadecimal integer literals
- [ ] Support decimal and hexadecimal floating-point literals, digit separators, standard suffixes, and user-defined literal suffixes
- [ ] Match multi-character operators before their shorter prefixes
- [ ] Treat `::`, template angle brackets, pointer/reference punctuation, and other punctuation lexically without attempting semantic parsing
- [ ] Color known built-in and standard type names as `TypeName`
- [ ] Apply conservative type-name heuristics after `class`, `struct`, `union`, `enum`, `using`, and `typedef`
- [ ] Apply a conservative function-name heuristic for identifiers followed by a call or declaration parenthesis
- [ ] Leave uncertain namespace-qualified names, template arguments, macros, aliases, and declarations as plain identifiers
- [ ] Defer semantic template parsing, namespace and scope resolution, macro expansion, contextual type inference, and nested language highlighting

### C++ verification and documentation

- [ ] Add focused scanner cases for comments, continued directives, prefixed literals, raw strings with custom delimiters, numeric forms, operators, templates, and malformed input
- [ ] Verify that C++ spans are ordered, in bounds, non-overlapping, and deterministic
- [ ] Add a mixed C/C++ regression case to ensure shared helpers do not change existing C behavior
- [ ] Manually verify Open, Save As, theme reapplication, status-bar detection, editing, undo, find/replace, line numbers, and non-ASCII text in C++ files
- [ ] Update the language status table, C++ limitations, README supported-language list, and manual-test notes

## Milestone 9: Markdown syntax coloring

Implement only Markdown in this milestone. Use a deterministic Markdown-aware scanner rather than regular-expression passes over the whole document.

### Markdown roles and detection

- [ ] Finalize the documented pragmatic Markdown first-pass contract before implementation
- [ ] Add theme-independent `Heading`, `Emphasis`, `Link`, `Code`, and `Quote` token roles
- [ ] Map every new Markdown role explicitly in both the light and dark palettes
- [ ] Map `.md` and `.markdown` to `Markdown`
- [ ] Verify that Save As to or from a Markdown extension updates both the highlighter and status-bar language name

### Markdown scanner structure and precedence

- [ ] Add a dedicated `MarkdownSyntaxHighlighter` that implements `ISyntaxHighlighter`
- [ ] Scan block constructs by line before scanning inline constructs inside unclaimed text
- [ ] Give fenced code blocks and recognized HTML blocks precedence over other Markdown rules
- [ ] Track fenced-code and HTML-block state across lines
- [ ] Use look-behind or deferred emission for Setext headings while preserving ordered span output
- [ ] Scan inline code before links, images, emphasis, and strong emphasis
- [ ] Prevent inline rules from emitting spans inside fenced code, inline code, or protected HTML regions
- [ ] Treat escaped punctuation as literal text for the supported inline rules
- [ ] Validate all emitted spans for UTF-16 bounds, ascending order, and non-overlap

### Markdown block constructs

- [ ] Support ATX headings with up to three leading spaces, one to six `#` markers, and the required following space or end of line
- [ ] Support optional closing markers on ATX headings
- [ ] Support Setext headings using `=` or `-` underline lines after eligible nonblank text
- [ ] Resolve the Setext-heading versus thematic-break ambiguity consistently with the documented first-pass rules
- [ ] Support backtick and tilde fenced code blocks with at least three opening markers
- [ ] Require a closing fence to use the same marker and at least the opening fence length
- [ ] Treat an unclosed fenced code block as code through end of input
- [ ] Treat the fence info string as code without attempting to select another highlighter
- [ ] Support one or more block-quote markers and color the markers with the `Quote` role
- [ ] Support unordered and ordered list markers after permitted indentation
- [ ] Support thematic breaks without confusing them with list markers or Setext underlines
- [ ] Recognize a conservative subset of HTML comments, declarations, and common block-tag regions as protected plain text

### Markdown inline constructs

- [ ] Support variable-length inline backtick code spans and require a closing run of the same length
- [ ] Normalize neither source text nor span positions when handling whitespace inside code spans
- [ ] Support inline links and images with labels and destinations
- [ ] Support one level of balanced parentheses in inline link destinations and handle escaped delimiters
- [ ] Support full and collapsed reference-link syntax without resolving reference definitions semantically
- [ ] Support same-line, non-nested emphasis and strong emphasis with `*` and `_`
- [ ] Avoid treating intraword underscores and unmatched delimiters as emphasis
- [ ] Prefer strong-emphasis delimiters before single-emphasis delimiters when both can start at the same position
- [ ] Leave unmatched or ambiguous Markdown punctuation as plain text

### Deliberately deferred Markdown features

- [ ] Document nested syntax highlighting inside fenced code blocks as deferred
- [ ] Document full CommonMark delimiter-stack behavior and deeply nested emphasis as deferred
- [ ] Document semantic reference-link resolution, autolinks, tables, task lists, footnotes, and other flavor-specific extensions as deferred
- [ ] Document complete CommonMark HTML-block classification and nested HTML syntax coloring as deferred

### Markdown verification and documentation

- [ ] Add focused scanner cases for ATX and Setext headings, emphasis, strong emphasis, code spans, fences, links, images, quotes, lists, thematic breaks, HTML blocks, escapes, and malformed input
- [ ] Add ambiguity cases for Setext headings versus thematic breaks and emphasis versus intraword punctuation
- [ ] Verify that Markdown spans are ordered, in bounds, non-overlapping, and deterministic
- [ ] Manually verify editing near multiline fences and HTML blocks, undo, find/replace, Save As detection, viewport preservation, and theme reapplication
- [ ] Update the language status table, Markdown limitations, README supported-language list, and manual-test notes

## Milestone 10: C# syntax coloring

Implement only C# in this milestone. Keep the C# scanner separate from C and C++, using composition for shared lexical helpers instead of a C-like inheritance hierarchy.

### C# roles, contract, and detection

- [ ] Finalize the documented C# 14 first-pass lexical contract before implementation
- [ ] Add a theme-independent `Annotation` token role for C# attribute names and later Java annotations and Python decorators
- [ ] Map `Annotation` explicitly in both the light and dark palettes
- [ ] Map `.cs` to `C#`
- [ ] Verify that Save As to or from `.cs` updates both the active highlighter and status-bar language name

### C# comments and directives

- [ ] Add a dedicated `CSharpSyntaxHighlighter` that implements `ISyntaxHighlighter`
- [ ] Support line comments and multiline block comments
- [ ] Recognize preprocessor directives only when `#` is the first non-whitespace character on a line
- [ ] Treat directive lines as `Preprocessor` without evaluating conditional-compilation expressions
- [ ] Handle unterminated block comments and incomplete directives deterministically through their defined boundary

### C# literals

- [ ] Support character literals and escaped characters
- [ ] Support normal string literals and standard escape sequences
- [ ] Support verbatim strings, including doubled quote escaping
- [ ] Support interpolated normal and verbatim strings in either valid prefix order
- [ ] Support single-line and multiline raw string literals with quote delimiters of three or more characters
- [ ] Support interpolated raw strings with multiple `$` prefixes and the corresponding brace-count rules
- [ ] Color each interpolated string as a string in the first pass and defer nested expression highlighting inside interpolation holes
- [ ] Treat unterminated normal, verbatim, interpolated, and raw strings deterministically without scanning past their permitted boundary
- [ ] Support decimal, hexadecimal, and binary integers; real literals; digit separators; exponents; and standard numeric suffixes

### C# identifiers and contextual constructs

- [ ] Support reserved and documented contextual keywords using conservative lexical context
- [ ] Color C# predefined types as `TypeName`
- [ ] Apply conservative type-name heuristics after `class`, `struct`, `interface`, `record`, `enum`, and `delegate`
- [ ] Apply a conservative function-name heuristic for declarations and invocations while excluding control-flow keywords
- [ ] Recognize attribute lists and color attribute identifiers as `Annotation` while allowing their argument expressions to use ordinary token rules
- [ ] Match multi-character operators before their shorter prefixes
- [ ] Leave uncertain generic type arguments, LINQ contextual keywords, pattern variables, aliases, namespaces, and semantic symbol classifications as plain identifiers
- [ ] Defer semantic parsing, symbol resolution, inactive preprocessor-region dimming, XML documentation parsing, and nested interpolation-expression highlighting

### C# verification and documentation

- [ ] Add focused scanner cases for comments, directives, characters, normal strings, verbatim strings, all interpolated-string prefix forms, raw strings, numeric suffixes, attributes, contextual keywords, and malformed input
- [ ] Add raw-string cases with varying quote counts and interpolated raw-string cases with varying dollar and brace counts
- [ ] Verify that C# spans are ordered, in bounds, non-overlapping, and deterministic
- [ ] Add mixed C, C++, and C# regression cases to ensure shared helpers do not merge language-specific behavior
- [ ] Manually verify Open, Save As, editing, undo, find/replace, viewport preservation, status-bar detection, theme reapplication, and non-ASCII identifiers
- [ ] Update the language status table, C# limitations, README supported-language list, and manual-test notes

### Multi-language checkpoint before Dark Mode and Tabs

- [ ] Confirm that C, C++, Markdown, and C# are each selected through the same language registry
- [ ] Confirm that each language decision supplies both the active highlighter and exactly one status-bar display name
- [ ] Confirm case-insensitive extension handling, `.h` as C, and `Plain Text` fallback behavior
- [ ] Confirm that Open, Save As, New, and recent-file workflows re-resolve the active language correctly
- [ ] Confirm that every scanner returns deterministic, ordered, in-bounds, non-overlapping spans for valid and malformed samples
- [ ] Confirm that every implemented token role has an explicit light- and dark-palette mapping
- [ ] Confirm that no scanner contains language-specific color values or depends on the active theme
- [ ] Confirm caret, selection, viewport, dirty state, undo history, find/replace, line numbers, word wrap, encoding, and status information remain correct for every supported language
- [ ] Measure full-document highlighting and debounced typing on representative small, medium, and large files for all four languages
- [ ] Record any measured threshold or correctness issue that must be fixed before the editor is duplicated across tabs
- [ ] Confirm `syntax-coloring.md`, `syntax-languages.md`, `file-encoding.md`, `README.md`, and `AGENTS.md` agree with the implemented behavior and known limitations
- [ ] Do not begin Milestone 11 or Milestone 12 until the checkpoint has no unresolved correctness regressions

## Milestone 11: Dark Mode

Keep this milestone limited to completing the user-facing theme feature. Do not add another language or begin the tabbed document model here.

- [ ] Add `View > Dark Mode` to the existing `View` menu
- [ ] Store the current `EditorThemeMode` in application state
- [ ] Define the light and dark UI colors in one theme-application path
- [ ] Apply theme colors to the main form, editor, line-number gutter, menu bar, context menus, and status UI where practical
- [ ] Switch the active `SyntaxColorPalette` with the UI theme
- [ ] Reapply syntax coloring for C, C++, Markdown, C#, and plain text after a theme change
- [ ] Verify every implemented token role remains readable and distinct in both palettes
- [ ] Restore the default light theme when Dark Mode is toggled off
- [ ] Keep the `Dark Mode` menu item's checked state synchronized with application state
- [ ] Preserve the selected theme during the application session
- [ ] Defer cross-session theme persistence to a later settings milestone
- [ ] Preserve caret, selection, viewport, dirty state, and undo history while switching themes
- [ ] Verify Open, Save, Find/Replace, word wrap, line numbers, status information, and encoding workflows in both themes
- [ ] Update `README.md` and the syntax-coloring manual-test notes for switching between light and dark mode

## Milestone 12: Tabs

Keep this milestone limited to the tabbed document model and adapting existing features to the active document. Do not add another language scanner in the same change.

### Per-tab document model

- [ ] Create a document state model for each tab
- [ ] Track editor control, file path, dirty state, encoding, detected language, and syntax-highlighting state separately for each tab
- [ ] Associate each editor with its own line-number gutter or editor container
- [ ] Add a tab container to the main window and move the existing document into the first tab
- [ ] Update each tab title to show its file name and an unsaved-change marker
- [ ] Keep one authoritative active-document accessor for menu commands, status updates, and keyboard shortcuts

### Tab creation, switching, and closing

- [ ] Add `File > New Tab` and create a new blank editor document in a new tab
- [ ] Keep `File > New` scoped to resetting the active document after the existing unsaved-changes prompt
- [ ] Add a close button or close command for the active tab
- [ ] Prompt to save when closing a dirty tab
- [ ] Close only the selected tab when multiple tabs are open
- [ ] Close the application when closing the only remaining tab
- [ ] Prompt for every dirty document that could be lost when exiting the application
- [ ] Cancel a close or application exit without losing any tab content when the user cancels a save prompt
- [ ] Cancel and dispose pending syntax-highlighting work when a tab closes
- [ ] Add keyboard shortcuts for creating a tab, closing the active tab, and switching between tabs

### Existing workflow integration

- [ ] Update `File > Open` and recent-files commands to load into the active tab using a documented replacement policy
- [ ] Update `File > Save` and `File > Save As` to operate only on the active tab
- [ ] Re-resolve syntax language and status name from each tab's file path after Open, Save As, and New
- [ ] Update the main window title, line and column, encoding, language, and dirty indicators from the active tab
- [ ] Route Undo, Cut, Copy, Paste, Select All, Find, Replace, word wrap, font, smart indent, and line-number commands to the active editor
- [ ] Apply light/dark theme changes to every open tab and reapply each tab's active syntax palette
- [ ] Preserve each tab's independent selection, viewport, undo history, and dirty state while switching tabs
- [ ] Ensure background tabs do not perform unnecessary full-document highlighting while inactive
- [ ] Defer tab reordering, detachable tabs, split views, and restoring open tabs between sessions

### Tabs verification and documentation

- [ ] Add manual tests for creating, opening, saving, switching, and closing clean and dirty tabs
- [ ] Test multiple tabs containing C, C++, Markdown, C#, plain text, and files with different encodings
- [ ] Test Save As language changes, theme changes, find/replace, line numbers, and status updates independently in multiple tabs
- [ ] Test closing one dirty tab, closing the only tab, and exiting with multiple dirty tabs, including Cancel paths
- [ ] Update `README.md`, `AGENTS.md`, syntax-coloring integration notes, encoding integration notes, and manual-test documentation for the tabbed document model
