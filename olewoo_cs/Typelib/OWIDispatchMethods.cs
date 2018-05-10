using System.Collections.Generic;
using System.Linq;

namespace Org.Benf.OleWoo.Typelib
{
    internal class OWIDispatchMethods : ITlibNode
    {
        private readonly OWDispInterface _parent;

        public OWIDispatchMethods(OWDispInterface parent)
        {
            _parent = parent;
        }
        public override string Name => "Methods";

        public override string ShortName => null;

        public override string ObjectName => null;

        public override bool DisplayAtTLBLevel(ICollection<string> interfaceNames) => false;

        public override int ImageIndex => (int)ImageIndices.idx_methodlist; 

        public override ITlibNode Parent => _parent; 

        public override List<ITlibNode> GenChildren()
        {
            return _parent.MethodChildren();
        }
        public override void BuildIDLInto(IDLFormatter ih)
        {
            EnterElement();
            var meths = Children.Select(x => x as OWMethod).ToList();
            ih.AppendLine("methods:");
            if (meths.Count > 0)
            {
                using (new IDLHelperTab(ih)) meths.ForEach(x => x.BuildIDLInto(ih, true));
            }
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
                listener.EnterIDispatchMethod(this);
            }
        }

        public override void ExitElement()
        {
            foreach (var listener in Listeners)
            {
                listener.ExitIDispatchMethod(this);
            }
        }
    }
}