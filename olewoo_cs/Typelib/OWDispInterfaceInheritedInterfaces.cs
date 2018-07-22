using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWDispInterfaceInheritedInterfaces : OWInheritedInterfaces
    {
        public OWDispInterfaceInheritedInterfaces(ITlibNode parent, ITypeInfo ti, TypeAttr ta) 
            : base(parent, ti, ta)
        {
        }
        public override List<ITlibNode> GenChildren()
        {
            var ti = _ti;
            var ta = _ta;
            ITypeInfoXtra.SwapForInterface(ref ti, ref ta);
            var res = new List<ITlibNode> {new OWInterface(this, ti, ta, false)};
            return res;
        }
        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterDispInterfaceInheritedInterface(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitDispInterfaceInheritedInterface(this);
            }
        }
    }
}