/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUICallLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUICallLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUICallLayerViewComponent>();
    }
}