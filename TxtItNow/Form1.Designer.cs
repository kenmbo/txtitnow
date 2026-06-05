namespace TxtItNow;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private TextBox editorTextBox;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        editorTextBox = new TextBox();
        SuspendLayout();
        // 
        // editorTextBox
        // 
        editorTextBox.Multiline = true;
        editorTextBox.Dock = DockStyle.Fill;
        editorTextBox.Name = "editorTextBox";
	editorTextBox.ScrollBars = ScrollBars.Vertical;
	editorTextBox.WordWrap = true;
        editorTextBox.Size = new Size(1000, 700);
        editorTextBox.TabIndex = 0;
        // 
        // Form1
        // 
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 700);
        Text = "TxtItNow";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
