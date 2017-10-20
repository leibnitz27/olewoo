using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class IDLGrabber : IDLFormatter_iop
    {
        public override void AddLink(string s, string s2)
        {
            Value += s;
        }
        public override void AddString(string s)
        {
            Value += s;
        }
        public string Value { get; private set; }
    }
}