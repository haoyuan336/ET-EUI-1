/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIHeroCardBagLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUIHeroCardBagLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIHeroCardBagLayerViewComponent>();

        public Dictionary<int, UIBaseWindow> UIBaseWindows = new Dictionary<int, UIBaseWindow>();

        public List<HeroCard> HeroCards = null;

        public UIEventComponent UIEventComponent = null;
    }
}