/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUIHeroCardBagLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIHeroCardBagLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIHeroCardBagLayerComponent self)
        {
            self.View.CloseButton.SetListener(self.OnCloseButtonClick);

            self.View.HeroCardList.SetVirtual();

            self.View.HeroCardList.defaultItem = ResourcesUrlMap.HeroCardItemCell;

            self.View.HeroCardList.itemRenderer = self.OnItemRenderer;

            self.UIEventComponent = self.Root().GetComponent<UIEventComponent>();
        }

        private static void OnItemRenderer(this FGUIHeroCardBagLayerComponent self, int index, GObject gObject)
        {
            GComponent gComponent = gObject.asCom;

            UIBaseWindow baseWindow = self.UIBaseWindows[index];

            HeroCard heroCard = self.HeroCards[index];

            self.UIEventComponent.GetUIEventHandler(WindowID.HeroCardItemCell).BindComponent(baseWindow, gComponent);

            FGUIHeroCardItemCellComponent itemCellComponent = baseWindow.GetComponent<FGUIHeroCardItemCellComponent>();

            itemCellComponent.SetInfo(heroCard);
        }

        private static void OnCloseButtonClick(this FGUIHeroCardBagLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root(), new CloseLayerById() { WindowID = WindowID.HeroCardBagLayer });
        }

        public static void ShowWindow(this FGUIHeroCardBagLayerComponent self, Entity contextData = null)
        {
            self.HeroCards = HeroCardHelper.GetHeroCards(self.Root());

            self.AddUIListItems(ref self.UIBaseWindows, self.HeroCards.Count, WindowID.HeroCardItemCell);

            self.View.HeroCardList.numItems = self.HeroCards.Count;
        }

        public static void HideWindow(this FGUIHeroCardBagLayerComponent self)
        {
        }
    }
}