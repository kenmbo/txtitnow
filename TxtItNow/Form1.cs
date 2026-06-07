namespace TxtItNow;

public partial class Form1 : Form
{
    private string? currentFilePath;
    private bool isDocumentDirty;

    public Form1()
    {
        InitializeComponent();
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

    private void ShowNotImplementedMessage(string commandName)
    {
        MessageBox.Show(
            $"{commandName} is not implemented yet.",
            "TxtItNow",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}

