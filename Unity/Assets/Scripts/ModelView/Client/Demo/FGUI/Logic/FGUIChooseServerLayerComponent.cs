/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIChooseServerLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUIChooseServerLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIChooseServerLayerViewComponent>();

        public Dictionary<int, UIBaseWindow> BaseWindows = new Dictionary<int, UIBaseWindow>();

        // public List<UIBaseWindow> BaseWindows = new List<UIBaseWindow>();
    }
}