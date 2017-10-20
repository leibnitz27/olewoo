using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Org.Benf.OleWoo
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref int lParam);

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int LoadTypeLib(string fileName, out ITypeLib typeLib);

        public static void SetTabs(this System.Windows.Forms.TextBox box, int nSpaces)
        {
            //EM_SETTABSTOPS - http://msdn.microsoft.com/en-us/library/bb761663%28VS.85%29.aspx
            var lParam = nSpaces * 4;  //Set tab size to 4 spaces
            SendMessage(box.Handle, 0x00CB, new IntPtr(1), ref lParam);
            box.Invalidate();
        }
        public static void SetTabs(this System.Windows.Forms.RichTextBox box, int nSpaces)
        {
            //EM_SETTABSTOPS - http://msdn.microsoft.com/en-us/library/bb761663%28VS.85%29.aspx
            var lParam = nSpaces * 4;  //Set tab size to 4 spaces
            SendMessage(box.Handle, 0x00CB, new IntPtr(1), ref lParam);
            box.Invalidate();
        }
    }
}
