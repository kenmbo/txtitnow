namespace TxtItNow;

internal sealed class EditorRichTextBox : RichTextBox
{
    private const int WmSetRedraw = 0x000B;
    private const int WmHScroll = 0x0114;
    private const int WmVScroll = 0x0115;
    private const int WmMouseWheel = 0x020A;

    public event EventHandler? ViewportChanged;

    public void SetRedrawEnabled(bool enabled)
    {
        if (!IsHandleCreated)
        {
            return;
        }

        SendMessage(Handle, WmSetRedraw, enabled ? 1 : 0, 0);

        if (enabled)
        {
            Invalidate();
        }
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg is WmHScroll or WmVScroll or WmMouseWheel)
        {
            ViewportChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern nint SendMessage(nint windowHandle, int message, nint wParam, nint lParam);
}
