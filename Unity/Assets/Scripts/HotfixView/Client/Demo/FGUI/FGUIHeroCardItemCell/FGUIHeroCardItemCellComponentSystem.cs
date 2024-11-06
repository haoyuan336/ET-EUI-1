/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEditor.UI;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(FGUIHeroCardItemCellComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIHeroCardItemCellComponentSystem
    {
        public static void RegisterUIEvent(this FGUIHeroCardItemCellComponent self)
        {
        }

        public static void ShowWindow(this FGUIHeroCardItemCellComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIHeroCardItemCellComponent self)
        {
        }

        public static void SetInfo(this FGUIHeroCardItemCellComponent self, HeroCard heroCard)
        {
            self.HeroCard = heroCard;

            self.View.HeroName.text = heroCard.Config.HeroName;

            self.View.DestroyButton.SetListener(self.OnDestroyButtonClick);

            self.View.ClickButton.SetListener(self.OnButtonClick);
            
            self.View.Level.SetVar("Level", self.HeroCard.Level.ToString()).FlushVars();
            
        }

        private static void OnButtonClick(this FGUIHeroCardItemCellComponent self)
        {
            ShowWindowData data = new ShowWindowData() { Entity = self.HeroCard };

            EventSystem.Instance.Publish(self.Root(),
                new PushLayerById() { WindowID = WindowID.HeroInfoLayer, ShowWindowData = data });

            // UIComponent uiComponent = self.Root().GetComponent<UIComponent>();
            //
            // FGUIHeroInfoLayerComponent heroInfoLayerComponent = uiComponent.GetDlgLogic<FGUIHeroInfoLayerComponent>();
            //
            // heroInfoLayerComponent.SetHeroInfo(self.HeroCard);
        }

        private static async void OnDestroyButtonClick(this FGUIHeroCardItemCellComponent self)
        {
            await HeroCardHelper.DestroyHeroCard(self.HeroCard);

            UIComponent uiComponent = self.Root().GetComponent<UIComponent>();

            FGUIHeroCardBagLayerComponent bagLayerComponent = uiComponent.GetDlgLogic<FGUIHeroCardBagLayerComponent>();

            bagLayerComponent.RefreCardList();
        }
    }
}