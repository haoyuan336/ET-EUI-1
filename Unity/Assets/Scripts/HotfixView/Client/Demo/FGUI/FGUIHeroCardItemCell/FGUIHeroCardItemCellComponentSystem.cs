/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

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