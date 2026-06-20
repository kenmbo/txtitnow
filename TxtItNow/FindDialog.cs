namespace TxtItNow;

internal sealed class FindDialog : Form
{
    private readonly TextBox findTextBox;

    public FindDialog(string initialSearchText)
    {
        Text = "Find";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(360, 116);

        Label findLabel = new()
        {
            AutoSize = true,
            Location = new Point(12, 17),
            Text = "Find what:"
        };

        findTextBox = new TextBox()
        {
            Location = new Point(82, 14),
            Size = new Size(266, 23),
            Text = initialSearchText
        };

        Button findNextButton = new()
        {
            DialogResult = DialogResult.OK,
            Location = new Point(174, 70),
            Size = new Size(84, 27),
            Text = "Find Next"
        };

        Button cancelButton = new()
        {
            DialogResult = DialogResult.Cancel,
            Location = new Point(264, 70),
            Size = new Size(84, 27),
            Text = "Cancel"
        };

        AcceptButton = findNextButton;
        CancelButton = cancelButton;

        Controls.Add(findLabel);
        Controls.Add(findTextBox);
        Controls.Add(findNextButton);
        Controls.Add(cancelButton);

        Shown += (_, _) =>
        {
            findTextBox.Focus();
            findTextBox.SelectAll();
        };
    }

    public string SearchText => findTextBox.Text;
}
