/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIMainLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUIMainLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIMainLayerViewComponent>();

        public Dictionary<int, UIBaseWindow> BaseWindows = new Dictionary<int, UIBaseWindow>();

        public Stack<FGUIHPProgressItemCellComponent> FguihpProgressItemCellComponents = new Stack<FGUIHPProgressItemCellComponent>();
    }
}