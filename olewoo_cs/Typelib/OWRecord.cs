using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWRecord : ITlibNode
    {
        private readonly string _name;
        private readonly ITypeInfo _ti;
        private readonly TypeAttr _ta;
        private readonly IDLData _data;

        public OWRecord(ITlibNode parent, ITypeInfo ti, TypeAttr ta)
        {
            Parent = parent;
            _ti = ti;
            _ta = ta;
            _name = _ti.GetName();
            _data = new IDLData(this);
        }
        public override string Name => "typedef struct " + _name;
        public override string ShortName => _name;
        public override string ObjectName => _name + "#s";

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return true;
        }
        public override int ImageIndex => (int)ImageIndices.idx_struct;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            for (var x = 0; x < _ta.cVars; ++x)
            {
                var vd = new VarDesc(_ti, x);
                res.Add(new OWRecordMember(this, _ti, vd));
            }
            return res;
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            ih.AppendLine("typedef struct tag" + _name + " {");
            using (new IDLHelperTab(ih))
            {
                Children.ForEach( x => x.BuildIDLInto(ih) );
            }
            ih.AppendLine("} " + _name + ";");
        }

        public override List<string> GetAttributes()
        {
            return new List<string>();
        }
    }
}