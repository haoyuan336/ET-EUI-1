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
            self.View.HeroName.text = heroCard.Config.HeroName;
        }
    }
}