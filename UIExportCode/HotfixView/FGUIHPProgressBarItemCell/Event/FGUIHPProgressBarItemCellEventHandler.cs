/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;
namespace ET.Client
{
    [AUIEvent(WindowID.HPProgressBarItemCell)]
    [FriendOf(typeof (UIBaseWindow))]
    public class FGUIHPProgressBarItemCellEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.Normal;
        }
        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUIHPProgressBarItemCellViewComponent>();
            uiBaseWindow.AddComponent<FGUIHPProgressBarItemCellComponent>();
        }
        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIHPProgressBarItemCellComponent>().RegisterUIEvent();
        }
        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<FGUIHPProgressBarItemCellComponent>().ShowWindow();
        }
        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIHPProgressBarItemCellComponent>().HideWindow();
        }
        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUIHPProgressBarItemCellViewComponent>().ClearBindCache();
        }
    }
}