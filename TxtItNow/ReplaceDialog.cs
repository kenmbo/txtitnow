namespace TxtItNow;

internal sealed class ReplaceDialog : Form
{
    private readonly TextBox findTextBox;
    private readonly TextBox replaceTextBox;

    public ReplaceDialog(string initialSearchText, string initialReplacementText)
    {
        Text = "Replace";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(390, 156);

        Label findLabel = new()
        {
            AutoSize = true,
            Location = new Point(12, 17),
            Text = "Find what:"
        };

        findTextBox = new TextBox()
        {
            Location = new Point(100, 14),
            Size = new Size(278, 23),
            Text = initialSearchText
        };

        Label replaceLabel = new()
        {
            AutoSize = true,
            Location = new Point(12, 52),
            Text = "Replace with:"
        };

        replaceTextBox = new TextBox()
        {
            Location = new Point(100, 49),
            Size = new Size(278, 23),
            Text = initialReplacementText
        };

        Button replaceButton = new()
        {
            DialogResult = DialogResult.OK,
            Location = new Point(204, 110),
            Size = new Size(84, 27),
            Text = "Replace"
        };

        Button cancelButton = new()
        {
            DialogResult = DialogResult.Cancel,
            Location = new Point(294, 110),
            Size = new Size(84, 27),
            Text = "Cancel"
        };

        AcceptButton = replaceButton;
        CancelButton = cancelButton;

        Controls.Add(findLabel);
        Controls.Add(findTextBox);
        Controls.Add(replaceLabel);
        Controls.Add(replaceTextBox);
        Controls.Add(replaceButton);
        Controls.Add(cancelButton);

        Shown += (_, _) =>
        {
            findTextBox.Focus();
            findTextBox.SelectAll();
        };
    }

    public string SearchText => findTextBox.Text;

    public string ReplacementText => replaceTextBox.Text;
}
