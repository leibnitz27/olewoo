using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWEnum : ITlibNode
    {
        private readonly string _name;
        private readonly TypeAttr _ta;
        private readonly ITypeInfo _ti;
        private readonly IDLData _data;

        public OWEnum(ITlibNode parent, ITypeInfo ti, TypeAttr ta)
        {
            Parent = parent;
            _name = ti.GetName();
            _ta = ta;
            _ti = ti;
            _data = new IDLData(this);
        }

        public override string Name => "typedef enum " + _name;
        public override string ShortName => _name;
        public override string ObjectName => _name + "#i";

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return false;
        }
        public override int ImageIndex => (int)ImageIndices.idx_enum;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            for (var x = 0; x < _ta.cVars; ++x)
            {
                var vd = new VarDesc(_ti, x);
                res.Add(new OWEnumValue(this, _ti, vd));
            }
            return res;
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            var tde = "typedef ";
            // If the enum has a uuid, or a version associate with it, we provide that information on the same line.

            if (!_ta.guid.Equals(Guid.Empty))
            {
                var lprops = GetAttributes();
                tde += "[" + string.Join(",", lprops) + "]";
                ih.AppendLine(tde);
                tde = "";
            }
            ih.AppendLine(tde + "enum {");
            using (new IDLHelperTab(ih))
            {
                var idx = 0;
                Children.ForEach(x => ((OWEnumValue) x).BuildIDLInto(ih, true, ++idx == _ta.cVars));
            }
            ih.AppendLine("} " + _name + ";");
        }

        public override List<string> GetAttributes()
        {
            var lprops = new List<string>();
            lprops.Add("uuid(" + _ta.guid + ")");
            lprops.Add("version(" + _ta.wMajorVerNum + "." + _ta.wMinorVerNum + ")");
            return lprops;
        }
        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterEnum(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitEnum(this);
            }
        }
    }
}