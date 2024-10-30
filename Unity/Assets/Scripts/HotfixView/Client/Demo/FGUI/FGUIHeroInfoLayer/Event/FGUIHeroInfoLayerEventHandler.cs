/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [AUIEvent(WindowID.HeroInfoLayer)]
    [FriendOf(typeof(UIBaseWindow))]
    public class FGUIHeroInfoLayerEventHandler : IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.windowType = UIWindowType.PopUp;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<FGUIHeroInfoLayerViewComponent>();
            uiBaseWindow.AddComponent<FGUIHeroInfoLayerComponent>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIHeroInfoLayerComponent>().RegisterUIEvent();
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            ShowWindowData showWindowData = contextData as ShowWindowData;

            uiBaseWindow.GetComponent<FGUIHeroInfoLayerComponent>().ShowWindow(showWindowData.Entity);
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<FGUIHeroInfoLayerComponent>().HideWindow();
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }

        public void BindComponent(UIBaseWindow uiBaseWindow, GComponent gComponent)
        {
            uiBaseWindow.GComponent = gComponent;
            uiBaseWindow.GetComponent<FGUIHeroInfoLayerViewComponent>().ClearBindCache();
        }
    }
}