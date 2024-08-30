/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;
namespace ET.Client
{
    [UIEvent(WindowID.FGUILoginLayer)]
    [FriendOf(typeof (UIBaseWindow))]
    public class FGUILoginLayerEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.Normal;
        }
        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUILoginLayerViewComponent>();
            uiBaseWindow.AddComponent<FGUILoginLayerComponent>();
        }
        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUILoginLayerComponent>().RegisterUIEvent();
        }
        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<FGUILoginLayerComponent>().ShowWindow();
        }
        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUILoginLayerComponent>().HideWindow();
        }
        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUILoginLayerViewComponent>().ClearBindCache();
        }
    }
}