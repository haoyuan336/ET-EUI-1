/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUILoginLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUILoginLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUILoginLayerViewComponent>();

        public string InputType = "Account";
    }
}