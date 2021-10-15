using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Org.Benf.OleWoo
{
    public static class NativeMethods
    {
        #region Library loading for finding type libraries

        [System.Flags]
        private enum LoadLibraryFlags : uint
        {
            None = 0,
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        private delegate bool EnumResNameDelegate(
        IntPtr hModule,
        IntPtr lpszType,
        IntPtr lpszName,
        IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr hModule);

        private static bool IsIntResource(IntPtr value)
        {
            if (((uint)value) > ushort.MaxValue)
                return false;
            return true;
        }

        private static string GetResourceName(IntPtr value)
        {
            if (IsIntResource(value) == true)
                return value.ToString();
            return Marshal.PtrToStringUni((IntPtr)value);
        }

        /// <summary>
        /// Enumerates the resources of a DLL or EXE that are of type TYPELIB and returns an array of their names.
        /// These can be specified in <see cref="LoadTypeLib(string, out ITypeLib)"/> by suffixing \name onto the file path,
        /// e.g. C:\Windows\SysWOW64\msvbvm60.dll\3
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string[] EnumerateTypeLibs(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            if (ext != ".dll" && ext != ".exe")
                return Array.Empty<string>();

            IntPtr handle = NativeMethods.LoadLibraryEx(filePath, IntPtr.Zero, NativeMethods.LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE);
            List<string> names = new List<string>();

            try
            {
                if (NativeMethods.EnumResourceNamesWithName(handle, "TYPELIB",
            (hModule, lpszType, lpszName, lParam) => { names.Add(GetResourceName(lpszName)); return true; }, IntPtr.Zero))
                    return names.ToArray();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (handle != IntPtr.Zero)
                    NativeMethods.FreeLibrary(handle);
            }

            return Array.Empty<string>();

        }

        #endregion

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref int lParam);

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int LoadTypeLib(string fileName, out ITypeLib typeLib);


        [DllImport("kernel32.dll", EntryPoint = "EnumResourceNamesW",
      CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool EnumResourceNamesWithName(
      IntPtr hModule,
      string lpszType,
      EnumResNameDelegate lpEnumFunc,
      IntPtr lParam);


        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern void ClearCustData(IntPtr pCustData);

        public static void SetTabs(this System.Windows.Forms.TextBox box, int nSpaces)
        {
            //EM_SETTABSTOPS - http://msdn.microsoft.com/en-us/library/bb761663%28VS.85%29.aspx
            var lParam = nSpaces * 4;  //Set tab size to 4 spaces
            SendMessage(box.Handle, 0x00CB, new IntPtr(1), ref lParam);
            box.Invalidate();
        }
        public static void SetTabs(this System.Windows.Forms.RichTextBox box, int nSpaces)
        {
            //EM_SETTABSTOPS - http://msdn.microsoft.com/en-us/library/bb761663%28VS.85%29.aspx
            var lParam = nSpaces * 4;  //Set tab size to 4 spaces
            SendMessage(box.Handle, 0x00CB, new IntPtr(1), ref lParam);
            box.Invalidate();
        }
              
    }
}
