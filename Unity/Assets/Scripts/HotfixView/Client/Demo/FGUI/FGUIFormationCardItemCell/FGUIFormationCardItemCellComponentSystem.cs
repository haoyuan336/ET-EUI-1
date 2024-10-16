/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUIFormationCardItemCellComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIFormationCardItemCellComponentSystem
    {
        public static void RegisterUIEvent(this FGUIFormationCardItemCellComponent self)
        {
        }

        public static void ShowWindow(this FGUIFormationCardItemCellComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIFormationCardItemCellComponent self)
        {
        }

        public static void SetInfo(this FGUIFormationCardItemCellComponent self, HeroCard heroCard)
        {
            self.View.HeroName.text = heroCard.Config.HeroName;
            
        }
    }
}