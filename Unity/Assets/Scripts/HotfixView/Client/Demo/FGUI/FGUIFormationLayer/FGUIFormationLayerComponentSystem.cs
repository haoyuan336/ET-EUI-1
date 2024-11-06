/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
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

            FGUIFormationCardItemCellComponent itemCellComponent = baseWindow.GetComponent<FGUIFormationCardItemCellComponent>();

            itemCellComponent.SetInfo(heroCard);

            itemCellComponent.View.IsFormation.selectedPage = self.FormationCards.Contains(heroCard) ? "Yes" : "No";

            itemCellComponent.View.ClickButton.SetListener(self.OnHeroCardButtonClick, heroCard);
        }

        private static async void OnHeroCardButtonClick(this FGUIFormationLayerComponent self, HeroCard heroCard)
        {
            if (self.FormationCards.Contains(heroCard))
            {
                //找到第一个为空的card

                int index = self.FormationCards.FindIndex(a => a == heroCard);

                int errorCode = await TroopHelper.UnSetHeroFormation(self.Root(), self.CurrentTroop.Id, index);

                if (errorCode == ErrorCode.ERR_Success)
                {
                    self.FormationCards.Remove(heroCard);
                }

                self.ShowCurrentFormationCard();

                self.View.CardList.numItems = self.HeroCards.Count;
            }
            else
            {
                int index = self.FormationCards.FindIndex(a => a == null);

                if (index < 0)
                {
                    return;
                }

                int errorCode = await TroopHelper.SetHeroFormation(self.Root(), heroCard.Id, self.CurrentTroop.Id, index);

                if (errorCode == ErrorCode.ERR_Success)
                {
                    self.FormationCards.Add(heroCard);
                }

                self.ShowCurrentFormationCard();

                self.View.CardList.numItems = self.HeroCards.Count;
            }
        }

        private static void OnCloseButtonClick(this FGUIFormationLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root(), new CloseLayerById() { WindowID = WindowID.FormationLayer });
        }

        public static void ShowWindow(this FGUIFormationLayerComponent self, Entity contextData = null)
        {
            self.HeroCards = HeroCardHelper.GetHeroCards(self.Root());

            self.ShowCurrentFormationCard();

            self.AddUIListItems(ref self.UIBaseWindows, self.HeroCards.Count, WindowID.FormationCardItemCell);

            self.View.CardList.numItems = self.HeroCards.Count;
        }

        public static void ShowCurrentFormationCard(this FGUIFormationLayerComponent self)
        {
            Unit unit = UnitHelper.GetMyUnit(self.Root());

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            List<Troop> troops = TroopHelper.GetTroops(self.Root());

            Log.Debug($"troops count {troops.Count}");

            Troop troop = troops[0];

            self.CurrentTroop = troop;

            List<HeroCard> heroCards = new List<HeroCard>();

            foreach (var cardId in troop.HeroCardIds)
            {
                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(cardId);

                heroCards.Add(heroCard);
            }

            self.FormationCards = heroCards;

            Log.Debug($"formation cards {self.FormationCards.Count}");

            self.AddUIListItems(ref self.FormationBaseWinwdows, self.FormationCards.Count, WindowID.FormationCardItemCell);

            self.View.FormationList.RemoveChildrenToPool();

            UIEventComponent uiEventComponent = self.Root().GetComponent<UIEventComponent>();

            for (int i = 0; i < self.FormationCards.Count; i++)
            {
                HeroCard heroCard = self.FormationCards[i];

                UIBaseWindow uiBaseWindow = self.FormationBaseWinwdows[i];

                GComponent component = self.View.FormationList.AddItemFromPool(ResourcesUrlMap.FormationCardItemCell).asCom;

                uiEventComponent.GetUIEventHandler(WindowID.FormationCardItemCell).BindComponent(uiBaseWindow, component);

                FGUIFormationCardItemCellComponent itemCellComponent = uiBaseWindow.GetComponent<FGUIFormationCardItemCellComponent>();

                itemCellComponent.SetInfo(heroCard);
            }
        }

        public static void HideWindow(this FGUIFormationLayerComponent self)
        {
        }
    }
}