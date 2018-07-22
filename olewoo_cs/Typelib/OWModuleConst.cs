using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWModuleConst : ITlibNode
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly int _idx;
        private readonly string _name;
        private readonly object _val;

        public OWModuleConst(ITlibNode parent, ITypeInfo ti, VarDesc vd, int idx)
        {
            _idx = idx;
            Parent = parent;
            var ig = new IDLGrabber();
            vd.elemDescVar.tdesc.ComTypeNameAsString(ti, ig);
            _name = ig.Value + " " + ti.GetDocumentationById(vd.memid);
            _val = vd.varValue ?? "";
            if (_val is string)
            {
                _val = (_val as string).ReEscape();
            }
            _data = new IDLData(this);
        }
        public override string Name => "const " + _name;
        public override string ShortName => _name;
        public override string ObjectName => null;

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return false;
        }
        public override int ImageIndex => (int)ImageIndices.idx_const;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            return res;
        }

        private string negStr(int x)
        {
            return (x < 0) ? ("0x" + x.ToString("X")) : x.ToString();
        }
        public void BuildIDLInto(IDLFormatter ih, bool embedded, bool islast)
        {
            EnterElement();
            var desc = "";
            //int cnt = 0;
            //String help = _ti.GetHelpDocumentationById(_idx, out cnt);
            //List<String> props = new List<string>();
            //AddHelpStringAndContext(props, help, cnt);
            //if (props.Count > 0)
            //{
            //    desc += "[" + String.Join(",", props.ToArray()) + "] ";
            //}
            desc += _val is int i ? negStr(i) : _val.ToString();
            ih.AppendLine(_data.Name + " = " + desc + (embedded ? (islast ? "" : ",") : ";"));
            ExitElement();
        }

        public override void BuildIDLInto(IDLFormatter ih)
        {
            EnterElement();
            BuildIDLInto(ih, false, false);
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
                listener.EnterModuleConst(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitModuleConst(this);
            }
        }
    }
}