namespace TxtItNow;

public partial class Form1 : Form
{
    private string? currentFilePath = null;
    private bool isDocumentDirty;

    public Form1()
    {
        InitializeComponent();
        UpdateWindowTitle();
    }

    private void EditorTextBox_TextChanged(object sender, EventArgs e)
    {
        isDocumentDirty = true;
        UpdateWindowTitle();
    }

    private void NewToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowNotImplementedMessage("New");
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowNotImplementedMessage("Open");
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

    private void ShowNotImplementedMessage(string commandName)
    {
        MessageBox.Show(
            $"{commandName} is not implemented yet.",
            "TxtItNow",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void UpdateWindowTitle()
    {
        string documentName = currentFilePath is null
            ? "Untitled"
            : Path.GetFileName(currentFilePath);

        string dirtyMarker = isDocumentDirty ? "*" : string.Empty;
        Text = $"{dirtyMarker}{documentName} - TxtItNow";
    }
}
