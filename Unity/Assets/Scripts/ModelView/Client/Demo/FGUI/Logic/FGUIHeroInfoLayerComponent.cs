/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof (UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIHeroInfoLayerComponent: Entity, IAwake, IUILogic
    {
        public FGUIHeroInfoLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIHeroInfoLayerViewComponent>();

        public HeroCard HeroCard;

        public GameObject HeroSpine;
    }
}