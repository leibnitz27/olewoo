using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Win32;
using olewoo_interop;

// ReSharper disable InconsistentNaming
// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace Org.Benf.OleWoo.Typelib
{
    public class OWTypeLib : ITlibNode, IClearUp
    {
        private ITypeLib _tlib;
        private readonly string _name;

        public OWTypeLib(string path)
        {
            NativeMethods.LoadTypeLib(path, out _tlib);
            if (_tlib == null) throw new Exception(path + " is not a loadable typelibrary.");
            _name = _tlib.GetName();
            _name += " (" + _tlib.GetHelpDocumentation(out _) + ")";

            _data = new IDLData(this);
        }

        public OWTypeLib(ITypeLib tlib)
        {
            _tlib = tlib;
            _name = _tlib.GetName();
            _name += " (" + _tlib.GetHelpDocumentation(out _) + ")";

            _data = new IDLData(this);
        }

        public override List<string> GetAttributes()
        {
            var liba = new List<string>();
            using (var tla = new TypeLibAttr(_tlib))
            {
                liba.Add($"uuid({tla.guid})");
                liba.Add($"version({tla.wMajorVerNum}.{tla.wMinorVerNum})");
            }
            var cds = new CustomDatas(_tlib as ITypeLib2);
            {
                foreach (var cd in cds.Items)
                {
                    liba.Add("custom(" + cd.guid + ", " + ITypeInfoXtra.QuoteString(cd.varValue) + ")");
                }
            }
            var help = _tlib.GetHelpDocumentation(out var cnt);
            if (!string.IsNullOrEmpty(help)) liba.Add($"helpstring(\"{help}\")");
            if (cnt != 0) liba.Add($"helpcontext({cnt.PaddedHex()})");

            return liba;
        }

        public void ClearUp()
        {
            Marshal.ReleaseComObject(_tlib);
            _tlib = null; 
        }

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            throw new Exception("Should not be calling this!"); 
        }
        public override int ImageIndex => (int)ImageIndices.idx_typelib;

        public override string ShortName => _tlib.GetName();

        public override string ObjectName => ShortName;

        public override List<ITlibNode> GenChildren()
        {
            var res = new List<ITlibNode>();
            var ticount = _tlib.GetTypeInfoCount();
            for (var x = 0; x < ticount; ++x)
            {
                _tlib.GetTypeInfo(x, out var ti);
                CommonBuildTlibNode(this, ti, true, true, res);
            }
            return res;
        }

        public override string Name => _name;

        public override ITlibNode Parent => null;

        public override void BuildIDLInto(IDLFormatter ih)
        {
            // Header for type library, followed by a first pass to pre-declare
            // interfaces.
            // dispinterfaces aren't shown seperately.

            EnterElement();

            ih.AppendLine("// Generated .IDL file (by OleWoo)");
            ih.AppendLine("[");

            var liba = _data.Attributes;
            var cnt = 0;
            liba.ForEach(x => ih.AppendLine("  " + x + (++cnt == liba.Count ? "" : ",")));
            ih.AppendLine("]");
            ih.AppendLine("library " + _data.ShortName);
            ih.AppendLine("{");
            using (new IDLHelperTab(ih))
            {
                var lmd = new TypeLibMetadata();
                var l = lmd.GetDependentLibraries(_tlib);
                foreach (var dl in l)
                {
                    var attr = new TypeLibAttr(dl);
                    var hive = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default);
                    var libKey = hive.OpenSubKey($"TypeLib\\{{{attr.guid}}}\\{attr.wMajorVerNum}.{attr.wMinorVerNum}");
                    var longName = libKey?.GetValue(null);
                    if (longName != null)
                    {
                        longName = string.Concat(", ", longName);
                    }
                    var tlbKey = libKey?.OpenSubKey($"{attr.lcid}\\win32") ?? libKey?.OpenSubKey($"{attr.lcid}\\win64");
                    var path = tlbKey?.GetValue(null) as string;
                    ih.AppendLine($"// Tlib : {dl.GetName()}{longName} : {{{attr.guid}}}");
                    ih.AppendLine($"importlib(\"{path ?? dl.GetName()}\");");
                }
                ih.AppendLine(string.Empty);

                //Identify all interfaces & dispinterfaces to be listed
                var interfaceNames = Children.Aggregate<ITlibNode, ICollection<string>>(new HashSet<string>(),
                    (x, y) =>
                    {
                        if ((y as OWInterface) != null) x.Add(y.ShortName);
                        return x;
                    });

                // Forward declare all interfaces.
                ih.AppendLine("// Forward declare all types defined in this typelib");
                
                Children.FindAll(x => (x as OWCoClass) != null).ForEach(
                    x => ih.AppendLine(string.Concat(x.Name, ";"))
                );

                /* 
                 * Need to collect all dumpable interface names, in case we have dispinterfaces which don't have
                 * top level interfaces.  In THIS case, we'd dump the dispinterface.
                 */
                var fwdDeclarations = new Dictionary<string, string>();
                Children.FindAll(x =>
                    (x as OWInterface) != null).ForEach(
                    x => fwdDeclarations.Add(x.ShortName, string.Concat(x.Name, ";"))
                );
                Children.FindAll(x =>
                    (x as OWDispInterface) != null).ForEach(
                    x =>
                    {
                        if (!fwdDeclarations.ContainsKey(x.ShortName))
                        {
                            fwdDeclarations.Add(x.ShortName, string.Concat(x.Name, ";"));
                        }
                    }
                );
                foreach (var fwdDeclaration in fwdDeclarations)
                {
                    ih.AppendLine(fwdDeclaration.Value);
                }
                ih.AppendLine(string.Empty);
                Children.FindAll(x => (x as OWEnum) != null).ForEach(
                    x =>
                    {
                        x.BuildIDLInto(ih);
                        ih.AppendLine(string.Empty);
                    });
                ih.AppendLine(string.Empty);
                Children.FindAll(x => (x as OWRecord) != null).ForEach(x =>
                {
                    x.BuildIDLInto(ih);
                    ih.AppendLine(string.Empty);
                });

                Children.FindAll(x => x.DisplayAtTLBLevel(interfaceNames)).ForEach(
                    x =>
                    {
                        x.BuildIDLInto(ih);
                        ih.AppendLine("");
                    }
                );
            }
            ih.AppendLine("};");

            ExitElement();
        }

        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterTypeLib(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitTypeLib(this);
            }
        }
    }

    /*
     * A dispinterface's first inherited interface is the swap for interface.
     */
}



