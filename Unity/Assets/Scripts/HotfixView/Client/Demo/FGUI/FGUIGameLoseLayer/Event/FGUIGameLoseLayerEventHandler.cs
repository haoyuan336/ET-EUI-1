/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [AUIEvent(WindowID.GameLoseLayer)]
    [FriendOf(typeof(UIBaseWindow))]
    public class FGUIGameLoseLayerEventHandler : IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.PopUp;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUIGameLoseLayerViewComponent>();
            uiBaseWindow.AddComponent<FGUIGameLoseLayerComponent>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIGameLoseLayerComponent>().RegisterUIEvent();
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<FGUIGameLoseLayerComponent>().ShowWindow();
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIGameLoseLayerComponent>().HideWindow();
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }

        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUIGameLoseLayerViewComponent>().ClearBindCache();
        }
    }
}