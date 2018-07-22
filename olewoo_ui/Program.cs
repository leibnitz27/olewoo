using System;
using System.Windows.Forms;

namespace Org.Benf.OleWoo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GuiElem.OleWoo());
        }
    }
}
