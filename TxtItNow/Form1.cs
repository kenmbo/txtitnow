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
        using OpenFileDialog openFileDialog = new()
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            Title = "Open"
        };

        if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        editorTextBox.Text = File.ReadAllText(openFileDialog.FileName);
	SetCurrentFilePath(openFileDialog.FileName);
	MarkDocumentClean();
    }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowNotImplementedMessage("Save");
    }

    private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowNotImplementedMessage("Save As");
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
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

    private bool ConfirmDiscardUnsavedChanges()
    {
        if (!isDocumentDirty)
        {
            return true;
        }

        DialogResult result = MessageBox.Show(
            "You have unsaved changes. Do you wanna to discard them?",
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

    private void UpdateWindowTitle()
    {
        string documentName = currentFilePath is null
            ? "Untitled"
            : Path.GetFileName(currentFilePath);

        string dirtyMarker = isDocumentDirty ? "*" : string.Empty;
        Text = $"{dirtyMarker}{documentName} - {ApplicationName}";
    }
}

