using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWMethod : ITlibNode
    {
        private readonly string _name;
        private readonly FuncDesc _fd;
        private readonly ITypeInfo _ti;
        private readonly IDLData _data;

        public OWMethod(ITlibNode parent, ITypeInfo ti, FuncDesc fd)
        {
            Parent = parent;
            _ti = ti;
            _fd = fd;

            var names = fd.GetNames(ti);
            _name = names[0];
            _data= new IDLData(this);
        }

        public override string Name => _name;

        public override string ShortName => _name;

        public override string ObjectName => $"{_name}#m";

        public override List<string> GetAttributes()
        {
            var lprops = new List<string>();
            if (!MemIdInSpecialRange)
            {
                lprops.Add("id(" + _fd.memid.PaddedHex() + ")");
            }
            switch (_fd.invkind)
            {
                case FuncDesc.InvokeKind.INVOKE_PROPERTYGET:
                    lprops.Add("propget");
                    break;
                case FuncDesc.InvokeKind.INVOKE_PROPERTYPUT:
                    lprops.Add("propput");
                    break;
                case FuncDesc.InvokeKind.INVOKE_PROPERTYPUTREF:
                    lprops.Add("propputref");
                    break;
            }
            OWCustData.GetAllFuncCustData(_fd.memid - 1, _ti, ref lprops);
            var help = _ti.GetHelpDocumentationById(_fd.memid, out var context);
            if (0 != (_fd.wFuncFlags & FuncDesc.FuncFlags.FUNCFLAG_FRESTRICTED)) lprops.Add("restricted");
            if (0 != (_fd.wFuncFlags & FuncDesc.FuncFlags.FUNCFLAG_FHIDDEN)) lprops.Add("hidden");
            AddHelpStringAndContext(lprops, help, context);

            return lprops;
        }

        private bool MemIdInSpecialRange => (_fd.memid >= 0x60000000 && _fd.memid < 0x60020000);

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames) => false;

        public override int ImageIndex => (int)ImageIndices.idx_method;

        public override ITlibNode Parent { get; }

        public override List<ITlibNode> GenChildren() => new List<ITlibNode>();

        private string ParamFlagsDescription(ParamDesc pd)
        {
            var flg = pd.wParamFlags;
            var res = new List<string>();
            if (0 != (flg & ParamDesc.ParamFlags.PARAMFLG_FIN)) res.Add("in");
            if (0 != (flg & ParamDesc.ParamFlags.PARAMFLG_FOUT)) res.Add("out");
            if (0 != (flg & ParamDesc.ParamFlags.PARAMFLG_FRETVAL)) res.Add("retval");
            if (0 != (flg & ParamDesc.ParamFlags.PARAMFLG_FOPT)) res.Add("optional");
            if (0 != (flg & ParamDesc.ParamFlags.PARAMFLG_FHASDEFAULT)) res.Add("defaultvalue(" + ITypeInfoXtra.QuoteString(pd.varDefaultValue) + ")");
            return "[" + string.Join(", ", res.ToArray()) + "]";
        }
        public bool Property => _fd.invkind != FuncDesc.InvokeKind.INVOKE_FUNC;

        public override void BuildIDLInto(IDLFormatter ih)
        {
            BuildIDLInto(ih, false);
        }

        public void BuildIDLInto(IDLFormatter ih, bool bAsDispatch)
        {
            var lprops = GetAttributes();
            ih.AppendLine("[" + string.Join(", ", lprops.ToArray()) + "] ");
            // Prototype in a different line.
            var ed = _fd.elemdescFunc;

            Action<int> paramtextgen = null;
            ElemDesc elast = null;
            var bRetvalPresent = false;
            if (_fd.cParams > 0)
            {
                var names = _fd.GetNames(_ti);
                var edps = _fd.elemdescParams;
                if (edps.Length > 0) elast = edps[edps.Length - 1];
                if (bAsDispatch && elast != null && 0 != (elast.paramdesc.wParamFlags & ParamDesc.ParamFlags.PARAMFLG_FRETVAL))
                    bRetvalPresent = true;
                // ReSharper disable once UnusedVariable
                var maxCnt = (bAsDispatch && bRetvalPresent) ? _fd.cParams - 1 : _fd.cParams;

                paramtextgen = x =>
                {
                    var paramname = (names[x + 1] == null) ? "rhs" : names[x + 1];
                    var edp = edps[x];
                    ih.AddString(ParamFlagsDescription(edp.paramdesc) + " ");
                    edp.tdesc.ComTypeNameAsString(_ti, ih);
                    ih.AddString(" " + paramname);
                };
            }
            (bRetvalPresent ? elast : ed).tdesc.ComTypeNameAsString(_ti, ih);
            if (MemIdInSpecialRange)
            {
                ih.AddString(" " + _fd.callconv.ToString().Substring(2).ToLower());
            }
            ih.AddString($" {_name}");
            switch (_fd.cParams)
            {
                case 0:
                    ih.AppendLine("();");
                    break;
                case 1:
                    ih.AddString("(");
                    paramtextgen?.Invoke(0);
                    ih.AppendLine(");");
                    break;
                default:
                    ih.AppendLine("(");
                    using (new IDLHelperTab(ih))
                    {
                        for (var y = 0; y < _fd.cParams; ++y)
                        {
                            paramtextgen?.Invoke(y);
                            ih.AppendLine(y == _fd.cParams - 1 ? "" : ",");
                        }
                    }
                    ih.AppendLine(");");
                    break;
            }
        }
    }
}