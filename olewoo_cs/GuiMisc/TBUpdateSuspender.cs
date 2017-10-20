using System;
using System.Windows.Forms;

namespace Org.Benf.OleWoo.GuiMisc
{
    internal class TbUpdateSuspender : IDisposable
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private readonly TextBoxBase _tbb;
        public TbUpdateSuspender(TextBoxBase tbb)
        {
            _tbb = tbb;
            SendMessage(_tbb.Handle, 0xb, (IntPtr)0, IntPtr.Zero); 
        }
        public void Dispose()
        {
            SendMessage(_tbb.Handle, 0xb, (IntPtr)1, IntPtr.Zero);
            _tbb.Invalidate();
        }
    }
}