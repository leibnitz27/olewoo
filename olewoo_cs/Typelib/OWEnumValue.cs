using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWEnumValue : ITlibNode
    {
        private readonly string _name;
        private readonly VarDesc _vd;
        private readonly ITypeInfo _ti;
        private readonly int _val;
        private readonly IDLData _data;

        public OWEnumValue(ITlibNode parent, ITypeInfo ti, VarDesc vd)
        {
            Parent = parent;
            _name = ti.GetDocumentationById(vd.memid);
            _val = (int)vd.varValue;
            _vd = vd;
            _ti = ti;
            _data = new IDLData(this);
        }

        public override string Name => "const int " + _name + " = " + _val;
        public override string ShortName => _name;
        public override string ObjectName => null;

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return false;
        }
        public override int ImageIndex => (int)ImageIndices.idx_const;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            return res;
        }

        private string negStr(int x)
        {
            return (x < 0) ? ("0x" + x.ToString("X")) : x.ToString();
        }
        public void BuildIDLInto(IDLFormatter ih, bool embedded, bool islast)
        {
            ih.AppendLine("const int " + _ti.GetDocumentationById(_vd.memid) + " = " + negStr(_val) + (embedded ? (islast ? "" : ",") : ";"));
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            BuildIDLInto(ih, false, false);
        }

        public override List<string> GetAttributes()
        {
            return new List<string>();
        }
        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterEnumValue(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitEnumValue(this);
            }
        }
    }
}