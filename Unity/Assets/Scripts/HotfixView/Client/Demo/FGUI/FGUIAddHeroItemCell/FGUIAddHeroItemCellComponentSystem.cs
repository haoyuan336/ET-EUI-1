/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUIAddHeroItemCellComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIAddHeroItemCellComponentSystem
    {
        public static void RegisterUIEvent(this FGUIAddHeroItemCellComponent self)
        {
        }

        public static void ShowWindow(this FGUIAddHeroItemCellComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIAddHeroItemCellComponent self)
        {
        }

        public static void SetInfo(this FGUIAddHeroItemCellComponent self, HeroConfig heroConfig)
        {
            self.View.Label.text = "Add hero " + heroConfig.Id.ToString();

            self.HeroConfigId = heroConfig.Id;

            self.View.ClickButton.SetListener(self.OnClickButtonClick);
        }

        public static async void OnClickButtonClick(this FGUIAddHeroItemCellComponent self)
        {
            Log.Debug($"on add hero button click {self.HeroConfigId}");
            HeroCard heroCard = await HeroCardHelper.CreateNewHeroCardByConfigId(self.Root(), self.HeroConfigId);

            Log.Debug($"on click button click {heroCard.Id}");
        }
    }
}