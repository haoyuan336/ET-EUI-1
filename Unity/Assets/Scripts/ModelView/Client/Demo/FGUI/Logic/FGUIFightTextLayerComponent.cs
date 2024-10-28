/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [ComponentOf(typeof(UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIFightTextLayerComponent : Entity, IAwake, IUILogic
    {
        public FGUIFightTextLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIFightTextLayerViewComponent>();

        public Dictionary<string, Stack<Entity>> TextItemCellComponents =
                new Dictionary<string, Stack<Entity>>();
    }
}