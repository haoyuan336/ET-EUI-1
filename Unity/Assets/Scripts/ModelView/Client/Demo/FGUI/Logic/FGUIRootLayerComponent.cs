/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIRootLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUIRootLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIRootLayerViewComponent>();
    }
}