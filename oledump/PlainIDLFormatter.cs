using System;
using System.Text;
using Org.Benf.OleWoo;

namespace Org.Benf.OleDump
{
    class PlainIDLFormatter : IDLFormatter
    {
        private readonly StringBuilder _sb;
        bool _bPendingApplyTabs;

        public PlainIDLFormatter()
        {
            _sb = new StringBuilder();
            _bPendingApplyTabs = false;
        }

        public override void NewLine()
        {
            _sb.AppendLine("");
            _bPendingApplyTabs = true;
        }

        private void ApplyTabs()
        {
            _bPendingApplyTabs = false;
            if (TabDepth > 0)
            {
                var s = "";
                for (var x = 0; x < TabDepth; ++x)
                {
                    s += "\t";
                }
                _sb.Append(s);
            }
        }

        public override void AddString(String s)
        {
            if (_bPendingApplyTabs) ApplyTabs();
            _sb.Append(s);
        }

        public override void AddLink(String s, String o)
        {
            AddString(s);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}