/*
 * Cheap n cheerful non-com(shell extension) file registration for context menu.
 * 
 * From
 * 
 * http://www.codeproject.com/KB/shell/SimpleContextMenu.aspx
 * 
 */

using System;
using Microsoft.Win32;

// ReSharper disable LocalizableElement

namespace Org.Benf.OleWoo
{
    internal static class FileShellExtension
    {
        public static void Register(string fileType,
               string shellKeyName, string menuText, string menuCommand)
        {
            try
            {
                // create path to registry location

                var regPath = $@"{fileType}\shell\{shellKeyName}";

                // add context menu to the registry

                using (var key = Registry.ClassesRoot.CreateSubKey(regPath))
                {
                    key.SetValue(null, menuText);
                }

                // add command that is invoked to the registry

                using (var key = Registry.ClassesRoot.CreateSubKey($@"{regPath}\command"))
                {
                    key.SetValue(null, menuCommand);
                }
            }
            catch (UnauthorizedAccessException)
            {
                System.Windows.Forms.MessageBox.Show("Please run as administrator to perform this task.", "Insufficient Privileges.", System.Windows.Forms.MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error:", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }

        public static void Unregister(string fileType, string shellKeyName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileType) ||
                    string.IsNullOrEmpty(shellKeyName)) return;

                // path to the registry location
                var regPath = $@"{fileType}\shell\{shellKeyName}";

                // remove context menu from the registry
                Registry.ClassesRoot.DeleteSubKeyTree(regPath);
            }
            catch (UnauthorizedAccessException)
            {
                System.Windows.Forms.MessageBox.Show("Please run as administrator to perform this task.", "Insufficient Privileges.", System.Windows.Forms.MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error:", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }
    }
}
