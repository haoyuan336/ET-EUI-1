/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIInteractivePointItemCellComponent : Entity, IAwake, IUILogic, IUpdate
    {
        public FGUIInteractivePointItemCellViewComponent View =>
                this.GetParent<UIBaseWindow>().GetComponent<FGUIInteractivePointItemCellViewComponent>();

        public GameObject GameObject;
    }
}