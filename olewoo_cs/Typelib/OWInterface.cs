using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWInterface : ITlibNode
    {
        private readonly string _name;
        private readonly TypeAttr _ta;
        private readonly ITypeInfo _ti;
        private readonly bool _topLevel;

        public OWInterface(ITlibNode parent, ITypeInfo ti, TypeAttr ta, bool topLevel)
        {
            Parent = parent;
            _name = ti.GetName();
            _ta = ta;
            _ti = ti;
            _topLevel = topLevel;
            _data = new IDLData(this);
        }

        public override int ImageIndex => (int)ImageIndices.idx_interface; 

        public override string Name => (_topLevel ? "interface " : "") + _name;

        public override string ObjectName => $"{_name}#i";

        public override string ShortName => _name;

        public override List<string> GetAttributes()
        {
            var lprops = new List<string> { $"uuid({_ta.guid})" };
            var ta = new TypeAttr(_ti);
            if (ta.wMajorVerNum != 0 || ta.wMinorVerNum != 0)
            {
                lprops.Add($"version({ta.wMajorVerNum}.{ta.wMinorVerNum})");
            }
            OWCustData.GetCustData(_ti, ref lprops);
            var help = _ti.GetHelpDocumentationById(-1, out var context);
            AddHelpStringAndContext(lprops, help, context);
            if (0 != (_ta.wTypeFlags & TypeAttr.TypeFlags.TYPEFLAG_FHIDDEN)) lprops.Add("hidden");
            if (0 != (_ta.wTypeFlags & TypeAttr.TypeFlags.TYPEFLAG_FDUAL)) lprops.Add("dual");
            if (0 != (_ta.wTypeFlags & TypeAttr.TypeFlags.TYPEFLAG_FRESTRICTED)) lprops.Add("restricted");
            if (0 != (_ta.wTypeFlags & TypeAttr.TypeFlags.TYPEFLAG_FNONEXTENSIBLE)) lprops.Add("nonextensible");
            if (0 != (_ta.wTypeFlags & TypeAttr.TypeFlags.TYPEFLAG_FOLEAUTOMATION)) lprops.Add("oleautomation");

            return lprops;
        }

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames) => true;

        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            // First add a child for every method / property, then an inherited interfaces
            // child (if applicable).

            var nfuncs = _ta.cFuncs;
            for (var idx = 0; idx < nfuncs; ++idx)
            {
                var fd = new FuncDesc(_ti, idx);
                res.Add(new OWMethod(this, _ti, fd));
            }
            if (_ta.cImplTypes > 0)
            {
                res.Add(new OWInheritedInterfaces(this, _ti, _ta));
            }
            return res;
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            EnterElement();
            ih.AppendLine("[");
            var lprops = _data.Attributes;
            for (var i = 0; i < lprops.Count; ++i)
            {
                ih.AppendLine("  " + lprops[i] + (i < (lprops.Count - 1) ? "," : ""));
            }
            ih.AppendLine("]");

            if (_ta.cImplTypes > 0)
            {
                _ti.GetRefTypeOfImplType(0, out var href);
                _ti.GetRefTypeInfo(href, out var ti2);
                ih.AddString($"{_data.Name} : ");
                ih.AddLink(ti2.GetName(), "i");
                ih.AppendLine(" {");
            }
            else
            {
                ih.AppendLine("interface " + _data.Name + " {");
            }
            using (new IDLHelperTab(ih))
            {
                Children.ForEach( x => x.BuildIDLInto(ih) );
            }
            ih.AppendLine("};");
            ExitElement();
        }
        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterInterface(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitInterface(this);
            }
        }
    }
}