using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;
using IMPLTYPEFLAGS = System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWCoClass : ITlibNode
    {
        private readonly string _name;
        private readonly TypeAttr _ta;
        private readonly ITypeInfo _ti;
        private readonly IDLData _data;

        public OWCoClass(ITlibNode parent, ITypeInfo ti, TypeAttr ta)
        {
            Parent = parent;
            _name = ti.GetName();
            _ta = ta;
            _ti = ti;
            _data = new IDLData(this);
        }

        public override string Name => $"coclass{_name}";
        public override string ObjectName => $"{_name}#c";

        public override string ShortName => _name;
        public override List<string> GetAttributes()
        {
            var lprops = new List<string> { $"uuid({_ta.guid})" };
            var ta = new TypeAttr(_ti);
            lprops.Add($"version({ta.wMajorVerNum}.{ta.wMinorVerNum})");
            OWCustData.GetCustData(_ti, ref lprops);
            var help = _ti.GetHelpDocumentationById(-1, out var context);
            AddHelpStringAndContext(lprops, help, context);
            return lprops;
        }

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames) => true;

        public override int ImageIndex => (int)ImageIndices.idx_coclass;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            for (var x = 0; x < _ta.cImplTypes; ++x)
            {
                _ti.GetRefTypeOfImplType(x, out var href);
                _ti.GetRefTypeInfo(href, out var ti2);
                CommonBuildTlibNode(this, ti2, false, false, res);
            }
            return res;
        }

        public override void BuildIDLInto(IDLFormatter ih)
        {
            ih.AppendLine("[");
            var lprops = GetAttributes();
            for (var i = 0; i < lprops.Count; ++i)
            {
                ih.AppendLine("  " + lprops[i] + (i < (lprops.Count - 1) ? "," : ""));
            }
            ih.AppendLine("]");
            ih.AppendLine("coclass " + _name + " {");
            using (new IDLHelperTab(ih))
            {
                for (var x = 0; x < _ta.cImplTypes; ++x)
                {
                    _ti.GetRefTypeOfImplType(x, out var href);
                    _ti.GetRefTypeInfo(href, out var ti2);
                    _ti.GetImplTypeFlags(x, out var itypflags);
                    var res = new List<string>();
                    if (0 != (itypflags & IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT)) res.Add("default");
                    if (0 != (itypflags & IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE)) res.Add("source");

                    if (res.Count > 0) ih.AddString("[" + string.Join(", ", res.ToArray()) + "] ");
                    ih.AddString("interface ");
                    ih.AddLink(ti2.GetName(), "i");
                    ih.AppendLine(";");
                }
            }
            ih.AppendLine("};");
        }
    }
}