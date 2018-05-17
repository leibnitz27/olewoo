using Org.Benf.OleWoo.Typelib;

namespace Org.Benf.OleWoo.Listeners
{
    public interface ITypeLibListener
    {
        void EnterChildrenIndirect(ITlibNode indirectChildrenNode);
        void ExitChildrenIndirect(ITlibNode indirectChildrenNode);
        void EnterCoClass(ITlibNode coClassNode);
        void ExitCoClass(ITlibNode coClassNode);
        void EnterCoClassInterface(ITlibNode coClassInterfaceNode);
        void ExitCoClassInterface(ITlibNode coClassInterfaceNode);
        void EnterDispInterface(ITlibNode dispInterfaceNode);
        void ExitDispInterface(ITlibNode dispInterfaceNode);
        void EnterDispInterfaceInheritedInterface(ITlibNode dispInterfaceInheritedInterface);
        void ExitDispInterfaceInheritedInterface(ITlibNode dispInterfaceInheritedInterface);
        void EnterDispProperty(ITlibNode dispPropertyNode);
        void ExitDispProperty(ITlibNode dispPropertyNode);
        void EnterEnum(ITlibNode enumNode);
        void ExitEnum(ITlibNode enumNode);
        void EnterEnumValue(ITlibNode enumValueNode);
        void ExitEnumValue(ITlibNode enumValueNode);
        void EnterIDispatchMethod(ITlibNode iDispatchMethodNode);
        void ExitIDispatchMethod(ITlibNode iDispatchMethodNode);
        void EnterIDispatchProperties(ITlibNode iDispatchPropertiesNode);
        void ExitIDispatchProperties(ITlibNode iDispatchPropertiesNode);
        void EnterInheritedInterfaces(ITlibNode inheritedInterfaceNode);
        void ExitInheritedInterfaces(ITlibNode inheritedInterfaceNode);
        void EnterInterface(ITlibNode interfaceNode);
        void ExitInterface(ITlibNode interfaceNode);
        void EnterMethod(ITlibNode methodNode);
        void ExitMethod(ITlibNode methodNode);
        void EnterModule(ITlibNode moduleNode);
        void ExitModule(ITlibNode moduleNode);
        void EnterModuleConst(ITlibNode moduleConstNode);
        void ExitModuleConst(ITlibNode moduleConstNode);
        void EnterRecord(ITlibNode recordNode);
        void ExitRecord(ITlibNode recordNode);
        void EnterRecordMember(ITlibNode recordMemberNode);
        void ExitRecordMember(ITlibNode recordMemberNode);
        void EnterTypeDef(ITlibNode typeDefNode);
        void ExitTypeDef(ITlibNode typeDefNode);
        void EnterTypeLib(ITlibNode libNode);
        void ExitTypeLib(ITlibNode libNode);
    }

    public abstract class TypeLibListenerBase : ITypeLibListener
    {
        public virtual void EnterChildrenIndirect(ITlibNode indirectChildrenNode) { }
        public virtual void ExitChildrenIndirect(ITlibNode indirectChildrenNode) { }
        public virtual void EnterCoClass(ITlibNode coClassNode) { }
        public virtual void ExitCoClass(ITlibNode coClassNode) { }
        public virtual void EnterCoClassInterface(ITlibNode coClassInterfaceNode) { }
        public virtual void ExitCoClassInterface(ITlibNode coClassInterfaceNode) { }
        public virtual void EnterDispInterface(ITlibNode dispInterfaceNode) { }
        public virtual void ExitDispInterface(ITlibNode dispInterfaceNode) { }
        public virtual void EnterDispInterfaceInheritedInterface(ITlibNode dispInterfaceInheritedInterface) { }
        public virtual void ExitDispInterfaceInheritedInterface(ITlibNode dispInterfaceInheritedInterface) { }
        public virtual void EnterDispProperty(ITlibNode dispPropertyNode) { }
        public virtual void ExitDispProperty(ITlibNode dispPropertyNode) { }
        public virtual void EnterEnum(ITlibNode enumNode) { }
        public virtual void ExitEnum(ITlibNode enumNode) { }
        public virtual void EnterEnumValue(ITlibNode enumValueNode) { }
        public virtual void ExitEnumValue(ITlibNode enumValueNode) { }
        public virtual void EnterIDispatchMethod(ITlibNode iDispatchMethodNode) { }
        public virtual void ExitIDispatchMethod(ITlibNode iDispatchMethodNode) { }
        public virtual void EnterIDispatchProperties(ITlibNode iDispatchPropertiesNode) { }
        public virtual void ExitIDispatchProperties(ITlibNode iDispatchPropertiesNode) { }
        public virtual void EnterInheritedInterfaces(ITlibNode inheritedInterfaceNode) { }
        public virtual void ExitInheritedInterfaces(ITlibNode inheritedInterfaceNode) { }
        public virtual void EnterInterface(ITlibNode interfaceNode) { }
        public virtual void ExitInterface(ITlibNode interfaceNode) { }
        public virtual void EnterMethod(ITlibNode methodNode) { }
        public virtual void ExitMethod(ITlibNode methodNode) { }
        public virtual void EnterModule(ITlibNode moduleNode) { }
        public virtual void ExitModule(ITlibNode moduleNode) { }
        public virtual void EnterModuleConst(ITlibNode moduleConstNode) { }
        public virtual void ExitModuleConst(ITlibNode moduleConstNode) { }
        public virtual void EnterRecord(ITlibNode recordNode) { }
        public virtual void ExitRecord(ITlibNode recordNode) { }
        public virtual void EnterRecordMember(ITlibNode recordMemberNode) { }
        public virtual void ExitRecordMember(ITlibNode recordMemberNode) { }
        public virtual void EnterTypeDef(ITlibNode typeDefNode) { }
        public virtual void ExitTypeDef(ITlibNode typeDefNode) { }
        public virtual void EnterTypeLib(ITlibNode libNode) { }
        public virtual void ExitTypeLib(ITlibNode libNode) { }
    }
}
