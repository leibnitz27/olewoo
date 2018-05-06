using System.Collections.Generic;

namespace Org.Benf.OleWoo.Typelib
{
    public struct IDLData
    {
        public readonly ITlibNode Node;
        public IList<string> Attributes;
        public string Name;
        public string TypeName;

        public IDLData(ITlibNode node)
        {
            Node = node;
            Attributes = node.GetAttributes();
            Name = node.Name;
            TypeName = node.TypeName();
        }
    }
}
