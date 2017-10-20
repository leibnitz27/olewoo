using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace Org.Benf.OleWoo
{
    // ReSharper disable once InconsistentNaming
    internal class MRUList
    {
        private const int Maxdataitems = 9;

        private readonly RegistryKey _mrufiles;
        private LinkedList<string> _data;
        private Dictionary<string, bool> _uqitems;

        public MRUList(string path)
        {
            var hkcu = Registry.CurrentUser;
            _mrufiles = GetRegKey(path, hkcu);
            Refresh();
        }

        public string[] Items => _data.ToArray();

        public void Clear()
        {
            _data = new LinkedList<string>();
            _uqitems = new Dictionary<string, bool>(StringComparer.CurrentCultureIgnoreCase);
        }

        public void AddItem(string path)
        {
            if (!_uqitems.ContainsKey(path))
            {
                while (_data.Count >= Maxdataitems)
                {
                    var last = _data.Last.Value;
                    _uqitems.Remove(last);
                    _data.RemoveLast();
                }
                _data.AddFirst(path);
                _uqitems[path] = true;
            }
        }

        private void Refresh()
        {
            Clear();
            for (var i = 0; i < 10; ++i)
            {
                var o = _mrufiles.GetValue("MruFile" + i);
                var s = o as string;
                if (string.IsNullOrEmpty(s)) return;
                _data.AddLast(s);
                _uqitems[s] = true;
            }
        }

        public void Flush()
        {
            var mval = _data.Count;
            var data = _data.ToArray();
            for (var i = 0; i < 10; ++i)
            {
                var keyname = "MruFile" + i;
                _mrufiles.SetValue(keyname, i >= mval ? "" : data[i]);
            }
        }

        private static RegistryKey GetRegKey(string key, RegistryKey basekey)
        {
            var nkey = basekey.OpenSubKey(key, true) ?? basekey.CreateSubKey(key);
            return nkey;
        }
    }
}
