/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUIChooseServerLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIChooseServerLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIChooseServerLayerComponent self)
        {
        }

        public static void ShowWindow(this FGUIChooseServerLayerComponent self, Entity contextData = null)
        {
            PlayerComponent playerComponent = self.Root().GetComponent<PlayerComponent>();

            List<ServerInfo> serverInfos = playerComponent.ServerInfos;

            Log.Debug($"server infos count {serverInfos.Count} {self.Root().Id}");

            self.AddUIListItems(ref self.BaseWindows, serverInfos.Count, WindowID.ServerItemCell);

            UIEventComponent uiEventComponent = self.Root().GetComponent<UIEventComponent>();

            for (int i = 0; i < serverInfos.Count; i++)
            {
                
                ServerInfo serverInfo = serverInfos[i];
                //
                GComponent gComponent = self.View.List.AddItemFromPool(ResourcesUrlMap.ServerItemCell).asCom;
                //
                UIBaseWindow baseWindow = self.BaseWindows[i];
                //
                Log.Debug($"base windo {baseWindow.WindowID}");

                uiEventComponent.GetUIEventHandler(WindowID.ServerItemCell).BindComponent(baseWindow, gComponent);

                FGUIServerItemCellComponent itemCellComponent = baseWindow.GetComponent<FGUIServerItemCellComponent>();
                
                Log.Debug($"item cell component {itemCellComponent}");
                
                baseWindow.GetComponent<FGUIServerItemCellComponent>().SetInfo(serverInfo);
            }
        }

        public static void HideWindow(this FGUIChooseServerLayerComponent self)
        {
        }
    }
}