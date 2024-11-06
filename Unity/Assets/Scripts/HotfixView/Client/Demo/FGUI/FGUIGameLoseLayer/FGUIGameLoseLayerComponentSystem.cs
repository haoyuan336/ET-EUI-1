/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(FGUIGameLoseLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIGameLoseLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIGameLoseLayerComponent self)
        {
            self.View.RetryButton.SetListener(self.OnRetryButtonClick);

            self.View.BackMainButton.SetListener(self.OnBackButtonClick);
        }

        public static void OnBackButtonClick(this FGUIGameLoseLayerComponent self)
        {
            Scene root = self.Root();

            EventSystem.Instance.Publish(root, new CloseLayerById() { WindowID = WindowID.GameLoseLayer });
            Log.Debug("back button click");
            EventSystem.Instance.Publish(root, new TeleportUnitToMap() { MapConfigId = MapConfigCategory.Instance.GetMainCity().Id });
        }

        public static void OnRetryButtonClick(this FGUIGameLoseLayerComponent self)
        {
        }

        public static void ShowWindow(this FGUIGameLoseLayerComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIGameLoseLayerComponent self)
        {
        }
    }
}