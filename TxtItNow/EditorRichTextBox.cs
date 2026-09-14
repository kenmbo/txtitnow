namespace TxtItNow;

internal sealed class EditorRichTextBox : RichTextBox
{
    private const int WmSetRedraw = 0x000B;
    private const int WmHScroll = 0x0114;
    private const int WmVScroll = 0x0115;
    private const int WmMouseWheel = 0x020A;
    private const int EmGetFirstVisibleLine = 0x00CE;
    private const int EmLineScroll = 0x00B6;

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

    public int GetFirstVisibleLineIndex()
    {
        return !IsHandleCreated
            ? 0
            : (int)SendMessage(Handle, EmGetFirstVisibleLine, 0, 0);
    }

    public void RestoreFirstVisibleLineIndex(int firstVisibleLineIndex)
    {
        if (!IsHandleCreated)
        {
            return;
        }

        int lineDelta = firstVisibleLineIndex - GetFirstVisibleLineIndex();

        if (lineDelta != 0)
        {
            SendMessage(Handle, EmLineScroll, 0, lineDelta);
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
