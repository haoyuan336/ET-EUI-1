/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [ComponentOf(typeof(UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUITeleportLayerComponent : Entity, IAwake, IUILogic
    {
        public FGUITeleportLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUITeleportLayerViewComponent>();

        public List<MapConfig> MapConfigs = null;

        public List<string> ChaperNames = new List<string>();
    }
}