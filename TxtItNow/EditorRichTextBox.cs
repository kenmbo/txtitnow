namespace TxtItNow;

internal sealed class EditorRichTextBox : RichTextBox
{
    private const int WmHScroll = 0x0114;
    private const int WmVScroll = 0x0115;
    private const int WmMouseWheel = 0x020A;

    public event EventHandler? ViewportChanged;

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg is WmHScroll or WmVScroll or WmMouseWheel)
        {
            ViewportChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
