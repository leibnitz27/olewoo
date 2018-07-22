using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWTypeDef : ITlibNode
    {
        private readonly ITypeInfo _ti;
        private ITypeInfo _oti;
        private readonly TypeAttr _ta;
        private readonly string _name;

        public OWTypeDef(ITlibNode parent, ITypeInfo ti, TypeAttr ta)
        {
            Parent = parent;
            _ta = ta;
            _ti = ti;

            string prefix = string.Empty;
            if(VarEnum.VT_PTR == ((VarEnum)_ta.tdescAlias.vt & VarEnum.VT_PTR))
            {
                var otd = _ta.tdescAlias.lptdsec;
                _ti.GetRefTypeInfo(otd.hreftype, out _oti);
                prefix = _oti.GetName() + " ";
            }
            else if (VarEnum.VT_ARRAY == ((VarEnum) _ta.tdescAlias.vt & VarEnum.VT_ARRAY))
            {
                var oad = ta.tdescAlias.lpadesc;
                _ti.GetRefTypeInfo(oad.tdescElem.hreftype, out _oti);
                prefix = _oti.GetName() + " ";
            }
            else
            {
                _ti.GetRefTypeInfo(_ta.tdescAlias.hreftype, out _oti);
                prefix = _oti.GetName() + " ";
            }
            _name = prefix + ti.GetName();
            _data = new IDLData(this);
        }
        public override string Name => "typedef " + _name;
        public override string ShortName => _name;
        public override string ObjectName => _name + "#i";

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return true;
        }
        public override int ImageIndex => (int)ImageIndices.idx_typedef;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            if (_oti != null)
            {
                CommonBuildTlibNode(this, _oti, false, false, res);
            }
            return res;
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            EnterElement();
            var lprops = _data.Attributes;
            ih.AppendLine("typedef [" + string.Join(", ", lprops.ToArray()) + "] " + _data.ShortName + ";");
            ExitElement();
        }

        public override List<string> GetAttributes()
        {
            return new List<string>
            {
                "public"
            };
        }

        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterTypeDef(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitTypeDef(this);
            }
        }
    }
}