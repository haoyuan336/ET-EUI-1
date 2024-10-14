/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;
namespace ET.Client
{
    [AUIEvent(WindowID.HeroCardBagLayer)]
    [FriendOf(typeof (UIBaseWindow))]
    public class FGUIHeroCardBagLayerEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.Normal;
        }
        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUIHeroCardBagLayerViewComponent>();
            uiBaseWindow.AddComponent<FGUIHeroCardBagLayerComponent>();
        }
        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIHeroCardBagLayerComponent>().RegisterUIEvent();
        }
        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<FGUIHeroCardBagLayerComponent>().ShowWindow();
        }
        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIHeroCardBagLayerComponent>().HideWindow();
        }
        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUIHeroCardBagLayerViewComponent>().ClearBindCache();
        }
    }
}