using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWCoClassInterface : ITlibNode
    {
        private readonly string _name;
        private readonly ITypeInfo _ti;
        private readonly IMPLTYPEFLAGS _flags;

        public OWCoClassInterface(ITlibNode parent, ITypeInfo ti, IMPLTYPEFLAGS impltypeflags)
        {
            Parent = parent;
            Listeners = parent.Listeners;

            _ti = ti;
            _flags = impltypeflags;
            
            _name = _ti.GetName();
            _data= new IDLData(this);
        }

        public override string Name => _name;

        public override string ShortName => _name;

        public override string ObjectName => $"{_name}#i";

        public override List<string> GetAttributes()
        {
            var lprops = new List<string>();
            if (0 != (_flags & IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT)) lprops.Add("default");
            if (0 != (_flags & IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE)) lprops.Add("source");
            if (0 != (_flags & IMPLTYPEFLAGS.IMPLTYPEFLAG_FRESTRICTED)) lprops.Add("restricted");

            return lprops;
        }

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames) => false;

        public override int ImageIndex => (int)ImageIndices.idx_method;

        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren() => new List<ITlibNode>();

        public override void BuildIDLInto(IDLFormatter ih)
        {
            EnterElement();
            var lprops = _data.Attributes;
            
            if (lprops.Count > 0) ih.AddString("[" + string.Join(", ", lprops.ToArray()) + "] ");
            var attr = _ti.GetTypeAttr();
            if (TypeAttr.TypeFlags.TYPEFLAG_FDUAL != (attr.wTypeFlags & TypeAttr.TypeFlags.TYPEFLAG_FDUAL) &&
                TypeAttr.TypeKind.TKIND_DISPATCH == (attr.typekind & TypeAttr.TypeKind.TKIND_DISPATCH))
            {
                ih.AddString("dispinterface ");
                ih.AddLink(_data.ShortName, "di");
            }
            else
            {
                ih.AddString("interface ");
                ih.AddLink(_data.ShortName, "i");
            }
            ih.AppendLine(";");
            
            ExitElement();
        }
        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterCoClassInterface(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitCoClassInterface(this);
            }
        }
    }
}