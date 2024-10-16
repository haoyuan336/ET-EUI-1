/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [AUIEvent(WindowID.FormationLayer)]
    [FriendOf(typeof(UIBaseWindow))]
    public class FGUIFormationLayerEventHandler : IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.PopUp;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUIFormationLayerViewComponent>();
            uiBaseWindow.AddComponent<FGUIFormationLayerComponent>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIFormationLayerComponent>().RegisterUIEvent();
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<FGUIFormationLayerComponent>().ShowWindow();
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIFormationLayerComponent>().HideWindow();
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }

        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUIFormationLayerViewComponent>().ClearBindCache();
        }
    }
}