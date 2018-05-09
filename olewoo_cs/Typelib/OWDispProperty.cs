using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWDispProperty : ITlibNode
    {
        private readonly string _name;
        private readonly VarDesc _vd;
        private readonly ITypeInfo _ti;
        private readonly IDLData _data;

        public OWDispProperty(ITlibNode parent, ITypeInfo ti, VarDesc vd)
        {
            Parent = parent;
            _name = ti.GetDocumentationById(vd.memid);
            _vd = vd;
            _ti = ti;
            _data = new IDLData(this);
        }

        public override string Name => _name;
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

        // ReSharper disable once UnusedMember.Local
        private string negStr(int x)
        {
            return (x < 0) ? ("0x" + x.ToString("X")) : x.ToString();
        }
        public void BuildIDLInto(IDLFormatter ih, bool embedded)
        {
            var lprops = GetAttributes();
            ih.AppendLine("[" + string.Join(", ", lprops.ToArray()) + "] ");
            // Prototype in a different line.
            var ed = _vd.elemDescVar;

            ed.tdesc.ComTypeNameAsString(_ti, ih);
//            if (memIdInSpecialRange)
//            {
//                ih.AddString(" " + _fd.callconv.ToString().Substring(2).ToLower());
//            }
            ih.AppendLine(" " + _name + ";");
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            BuildIDLInto(ih, false);
        }

        private bool MemIdInSpecialRange => (_vd.memid >= 0x60000000 && _vd.memid < 0x60020000);

        public override List<string> GetAttributes()
        {
            var lprops = new List<string>();
            if (!MemIdInSpecialRange)
            {
                lprops.Add("id(" + _vd.memid.PaddedHex() + ")");
            }
            var help = _ti.GetHelpDocumentationById(_vd.memid, out var context);
            //            if (0 != (_vd.wFuncFlags & FuncDesc.FuncFlags.FUNCFLAG_FRESTRICTED)) lprops.Add("restricted");
            //            if (0 != (_vd.wFuncFlags & FuncDesc.FuncFlags.FUNCFLAG_FHIDDEN)) lprops.Add("hidden");
            AddHelpStringAndContext(lprops, help, context);

            return lprops;
        }
    }
}