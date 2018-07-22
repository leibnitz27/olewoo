using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using olewoo_interop;

namespace Org.Benf.OleWoo.Typelib
{
    internal static class ITypeInfoXtra
    {

        public static string PaddedHex(this int x)
        {
            return "0x" + x.ToString("x").PadLeft(8, '0');
        }

        private const int MEMBERID_NONE = -1;
        public static string GetName(this ITypeLib ti)
        {
            ti.GetDocumentation(MEMBERID_NONE, out var res, out _, out var _, out _);
            return res;
        }
        public static string GetName(this ITypeInfo ti)
        {
            return ti.GetDocumentationById(MEMBERID_NONE);
        }
        public static string GetDocumentationById(this ITypeInfo ti, int memid)
        {
            ti.GetDocumentation(memid, out var res, out _, out var _, out _);
            return res;
        }
        public static string GetHelpDocumentationById(this ITypeInfo ti, int memid, out int context)
        {
            ti.GetDocumentation(memid, out _, out var res, out context, out _);
            return res;
        }
        public static string GetHelpDocumentation(this ITypeLib ti, out int context)
        {
            ti.GetDocumentation(MEMBERID_NONE, out _, out var res, out context, out _);
            return res;
        }
        public static TypeAttr GetTypeAttr(this ITypeInfo ti)
        {         
            return new TypeAttr(ti);
        }
        public static bool SwapForInterface(ref ITypeInfo ti, ref TypeAttr ta)
        {
            try
            {
                if (ta.typekind == TypeAttr.TypeKind.TKIND_DISPATCH && 0 != (ta.wTypeFlags & TypeAttr.TypeFlags.TYPEFLAG_FDUAL))
                {
                    ti.GetRefTypeOfImplType(-1, out var href);
                    ti.GetRefTypeInfo(href, out ti);
                    ta = new TypeAttr(ti);
                    return true;
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return false;
        }

        public static string QuoteString(object o)
        {
            if (o == null) return "";
            if (o is bool b)
            {
                return b ? "-1" : "0";
            }
            if (o is string)
            {
                return $"\"{o}\"";
            }
            return o.ToString();
        }

        public static string ReEscape(this string s)
        {
            if (s.IndexOfAny(new[]{'\0', '\n', '\r', '\b', '\a', '\f', '\t', '\v'}) >= 0)
            {
                s = s.Replace("\0", "\\0");
                s = s.Replace("\n", "\\n");
                s = s.Replace("\r", "\\r");
                s = s.Replace("\b", "\\b");
                s = s.Replace("\a", "\\a");
                s = s.Replace("\f", "\\f");
                s = s.Replace("\t", "\\t");
                s = s.Replace("\v", "\\v");
            }
            return "\"" + s + "\"";
        }

        public static string VarTypeToString(long vt)
        {
            switch ((VarEnum)vt)
            {
                case VarEnum.VT_EMPTY: // 0,
                case VarEnum.VT_NULL: // 1,
                case VarEnum.VT_I2: // 2,
                case VarEnum.VT_I4: // 3,
                case VarEnum.VT_R4: // 4,
                case VarEnum.VT_R8: // 5,
                case VarEnum.VT_CY: // 6,
                case VarEnum.VT_DATE: // 7,
                case VarEnum.VT_BSTR: // 8,
                case VarEnum.VT_DISPATCH: // 9,
                case VarEnum.VT_ERROR: // 10,
                case VarEnum.VT_BOOL: // 11,
                case VarEnum.VT_VARIANT: // 12,
                case VarEnum.VT_UNKNOWN: // 13,
                case VarEnum.VT_DECIMAL: // 14,
                case VarEnum.VT_I1: // 16,
                case VarEnum.VT_UI1: // 17,
                case VarEnum.VT_UI2: // 18,
                    break;
                case VarEnum.VT_UI4: // 19,
                    return "unsigned long";
                case VarEnum.VT_I8: // 20,
                    return "int64";
                case VarEnum.VT_UI8: // 21,
                    return "uint64";
                case VarEnum.VT_INT: // 22,
                case VarEnum.VT_UINT: // 23,
                case VarEnum.VT_VOID: // 24,
                case VarEnum.VT_HRESULT: // 25,
                case VarEnum.VT_PTR: // 26,
                case VarEnum.VT_SAFEARRAY: // 27,
                case VarEnum.VT_CARRAY: // 28,
                    break;
                case VarEnum.VT_USERDEFINED: // 29,
                    return "USER DEFINED";
                case VarEnum.VT_LPSTR: // 30,
                case VarEnum.VT_LPWSTR: // 31,
                    return "LPWSTR";
                case VarEnum.VT_RECORD: // 36,
                //            case VarEnum.VT_INT_PTR	: // 37,
                //            case VarEnum.VT_UINT_PTR	: // 38,
                case VarEnum.VT_FILETIME: // 64,
                case VarEnum.VT_BLOB: // 65,
                case VarEnum.VT_STREAM: // 66,
                case VarEnum.VT_STORAGE: // 67,
                case VarEnum.VT_STREAMED_OBJECT: // 68,
                case VarEnum.VT_STORED_OBJECT: // 69,
                case VarEnum.VT_BLOB_OBJECT: // 70,
                case VarEnum.VT_CF: // 71,
                case VarEnum.VT_CLSID: // 72,
                //            case VarEnum.VT_VERSIONED_STREAM	: // 73,
                //            case VarEnum.VT_BSTR_BLOB	: // 0xfff,
                case VarEnum.VT_VECTOR: // 0x1000,
                    break;
            }
            return vt.ToString() + "???";
        }

        public static string[] GetNames(this FuncDesc fd, ITypeInfo ti)
        {
            var names = new string[fd.cParams + 1];
            ti.GetNames(fd.memid, names, fd.cParams + 1, out _);
            return names;
        }
    }
}