/** This is an automatically generated class by FairyGUI. Please do not modify it. **/


using FairyGUI;
namespace ET.Client
{
    [FriendOf(typeof (FGUIServerItemCellComponent))]
    [FriendOf(typeof (UIBaseWindow))]
    public static class FGUIServerItemCellComponentSystem
    {
        public static void RegisterUIEvent(this FGUIServerItemCellComponent self)
        {
        }
        public static void ShowWindow(this FGUIServerItemCellComponent self, Entity contextData = null)
        {
        }
        public static void HideWindow(this FGUIServerItemCellComponent self)
        {
        }

        public static void SetInfo(this FGUIServerItemCellComponent self, ServerInfo serverInfo)
        {
            self.View.ServerName.text = serverInfo.ServerName;
        }
    }
}