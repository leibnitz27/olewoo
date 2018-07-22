using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using INVOKEKIND = System.Runtime.InteropServices.ComTypes.INVOKEKIND;

namespace Org.Benf.OleWoo.Typelib
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CUSTDATAITEM
    {
        public Guid guid;
        [MarshalAs(UnmanagedType.Struct)]
        public object varValue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CUSTDATA
    {
        public int cCustData;
        public IntPtr prgCustData;
    }

    public static class OWCustData
    {
        public static void GetCustData(ITypeInfo ti, ref List<string> lprops)
        {
            if (!(ti is ITypeInfo2 t2))
            {
                return;
            }

            var custdata = new CUSTDATA();
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(custdata));
            t2.GetAllCustData(ptr);
            custdata = Marshal.PtrToStructure<CUSTDATA>(ptr);
            for (var x = 0; x < custdata.cCustData; x++)
            {
                var item = new CUSTDATAITEM(); // just to size it for next line
                var itemPtr = custdata.prgCustData + (x * Marshal.SizeOf(item));
                item = Marshal.PtrToStructure<CUSTDATAITEM>(itemPtr);
                lprops.Add($"custom({item.guid}, {ITypeInfoXtra.QuoteString(item.varValue)})");
            }
            NativeMethods.ClearCustData(ptr);
            Marshal.FreeHGlobal(ptr);
        }

        public static void GetAllFuncCustData(int memberid, INVOKEKIND invokekind, ITypeInfo ti, ref List<string> lprops)
        {
            if (!(ti is ITypeInfo2 t2))
            {
                return;
            }

            var custdata = new CUSTDATA();
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(custdata));
            try
            {
                t2.GetFuncIndexOfMemId(memberid, invokekind, out var index);
                t2.GetAllFuncCustData(index, ptr);
                try
                {
                    custdata = Marshal.PtrToStructure<CUSTDATA>(ptr);
                    for (var x = 0; x < custdata.cCustData; x++)
                    {
                        var item = new CUSTDATAITEM(); // just to size it for next line
                        var itemPtr = custdata.prgCustData + (x * Marshal.SizeOf(item));
                        item = Marshal.PtrToStructure<CUSTDATAITEM>(itemPtr);
                        lprops.Add($"custom({item.guid}, {ITypeInfoXtra.QuoteString(item.varValue)})");
                    }
                }
                finally
                {
                    NativeMethods.ClearCustData(ptr);
                }
            }
            catch (COMException e)
            {
                const int TYPE_E_ELEMENTNOTFOUND = unchecked((int)0x8002802B);
                if (e.HResult != TYPE_E_ELEMENTNOTFOUND)
                {
                    throw;
                }

                // not found; ignore
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
