/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using System.Linq;
using FairyGUI;
namespace ET.Client
{
    [FriendOf(typeof (FGUIMainLayerComponent))]
    [FriendOf(typeof (UIBaseWindow))]
    public static class FGUIMainLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIMainLayerComponent self)
        {
            
        }
        public static void ShowWindow(this FGUIMainLayerComponent self, Entity contextData = null)
        {
            self.View.List.RemoveChildrenToPool();
            
            self.AddUIListItems(ref self.BaseWindows, HeroConfigCategory.Instance.GetAll().Count, WindowID.AddHeroItemCell);

            List<HeroConfig> heroConfigs = HeroConfigCategory.Instance.GetAll().Values.ToList();

            UIEventComponent uiEventComponent = self.Root().GetComponent<UIEventComponent>();
            
            for (int i = 0; i < heroConfigs.Count; i++)
            {
                HeroConfig heroConfig = heroConfigs[i];

                UIBaseWindow baseWindow = self.BaseWindows[i];

                GComponent goComponent = self.View.List.AddItemFromPool(ResourcesUrlMap.AddHeroItemCell).asCom;
                
                uiEventComponent.GetUIEventHandler(WindowID.AddHeroItemCell).BindComponent(baseWindow, goComponent);

                FGUIAddHeroItemCellComponent itemCellComponent = baseWindow.GetComponent<FGUIAddHeroItemCellComponent>();

                itemCellComponent.SetInfo(heroConfig);

            }
            
            
        }
        public static void HideWindow(this FGUIMainLayerComponent self)
        {
        }
    }
}