/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIGameLoseLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUIGameLoseLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIGameLoseLayerViewComponent>();
    }
}