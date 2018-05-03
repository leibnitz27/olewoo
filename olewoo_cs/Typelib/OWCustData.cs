using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

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
                lprops.Add($"custom({item.guid}, \"{item.varValue}\")");
            }
            NativeMethods.ClearCustData(ptr);
            Marshal.FreeHGlobal(ptr);
        }
    }
}
