using System;
using System.Windows.Forms;

namespace Org.Benf.OleWoo.GuiMisc
{
    internal class UpdateSuspender : IDisposable
    {
        private readonly Action _eud;
        public UpdateSuspender(TreeView t)
        {
            t.BeginUpdate();
            _eud = t.EndUpdate;
        }
        public void Dispose()
        {
            _eud();
        }
    }
}
