namespace TxtItNow;

public partial class Form1 : Form
{
    private const string ApplicationName = "TxtItNow";
    private const string ApplicationIconResourceName = "TxtItNow.app.ico";
    private const int MaxRecentFiles = 5;

    private string? currentFilePath;
    private bool isDocumentDirty;
    private bool isWordWrapEnabled = true;
    private bool isLineNumbersEnabled = true;
    private Font? selectedEditorFont;
    private string lastFindText = string.Empty;
    private string lastReplaceText = string.Empty;
    private readonly List<string> recentFilePaths = new();

    public Form1()
    {
        InitializeComponent();
        SetApplicationIcon();
        SetCurrentFilePath(null);
        ApplyWordWrapSetting();
        ApplyLineNumbersSetting();
        UpdateStatusBar();
        UpdateRecentFilesMenu();
    }

    private void EditorTextBox_TextChanged(object sender, EventArgs e)
    {
        MarkDocumentDirty();
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private void EditorTextBox_KeyUp(object sender, KeyEventArgs e)
    {
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private void EditorTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if ((e.Control && e.KeyCode == Keys.V) || (e.Shift && e.KeyCode == Keys.Insert))
        {
            PasteClipboardText();
            e.SuppressKeyPress = true;
        }
    }

    private void EditorTextBox_MouseUp(object sender, MouseEventArgs e)
    {
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private void EditorTextBox_ViewportChanged(object? sender, EventArgs e)
    {
        UpdateLineNumberGutter();
    }

    private void NewToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!ConfirmDiscardUnsavedChanges())
        {
            return;
        }

        editorTextBox.Clear();
        editorTextBox.ClearUndo();
        SetCurrentFilePath(null);
        MarkDocumentClean();
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!ConfirmDiscardUnsavedChanges())
        {
            return;
        }

