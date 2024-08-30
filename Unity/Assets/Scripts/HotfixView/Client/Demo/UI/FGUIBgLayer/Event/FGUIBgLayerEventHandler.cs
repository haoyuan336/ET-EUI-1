/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;
namespace ET.Client
{
    [AUIEvent(WindowID.BgLayer)]
    [FriendOf(typeof (UIBaseWindow))]
    public class FGUIBgLayerEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.Normal;
        }
        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUIBgLayerViewComponent>();
            uiBaseWindow.AddComponent<FGUIBgLayerComponent>();
        }
        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIBgLayerComponent>().RegisterUIEvent();
        }
        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<FGUIBgLayerComponent>().ShowWindow();
        }
        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIBgLayerComponent>().HideWindow();
        }
        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUIBgLayerViewComponent>().ClearBindCache();
        }
    }
}