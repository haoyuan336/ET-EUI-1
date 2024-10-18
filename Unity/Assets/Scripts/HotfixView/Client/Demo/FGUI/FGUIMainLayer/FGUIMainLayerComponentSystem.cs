/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(FGUIMainLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIMainLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIMainLayerComponent self)
        {
            self.View.HeroBagButton.SetListener(self.OnHeroBagButtonClick);

            self.View.JoyStickLayerComponent.RegisterUIEvent();

            self.View.JoyStickLayerComponent.JoyAction = self.OnJoyAction;

            self.View.JoyStickLayerComponent.StartJoyAction = self.OnStartJoyAction;

            self.View.JoyStickLayerComponent.EndJoyAction = self.OnEndJoyAction;

            self.View.FormationButton.SetListener(self.OnFormationButtonClick);
        }

        private static void OnEndJoyAction(this FGUIMainLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root().CurrentScene(), new EndMoveUnitPos());
        }

        private static void OnStartJoyAction(this FGUIMainLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root().CurrentScene(), new StartMoveUnitPos());
        }

        private static void OnFormationButtonClick(this FGUIMainLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root(), new ShowLayerById() { WindowID = WindowID.FormationLayer });
        }

        private static void OnJoyAction(this FGUIMainLayerComponent self, Vector2 direction, float power)
        {
            EventSystem.Instance.Publish(self.Root().CurrentScene(), new MoveUnitPos() { Vector2 = direction, Power = power });
        }

        private static void OnHeroBagButtonClick(this FGUIMainLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root(), new ShowLayerById() { WindowID = WindowID.HeroCardBagLayer });
        }

        public static void ShowWindow(this FGUIMainLayerComponent self, Entity contextData = null)
        {
            self.View.List.RemoveChildrenToPool();

            self.AddUIListItems(ref self.BaseWindows, HeroConfigCategory.Instance.GetAll().Count, WindowID.AddHeroItemCell);

            List<HeroConfig> heroConfigs = HeroConfigCategory.Instance.GetAll().Values.ToList();

            UIEventComponent uiEventComponent = self.Root().GetComponent<UIEventComponent>();

            for (int i = 0; i < heroConfigs.Count; i++)
            {
                HeroConfig heroConfig = heroConfigs[i];

                UIBaseWindow baseWindow = self.BaseWindows[i];

                GComponent goComponent = self.View.List.AddItemFromPool(ResourcesUrlMap.AddHeroItemCell).asCom;

                uiEventComponent.GetUIEventHandler(WindowID.AddHeroItemCell).BindComponent(baseWindow, goComponent);

                FGUIAddHeroItemCellComponent itemCellComponent = baseWindow.GetComponent<FGUIAddHeroItemCellComponent>();

                itemCellComponent.SetInfo(heroConfig);
            }
        }

        public static void HideWindow(this FGUIMainLayerComponent self)
        {
        }
    }
}