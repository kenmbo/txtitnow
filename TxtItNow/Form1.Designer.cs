namespace TxtItNow;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private TextBox editorTextBox;
    private MenuStrip mainMenuStrip;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem newToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem saveAsToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;
    private ToolStripMenuItem undoToolStripMenuItem;
    private ToolStripMenuItem cutToolStripMenuItem;
    private ToolStripMenuItem copyToolStripMenuItem;
    private ToolStripMenuItem pasteToolStripMenuItem;
    private ToolStripMenuItem selectAllToolStripMenuItem;
    private StatusStrip editorStatusStrip;
    private ToolStripStatusLabel editorStatusLabel;

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
        mainMenuStrip = new MenuStrip();
        fileToolStripMenuItem = new ToolStripMenuItem();
        newToolStripMenuItem = new ToolStripMenuItem();
        openToolStripMenuItem = new ToolStripMenuItem();
        saveToolStripMenuItem = new ToolStripMenuItem();
        saveAsToolStripMenuItem = new ToolStripMenuItem();
        exitToolStripMenuItem = new ToolStripMenuItem();
        editToolStripMenuItem = new ToolStripMenuItem();
        undoToolStripMenuItem = new ToolStripMenuItem();
        cutToolStripMenuItem = new ToolStripMenuItem();
        copyToolStripMenuItem = new ToolStripMenuItem();
        pasteToolStripMenuItem = new ToolStripMenuItem();
        selectAllToolStripMenuItem = new ToolStripMenuItem();
        editorStatusStrip = new StatusStrip();
        editorStatusLabel = new ToolStripStatusLabel();
        SuspendLayout();
        // 
        // mainMenuStrip
        // 
        mainMenuStrip.Dock = DockStyle.Top;
        mainMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
        mainMenuStrip.Location = new Point(0, 0);
        mainMenuStrip.Name = "mainMenuStrip";
        mainMenuStrip.Size = new Size(1000, 24);
        mainMenuStrip.TabIndex = 0;
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        fileToolStripMenuItem.Size = new Size(37, 20);
        fileToolStripMenuItem.Text = "&File";
        // 
        // newToolStripMenuItem
        // 
        newToolStripMenuItem.Name = "newToolStripMenuItem";
        newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
        newToolStripMenuItem.Size = new Size(180, 22);
        newToolStripMenuItem.Text = "&New";
        newToolStripMenuItem.Click += NewToolStripMenuItem_Click;
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
        openToolStripMenuItem.Size = new Size(180, 22);
        openToolStripMenuItem.Text = "&Open";
        openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
        saveToolStripMenuItem.Size = new Size(180, 22);
        saveToolStripMenuItem.Text = "&Save";
        saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
        saveAsToolStripMenuItem.Size = new Size(180, 22);
        saveAsToolStripMenuItem.Text = "Save &As";
        saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
        exitToolStripMenuItem.Size = new Size(180, 22);
        exitToolStripMenuItem.Text = "E&xit";
        exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, selectAllToolStripMenuItem });
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Size = new Size(39, 20);
        editToolStripMenuItem.Text = "&Edit";
        editToolStripMenuItem.DropDownOpening += EditToolStripMenuItem_DropDownOpening;
        // 
        // undoToolStripMenuItem
        // 
        undoToolStripMenuItem.Enabled = false;
        undoToolStripMenuItem.Name = "undoToolStripMenuItem";
        undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
        undoToolStripMenuItem.Size = new Size(180, 22);
        undoToolStripMenuItem.Text = "&Undo";
        undoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
        // 
        // cutToolStripMenuItem
        // 
        cutToolStripMenuItem.Enabled = false;
        cutToolStripMenuItem.Name = "cutToolStripMenuItem";
        cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
        cutToolStripMenuItem.Size = new Size(180, 22);
        cutToolStripMenuItem.Text = "Cu&t";
        cutToolStripMenuItem.Click += CutToolStripMenuItem_Click;
        // 
        // copyToolStripMenuItem
        // 
        copyToolStripMenuItem.Enabled = false;
        copyToolStripMenuItem.Name = "copyToolStripMenuItem";
        copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
        copyToolStripMenuItem.Size = new Size(180, 22);
        copyToolStripMenuItem.Text = "&Copy";
        copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
        // 
        // pasteToolStripMenuItem
        // 
        pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
        pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
        pasteToolStripMenuItem.Size = new Size(180, 22);
        pasteToolStripMenuItem.Text = "&Paste";
        pasteToolStripMenuItem.Click += PasteToolStripMenuItem_Click;
        // 
        // selectAllToolStripMenuItem
        // 
        selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
        selectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
        selectAllToolStripMenuItem.Size = new Size(180, 22);
        selectAllToolStripMenuItem.Text = "Select &All";
        selectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
        // 
        // editorStatusStrip
        // 
        editorStatusStrip.Dock = DockStyle.Bottom;
        editorStatusStrip.Items.AddRange(new ToolStripItem[] { editorStatusLabel });
        editorStatusStrip.Location = new Point(0, 678);
        editorStatusStrip.Name = "editorStatusStrip";
        editorStatusStrip.Size = new Size(1000, 22);
        editorStatusStrip.TabIndex = 2;
        // 
        // editorStatusLabel
        // 
        editorStatusLabel.Name = "editorStatusLabel";
        editorStatusLabel.Size = new Size(63, 17);
        editorStatusLabel.Text = "Ln 1, Col 1";
        // 
        // editorTextBox
        // 
        editorTextBox.Dock = DockStyle.Fill;
        editorTextBox.Multiline = true;
        editorTextBox.Name = "editorTextBox";
        editorTextBox.ScrollBars = ScrollBars.Vertical;
        editorTextBox.Size = new Size(1000, 654);
        editorTextBox.TabIndex = 1;
        editorTextBox.WordWrap = true;
        editorTextBox.KeyUp += EditorTextBox_KeyUp;
        editorTextBox.MouseUp += EditorTextBox_MouseUp;
        editorTextBox.TextChanged += EditorTextBox_TextChanged;
        // 
        // Form1
        // 
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 700);
        Controls.Add(editorTextBox);
        Controls.Add(editorStatusStrip);
        Controls.Add(mainMenuStrip);
        MainMenuStrip = mainMenuStrip;
        Text = "TxtItNow";
        FormClosing += Form1_FormClosing;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
