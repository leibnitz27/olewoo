using Org.Benf.OleWoo.Typelib;

namespace Org.Benf.OleWoo.Listeners
{
    public interface ITypeLibListener
    {
        void EnterChildrenIndirect(ITlibNode indirectChildrenNode);
        void ExitChildrenIndirect(ITlibNode indirectChildrenNode);
        void EnterCoClass(ITlibNode coClassNode);
        void ExitCoClass(ITlibNode coClassNode);
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
}
