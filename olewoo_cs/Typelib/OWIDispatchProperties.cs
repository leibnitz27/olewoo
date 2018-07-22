using System.Collections.Generic;
using System.Linq;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWIDispatchProperties : ITlibNode
    {
        private readonly OWDispInterface _parent;

        public OWIDispatchProperties(OWDispInterface parent)
        {
            _parent = parent;
        }
        public override string Name => "Properties";

        public override string ShortName => null;

        public override string ObjectName => null;

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames)
        {
            return false;
        }
        public override int ImageIndex => (int)ImageIndices.idx_propertylist;
        public override ITlibNode Parent => _parent;

        public override List<ITlibNode> GenChildren()
        {
            return _parent.PropertyChildren();
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            EnterElement();
            var props = Children.ToList().ConvertAll(x => x as OWDispProperty);
            ih.AppendLine("properties:");
            if (props.Count > 0) using (new IDLHelperTab(ih)) props.ForEach(x => x.BuildIDLInto(ih, true));
            ExitElement();
        }

        public override List<string> GetAttributes()
        {
            return new List<string>();
        }

        public override void EnterElement()
        {
            foreach (var listener in Listeners)
            {
                listener.EnterIDispatchProperties(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitIDispatchProperties(this);
            }
        }
    }
}