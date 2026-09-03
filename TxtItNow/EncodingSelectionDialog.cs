namespace TxtItNow;

internal sealed class EncodingSelectionDialog : Form
{
    private readonly ComboBox encodingComboBox;

    public EncodingSelectionDialog(
        string operationName,
        bool allowAutoDetect,
        TextFileEncoding selectedEncoding)
    {
        Text = $"{operationName} Encoding";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(390, 126);

        Label encodingLabel = new()
        {
            AutoSize = true,
            Location = new Point(12, 18),
            Text = "Encoding:"
        };

        encodingComboBox = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(82, 14),
            Size = new Size(296, 23)
        };

        if (allowAutoDetect)
        {
            encodingComboBox.Items.Add(new EncodingChoice("Auto-detect (recommended)", null));
        }

        foreach (TextFileEncoding encoding in TextFileEncoding.SupportedEncodings)
        {
            encodingComboBox.Items.Add(new EncodingChoice(encoding.DisplayName, encoding));
        }

        encodingComboBox.SelectedIndex = FindInitialSelectionIndex(allowAutoDetect, selectedEncoding);

        Button confirmButton = new()
        {
            DialogResult = DialogResult.OK,
            Location = new Point(204, 80),
            Size = new Size(84, 27),
            Text = operationName
        };

        Button cancelButton = new()
        {
            DialogResult = DialogResult.Cancel,
            Location = new Point(294, 80),
            Size = new Size(84, 27),
            Text = "Cancel"
        };

        AcceptButton = confirmButton;
        CancelButton = cancelButton;

        Controls.Add(encodingLabel);
        Controls.Add(encodingComboBox);
        Controls.Add(confirmButton);
        Controls.Add(cancelButton);
    }

    public TextFileEncoding? SelectedEncoding =>
        (encodingComboBox.SelectedItem as EncodingChoice)?.Encoding;

    private static int FindInitialSelectionIndex(
        bool allowAutoDetect,
        TextFileEncoding selectedEncoding)
    {
        if (allowAutoDetect)
        {
            return 0;
        }

        int selectedIndex = TextFileEncoding.SupportedEncodings
            .ToList()
            .FindIndex(encoding => ReferenceEquals(encoding, selectedEncoding));

        return Math.Max(0, selectedIndex);
    }

    private sealed record EncodingChoice(string DisplayName, TextFileEncoding? Encoding)
    {
        public override string ToString()
        {
            return DisplayName;
        }
    }
}
