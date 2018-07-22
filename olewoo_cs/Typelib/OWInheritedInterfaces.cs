using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWInheritedInterfaces : ITlibNode
    {
        protected TypeAttr _ta;
        protected ITypeInfo _ti;

        public OWInheritedInterfaces(ITlibNode parent, ITypeInfo ti, TypeAttr ta)
        {
            Parent = parent;
            _ta = ta;
            _ti = ti;
        }
        public override string Name => "Inherited Interfaces";

        public override string ShortName => null;

        public override string ObjectName => null;

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames) => false;

        public override int ImageIndex => (int)ImageIndices.idx_interface; 

        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();

            if (_ta.cImplTypes > 0)
            {
                if (_ta.cImplTypes > 1) throw new Exception("Multiple inheritance!?");
                _ti.GetRefTypeOfImplType(0, out var href);
                _ti.GetRefTypeInfo(href, out var ti2);
                CommonBuildTlibNode(this, ti2, false, true, res);
            }
            return res;
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            EnterElement();
            ih.AppendLine("");
            ExitElement();
        }

        public override List<string> GetAttributes()
        {
            return new List<string>();
        }
        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterInheritedInterfaces(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitInheritedInterfaces(this);
            }
        }
    }
}