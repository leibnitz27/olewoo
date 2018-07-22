using System.Collections.Generic;

namespace Org.Benf.OleWoo.Typelib
{
    public class IDLData
    {
        public readonly ITlibNode Node;
        public List<string> Attributes;
        public string Name;
        public string ShortName;

        public IDLData(ITlibNode node)
        {
            Node = node;
            Attributes = node.GetAttributes();
            Name = node.Name;
            ShortName = node.ShortName;
        }
    }
}
