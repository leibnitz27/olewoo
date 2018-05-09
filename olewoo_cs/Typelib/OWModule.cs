using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWModule : ITlibNode
    {
        private readonly string _name;
        private readonly ITypeInfo _ti;
        private readonly TypeAttr _ta;
        private readonly string _dllname;
        private readonly IDLData _data;

        public OWModule(ITlibNode parent, ITypeInfo ti, TypeAttr ta)
        {
            Parent = parent;
            _ti = ti;
            _ta = ta;
            _name = _ti.GetName();
            if (_ta.cVars <= 0 && _ta.cFuncs <= 0) return;
            var tix = new olewoo_interop.ITypeInfoXtra();
            if (_ta.cFuncs > 0)
            {
                var fd = new FuncDesc(_ti, 0);
                var invkind = fd.invkind;
                var memid = fd.memid;
                _dllname = tix.GetDllEntry(ti, invkind, memid);
            }
            else
            {
                _dllname = null;
            }
            _data = new IDLData(this);
        }
        public override string Name => "module " + _name;
        public override string ShortName => _name;
        public override string ObjectName => null;

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return true;
        }
        public override int ImageIndex => (int)ImageIndices.idx_module;
        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            if (_ta.cVars > 0) res.Add(new OWChildrenIndirect(this, "Constants", (int)ImageIndices.idx_constlist, GenConstChildren));
            if (_ta.cFuncs > 0) res.Add(new OWChildrenIndirect(this, "Functions", (int)ImageIndices.idx_methodlist, GenFuncChildren));
            return res;
        }
        private List<ITlibNode> GenConstChildren()
        {
            var res = new List<ITlibNode>();
            for (var x = 0; x < _ta.cVars; ++x)
            {
                var vd = new VarDesc(_ti, x);
                res.Add(new OWModuleConst(this, _ti, vd, x));
            }
            return res;
        }
        private List<ITlibNode> GenFuncChildren()
        {
            var res = new List<ITlibNode>();
            for (var x = 0; x < _ta.cFuncs; ++x)
            {
                var fd = new FuncDesc(_ti, x);
                res.Add(new OWMethod(this, _ti, fd));
            }
            return res;
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            if (_ta.cFuncs == 0)
            {
                ih.AppendLine("// NOTE: This module has no entry points. There is no way to");
                ih.AppendLine("//       extract the dllname of a module with no entry points!");
                ih.AppendLine("// ");
            }
            ih.AppendLine("[");
            var liba = GetAttributes();
            var cnt = 0;
            liba.ForEach(x => ih.AppendLine("  " + x + (++cnt == liba.Count ? "" : ",")));
            ih.AppendLine("]");
            ih.AppendLine("module " + _name + " {");
            using (new IDLHelperTab(ih))
            {
                Children.ForEach(x => x.BuildIDLInto(ih));
            }
            ih.AppendLine("};");
        }

        public override List<string> GetAttributes()
        {
            var liba = new List<string>
            {
                "dllname(\"" + (string.IsNullOrEmpty(_dllname) ? "<no entry points>" : _dllname) + "\")"
            };

            if (_ta.guid != Guid.Empty) liba.Add("uuid(" + _ta.guid + ")");
            var help = _ti.GetHelpDocumentationById(-1, out var cnt);
            if (!string.IsNullOrEmpty(help)) liba.Add("helpstring(\"" + help + "\")");
            if (cnt != 0) liba.Add("helpcontext(" + cnt.PaddedHex() + ")");

            return liba;
        }
    }
}