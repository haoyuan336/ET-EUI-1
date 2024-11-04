using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [ChildOf(typeof(InteractivePointComponent))]
    public class InteractivePoint : Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject BindObject;

        public ColliderAction ColliderAction;

        public GComponent GComponent;

        public FGUIInteractivePointItemCellComponent InteractivePointItemCellComponent;

        public FGUIFightTextLayerComponent FightTextLayerComponent;
    }
}