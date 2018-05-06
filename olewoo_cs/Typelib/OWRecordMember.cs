using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWRecordMember : ITlibNode
    {
        private readonly string _type;
        private readonly string _name;

        public OWRecordMember(ITlibNode parent, ITypeInfo ti, VarDesc vd)
        {
            Parent = parent;
            _name = ti.GetDocumentationById(vd.memid);
            var ig = new IDLGrabber();
            vd.elemDescVar.tdesc.ComTypeNameAsString(ti, ig);
            _type = ig.Value;
        }
        public override string Name => _type + " " + _name;
        public override string ShortName => _name;
        public override string ObjectName => null;

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return false;
        }
        public override int ImageIndex => (int)ImageIndices.idx_strucmem;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            return res;
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            ih.AppendLine(Name + ";");
        }

        public override List<string> GetAttributes()
        {
            return new List<string>();
        }
    }
}