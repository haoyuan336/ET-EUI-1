/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Linq;
using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUITeleportLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUITeleportLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUITeleportLayerComponent self)
        {
            self.View.CloseButton.SetListener(self.OnCloseButtonClick);
        }
        //
        // private static void OnChapterRender(this FGUITeleportLayerComponent self, int index, GObject gObject)
        // {
        //     GButton gButton = gObject.asButton;
        //
        //     gButton.mode = ButtonMode.Check;
        //
        //     gButton.SetListener(self.OnChapterButtonClick, index);
        //
        //     string chapterName = self.ChaperNames[index];
        //     
        //     gButton.text = chapterName;
        // }

        private static void OnChapterButtonClick(this FGUITeleportLayerComponent self, int index)
        {
            Log.Debug($"on chapter button click {index}");

            string chapterName = self.ChaperNames[index];

            self.MapConfigs = MapConfigCategory.Instance.ChapterList[chapterName];

            self.View.MapList.RemoveChildrenToPool();

            Unit unit = UnitHelper.GetMyUnit(self.Root());

            for (int i = 0; i < self.MapConfigs.Count; i++)
            {
                MapConfig mapConfig = self.MapConfigs[i];

                GComponent gComponent = self.View.MapList.AddItemFromPool(ResourcesUrlMap.TeleportMapItemCell).asCom;

                GTextField mapTitle = gComponent.GetChild("MapTitle").asTextField;

                GTextField fightPower = gComponent.GetChild("FightPower").asTextField;

                mapTitle.text = mapConfig.MapName;

                fightPower.text = mapConfig.FightPower.ToString();

                Controller controller = gComponent.GetController("IsCurrentMap");

                controller.selectedIndex = mapConfig.Id == unit.CurrentMapConfigId ? 1 : 0;

                GButton gButton = gComponent.GetChild("ClickButton").asButton;

                gButton.SetListener(self.OnMapButtonClick, mapConfig);
            }
        }

        private static void OnMapButtonClick(this FGUITeleportLayerComponent self, MapConfig mapConfig)
        {
            Log.Debug($"On map button click {mapConfig.Id}");
            Unit unit = UnitHelper.GetMyUnit(self.Root());

            if (mapConfig.Id == unit.CurrentMapConfigId)
            {
                return;
            }

            Scene scene = self.Root();

            EventSystem.Instance.Publish(scene, new PopLayer());

            EventSystem.Instance.Publish(scene, new TeleportUnitToMap()
            {
                MapConfigId = mapConfig.Id
            });
        }

        private static void OnCloseButtonClick(this FGUITeleportLayerComponent self)
        {
            EventSystem.Instance.Publish(self.Root(), new PopLayer());
        }

        public static void ShowWindow(this FGUITeleportLayerComponent self, Entity contextData = null)
        {
            self.View.ChapterList.RemoveChildrenToPool();

            self.ChaperNames = MapConfigCategory.Instance.ChapterList.Keys.ToList();

            for (int i = 0; i < self.ChaperNames.Count; i++)
            {
                string chapterName = self.ChaperNames[i];

                GButton gButton = self.View.ChapterList.AddItemFromPool(ResourcesUrlMap.ChapterButton).asButton;

                gButton.text = chapterName;

                gButton.AddListener(self.OnChapterButtonClick, i);

                if (i == 0)
                {
                    gButton.selected = true;

                    self.OnChapterButtonClick(i);
                }
            }
        }

        public static void HideWindow(this FGUITeleportLayerComponent self)
        {
        }
    }
}