using System;

namespace Org.Benf.OleWoo
{
    // ReSharper disable once InconsistentNaming
    public abstract class IDLFormatter : olewoo_interop.IDLFormatter_iop
    {
        protected int TabDepth { get; set; }
        public void Indent() { TabDepth++; }
        public void Dedent() { TabDepth--; }
        public abstract void NewLine();
        public void AppendLine(string s)
        {
            AddString(s);
            NewLine();
        }
    }

    internal class IDLHelperTab : IDisposable
    {
        private readonly IDLFormatter _ih;

        public IDLHelperTab(IDLFormatter ih)
        {
            ih.Indent();
            _ih = ih;
        }
        public void Dispose()
        {
            _ih.Dedent();
        }
    }
}
