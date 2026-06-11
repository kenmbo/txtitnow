namespace TxtItNow;

public partial class Form1 : Form
{
    private const string ApplicationName = "TxtItNow";

    private string? currentFilePath;
    private bool isDocumentDirty;

    public Form1()
    {
        InitializeComponent();
        SetCurrentFilePath(null);
    }

    private void EditorTextBox_TextChanged(object sender, EventArgs e)
    {
        MarkDocumentDirty();
    }

    private void NewToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!ConfirmDiscardUnsavedChanges())
        {
            return;
        }

        editorTextBox.Clear();
        SetCurrentFilePath(null);
        MarkDocumentClean();
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

        if (!TryReadFile(openFileDialog.FileName, out string fileContents))
        {
            return;
        }

        editorTextBox.Text = fileContents;
        SetCurrentFilePath(openFileDialog.FileName);
        MarkDocumentClean();
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
        return true;
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
