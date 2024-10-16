/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUIFormationLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIFormationLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIFormationLayerComponent self)
        {
            self.View.CloseButton.SetListener(self.OnCloseButtonClick);

            self.View.CardList.SetVirtual();

            self.View.CardList.defaultItem = ResourcesUrlMap.FormationCardItemCell;

            self.View.CardList.itemRenderer = self.OnItemRender;

            self.UIEventComponent = self.Root().GetComponent<UIEventComponent>();
        }

        private static void OnItemRender(this FGUIFormationLayerComponent self, int index, GObject gObject)
        {
            GComponent gComponent = gObject.asCom;

            UIBaseWindow baseWindow = self.UIBaseWindows[index];
            
            self.UIEventComponent.GetUIEventHandler(WindowID.FormationCardItemCell).BindComponent(baseWindow, gComponent);

            HeroCard heroCard = self.HeroCards[index];

            baseWindow.GetComponent<FGUIFormationCardItemCellComponent>().SetInfo(heroCard);
        }

        private static void OnCloseButtonClick(this FGUIFormationLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root(), new CloseLayerById() { WindowID = WindowID.FormationLayer });
        }

        public static void ShowWindow(this FGUIFormationLayerComponent self, Entity contextData = null)
        {
            self.HeroCards = HeroCardHelper.GetHeroCards(self.Root());

            self.AddUIListItems(ref self.UIBaseWindows, self.HeroCards.Count, WindowID.FormationCardItemCell);

            self.View.CardList.numItems = self.HeroCards.Count;
        }

        public static void HideWindow(this FGUIFormationLayerComponent self)
        {
        }
    }
}