        using OpenFileDialog openFileDialog = new()
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Open"
        };

        if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        OpenDocumentFromPath(openFileDialog.FileName);
    }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (currentFilePath is null)
        {
            SaveDocumentAs();
            return;
        }

        SaveDocumentToPath(currentFilePath);
    }

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveDocumentAs();
    }

    private void RecentFileToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        if (sender is not ToolStripMenuItem { Tag: string filePath })
        {
            return;
        }

        if (!ConfirmDiscardUnsavedChanges())
        {
            return;
        }

        if (!OpenDocumentFromPath(filePath))
        {
            RemoveRecentFile(filePath);
        }
    }

    private bool SaveDocumentAs()
    {
        using SaveFileDialog saveFileDialog = new()
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Save As"
        };

        if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return false;
        }

        return SaveDocumentToPath(saveFileDialog.FileName);
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!ConfirmDiscardUnsavedChanges())
        {
            e.Cancel = true;
        }
    }

    private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (editorTextBox.CanUndo)
        {
            editorTextBox.Undo();
        }

        UpdateEditMenuItemStates();
        UpdateStatusBar();
    }

    private void EditToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
        UpdateEditMenuItemStates();
    }

    private void CutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        editorTextBox.Cut();
        UpdateEditMenuItemStates();
        UpdateStatusBar();
    }

    private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
    {
        editorTextBox.Copy();
        UpdateEditMenuItemStates();
        UpdateStatusBar();
    }

    private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
        PasteClipboardText();
    }

    private void FindToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using FindDialog findDialog = new(lastFindText);

        if (findDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        lastFindText = findDialog.SearchText;
        TryFindInDocument(lastFindText);
    }

    private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using ReplaceDialog replaceDialog = new(lastFindText, lastReplaceText);

        if (replaceDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        lastFindText = replaceDialog.SearchText;
        lastReplaceText = replaceDialog.ReplacementText;
        ReplaceInDocument(lastFindText, lastReplaceText);
    }

    private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
        editorTextBox.SelectAll();
        UpdateEditMenuItemStates();
        UpdateStatusBar();
    }

    private void WordWrapToolStripMenuItem_Click(object sender, EventArgs e)
    {
        isWordWrapEnabled = !isWordWrapEnabled;
        ApplyWordWrapSetting();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private void LineNumbersToolStripMenuItem_Click(object sender, EventArgs e)
    {
        isLineNumbersEnabled = !isLineNumbersEnabled;
        ApplyLineNumbersSetting();
    }

    private void FontToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using FontDialog fontDialog = new()
        {
            Font = selectedEditorFont ?? editorTextBox.Font
        };

        if (fontDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        Font? previousSelectedEditorFont = selectedEditorFont;
        selectedEditorFont = (Font)fontDialog.Font.Clone();
        editorTextBox.Font = selectedEditorFont;
        previousSelectedEditorFont?.Dispose();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show(
            this,
            $"{ApplicationName}{Environment.NewLine}Version 1.0{Environment.NewLine}{Environment.NewLine}A small Notepad-like text editor built with C# and WinForms.",
            $"About {ApplicationName}",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void MarkDocumentClean()
    {
        isDocumentDirty = false;
        UpdateWindowTitle();
    }

    private void MarkDocumentDirty()
    {
        isDocumentDirty = true;
        UpdateWindowTitle();
    }

    private void SetCurrentFilePath(string? filePath)
    {
        currentFilePath = filePath;
        UpdateWindowTitle();
    }

    private void UpdateEditMenuItemStates()
    {
        undoToolStripMenuItem.Enabled = editorTextBox.CanUndo;
        bool hasSelection = editorTextBox.SelectionLength > 0;
        cutToolStripMenuItem.Enabled = hasSelection;
        copyToolStripMenuItem.Enabled = hasSelection;
    }

    private void ApplyWordWrapSetting()
    {
        editorTextBox.WordWrap = isWordWrapEnabled;
        editorTextBox.ScrollBars = isWordWrapEnabled
            ? ScrollBars.Vertical
            : ScrollBars.Both;
        wordWrapToolStripMenuItem.Checked = isWordWrapEnabled;
    }

    private void ApplyLineNumbersSetting()
    {
        lineNumberGutterPanel.Visible = isLineNumbersEnabled;
        lineNumbersToolStripMenuItem.Checked = isLineNumbersEnabled;
        UpdateLineNumberGutter();
    }

    private void PasteClipboardText()
    {
        if (!Clipboard.ContainsText(TextDataFormat.UnicodeText))
        {
            return;
        }

        string clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
        editorTextBox.SelectedText = NormalizeLineEndings(clipboardText);
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private bool TryFindInDocument(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            MessageBox.Show(
                this,
                "Enter text to find.",
                ApplicationName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return false;
        }

        string editorText = editorTextBox.Text;
        int startIndex = editorTextBox.SelectionStart + editorTextBox.SelectionLength;
        int matchIndex = editorText.IndexOf(searchText, startIndex, StringComparison.CurrentCultureIgnoreCase);

        if (matchIndex == -1 && startIndex > 0)
        {
            matchIndex = editorText.IndexOf(searchText, 0, StringComparison.CurrentCultureIgnoreCase);
        }

        if (matchIndex == -1)
        {
            MessageBox.Show(
                this,
                $"Cannot find \"{searchText}\".",
                ApplicationName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return false;
        }

        editorTextBox.Focus();
        editorTextBox.Select(matchIndex, searchText.Length);
        editorTextBox.ScrollToCaret();
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
        return true;
    }

    private void ReplaceInDocument(string searchText, string replacementText)
    {
        if (!SelectionMatches(searchText) && !TryFindInDocument(searchText))
        {
            return;
        }

        int replacementStart = editorTextBox.SelectionStart;
        editorTextBox.SelectedText = replacementText;
        editorTextBox.Select(replacementStart, replacementText.Length);
        editorTextBox.ScrollToCaret();
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
    }

    private bool SelectionMatches(string searchText)
    {
        return editorTextBox.SelectionLength == searchText.Length
            && string.Equals(editorTextBox.SelectedText, searchText, StringComparison.CurrentCultureIgnoreCase);
    }

    private static string NormalizeLineEndings(string text)
    {
        return text
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Replace("\n", Environment.NewLine);
    }

    private void SetApplicationIcon()
    {
        using Stream? iconStream = typeof(Form1).Assembly.GetManifestResourceStream(ApplicationIconResourceName);

        if (iconStream is null)
        {
            return;
        }

        using Icon applicationIcon = new(iconStream);
        Icon = (Icon)applicationIcon.Clone();
    }

    private void UpdateStatusBar()
    {
        int currentLineIndex = editorTextBox.GetLineFromCharIndex(editorTextBox.SelectionStart);
        int lineNumber = currentLineIndex + 1;
        int lineStartIndex = editorTextBox.GetFirstCharIndexFromLine(currentLineIndex);
        int columnNumber = editorTextBox.SelectionStart - lineStartIndex + 1;

        editorStatusLabel.Text = $"Ln {lineNumber}, Col {columnNumber}";
    }

    private void UpdateLineNumberGutter()
    {
        if (!isLineNumbersEnabled)
        {
            return;
        }

        int lineCount = Math.Max(1, editorTextBox.Lines.Length);
        int digitCount = lineCount.ToString().Length;
        int gutterWidth = Math.Max(48, TextRenderer.MeasureText(new string('9', digitCount), editorTextBox.Font).Width + 20);

        if (lineNumberGutterPanel.Width != gutterWidth)
        {
            lineNumberGutterPanel.Width = gutterWidth;
        }

        lineNumberGutterPanel.Invalidate();
    }

    private void LineNumberGutterPanel_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(SystemColors.Control);

        if (!isLineNumbersEnabled)
        {
            return;
        }

        int firstVisibleCharIndex = editorTextBox.GetCharIndexFromPosition(new Point(0, 0));
        int firstVisibleLineIndex = editorTextBox.GetLineFromCharIndex(firstVisibleCharIndex);
        int lastVisibleCharIndex = editorTextBox.GetCharIndexFromPosition(new Point(editorTextBox.ClientSize.Width - 1, editorTextBox.ClientSize.Height - 1));
        int lastVisibleLineIndex = Math.Max(firstVisibleLineIndex, editorTextBox.GetLineFromCharIndex(lastVisibleCharIndex));
        int currentLineIndex = editorTextBox.GetLineFromCharIndex(editorTextBox.SelectionStart);

        using Brush textBrush = new SolidBrush(SystemColors.GrayText);
        using Font currentLineFont = new(editorTextBox.Font, FontStyle.Bold);

        for (int lineIndex = firstVisibleLineIndex; lineIndex <= lastVisibleLineIndex; lineIndex++)
        {
            int lineStartIndex = editorTextBox.GetFirstCharIndexFromLine(lineIndex);

            if (lineStartIndex < 0)
            {
                continue;
            }

            Point linePosition = editorTextBox.GetPositionFromCharIndex(lineStartIndex);
            string lineNumber = (lineIndex + 1).ToString();
            Font lineNumberFont = lineIndex == currentLineIndex
                ? currentLineFont
                : editorTextBox.Font;
            Size lineNumberSize = TextRenderer.MeasureText(lineNumber, lineNumberFont);
            float x = lineNumberGutterPanel.Width - lineNumberSize.Width - 8;

            e.Graphics.DrawString(lineNumber, lineNumberFont, textBrush, x, linePosition.Y);
        }
    }

    private bool OpenDocumentFromPath(string filePath)
    {
        if (!TryReadFile(filePath, out string fileContents))
        {
            return false;
        }

        editorTextBox.Text = fileContents;
        editorTextBox.ClearUndo();
        SetCurrentFilePath(filePath);
        MarkDocumentClean();
        AddRecentFile(filePath);
        UpdateEditMenuItemStates();
        UpdateStatusBar();
        UpdateLineNumberGutter();
        return true;
    }

    private bool TryReadFile(string filePath, out string fileContents)
    {
        try
        {
            fileContents = File.ReadAllText(filePath);
            return true;
        }
        catch (Exception ex)
        {
            fileContents = string.Empty;
            ShowFileError("open", filePath, ex);
            return false;
        }
    }

    private bool SaveDocumentToPath(string filePath)
    {
        try
        {
            File.WriteAllText(filePath, editorTextBox.Text);
        }
        catch (Exception ex)
        {
            ShowFileError("save", filePath, ex);
            return false;
        }

        SetCurrentFilePath(filePath);
        MarkDocumentClean();
        AddRecentFile(filePath);
        return true;
    }

    private void AddRecentFile(string filePath)
    {
        string normalizedFilePath = Path.GetFullPath(filePath);
        recentFilePaths.RemoveAll(path => string.Equals(path, normalizedFilePath, StringComparison.OrdinalIgnoreCase));
        recentFilePaths.Insert(0, normalizedFilePath);

        if (recentFilePaths.Count > MaxRecentFiles)
        {
            recentFilePaths.RemoveRange(MaxRecentFiles, recentFilePaths.Count - MaxRecentFiles);
        }

        UpdateRecentFilesMenu();
    }

    private void RemoveRecentFile(string filePath)
    {
        string normalizedFilePath = Path.GetFullPath(filePath);
        recentFilePaths.RemoveAll(path => string.Equals(path, normalizedFilePath, StringComparison.OrdinalIgnoreCase));
        UpdateRecentFilesMenu();
    }

    private void UpdateRecentFilesMenu()
    {
        recentFilesToolStripMenuItem.DropDownItems.Clear();

        if (recentFilePaths.Count == 0)
        {
            recentFilesToolStripMenuItem.DropDownItems.Add(noRecentFilesToolStripMenuItem);
            return;
        }

        for (int index = 0; index < recentFilePaths.Count; index++)
        {
            string filePath = recentFilePaths[index];
            ToolStripMenuItem recentFileMenuItem = new()
            {
                Text = $"&{index + 1} {filePath.Replace("&", "&&")}",
                Tag = filePath,
                ToolTipText = filePath
            };

            recentFileMenuItem.Click += RecentFileToolStripMenuItem_Click;
            recentFilesToolStripMenuItem.DropDownItems.Add(recentFileMenuItem);
        }
    }

    private bool ConfirmDiscardUnsavedChanges()
    {
        if (!isDocumentDirty)
        {
            return true;
        }

        DialogResult result = MessageBox.Show(
            "You have unsaved changes. Do you want to discard them?",
            ApplicationName,
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Warning);

        return result == DialogResult.OK;
    }

    private void ShowNotImplementedMessage(string commandName)
    {
        MessageBox.Show(
            $"{commandName} is not implemented yet.",
            ApplicationName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void ShowFileError(string action, string filePath, Exception ex)
    {
        MessageBox.Show(
            $"Could not {action} file:{Environment.NewLine}{filePath}{Environment.NewLine}{Environment.NewLine}{ex.Message}",
            ApplicationName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private void UpdateWindowTitle()
    {
        string documentName = currentFilePath is null
            ? "Untitled"
            : Path.GetFileName(currentFilePath);

        string dirtyMarker = isDocumentDirty ? "*" : string.Empty;
        Text = $"{dirtyMarker}{documentName} - {ApplicationName}";
    }
}
