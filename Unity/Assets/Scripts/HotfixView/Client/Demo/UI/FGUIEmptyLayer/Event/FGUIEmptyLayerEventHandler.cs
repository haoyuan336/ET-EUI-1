/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;
namespace ET.Client
{
    [AUIEvent(WindowID.EmptyLayer)]
    [FriendOf(typeof (UIBaseWindow))]
    public class FGUIEmptyLayerEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.Normal;
        }
        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUIEmptyLayerViewComponent>();
            uiBaseWindow.AddComponent<FGUIEmptyLayerComponent>();
        }
        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIEmptyLayerComponent>().RegisterUIEvent();
        }
        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<FGUIEmptyLayerComponent>().ShowWindow();
        }
        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIEmptyLayerComponent>().HideWindow();
        }
        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUIEmptyLayerViewComponent>().ClearBindCache();
        }
    }
}