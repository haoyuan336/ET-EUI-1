/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using FairyGUI;

namespace ET.Client
{
    [FriendOf(typeof(FGUIHeadItemBarLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIHeadItemBarLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIHeadItemBarLayerComponent self)
        {
        }

        public static void ShowWindow(this FGUIHeadItemBarLayerComponent self, Entity contextData = null)
        {
            List<ItemType> itemTypes = new List<ItemType>()
            {
                ItemType.Gold, ItemType.Diamond,
                ItemType.Meat, ItemType.Wood, ItemType.BlueStone
            };

            Unit unit = UnitHelper.GetMyUnit(self.Root());

            ItemComponent itemComponent = unit.GetComponent<ItemComponent>();

            Type type = self.View.GetType();

            foreach (var itemType in itemTypes)
            {
                string name = itemType.ToString() + "ItemComponent";

                ItemConfig itemConfig = ItemConfigCategory.Instance.GetConfigByType(itemType);

                int count = itemComponent.Items[itemConfig.Id.ToString()];

                PropertyInfo propertyInfo = type.GetProperty(name);

                if (propertyInfo == null)
                {
                    continue;
                }

                FGUIItemBarItemCellComponent itemCellComponent = propertyInfo.GetValue(self.View) as FGUIItemBarItemCellComponent;

                itemCellComponent.SetInfo(itemConfig.Id, count);
            }

            self.SetUnitInfo(unit);
        }

        public static void UpdateItemCount(this FGUIHeadItemBarLayerComponent self, string key, int count)
        {
            ItemConfig itemConfig = ItemConfigCategory.Instance.Get(int.Parse(key));

            string name = itemConfig.ItemType + "ItemComponent";

            Type type = self.View.GetType();

            PropertyInfo propertyInfo = type.GetProperty(name);

            FGUIItemBarItemCellComponent itemBarLayerComponent = propertyInfo.GetValue(self.View) as FGUIItemBarItemCellComponent;

            itemBarLayerComponent.SetInfo(int.Parse(key), count);
        }

        public static void SetUnitInfo(this FGUIHeadItemBarLayerComponent self, Unit unit)
        {
            self.View.UnitLevel.SetVar("level", unit.Level.ToString()).FlushVars();

            UnitUpLevelExpConfig unitUpLevelExpConfig = UnitUpLevelExpConfigCategory.Instance.GetByLevel(unit.Level + 1);

            self.View.ExpProgress.min = 0;

            self.View.ExpProgress.value = unit.CurrentExp;

            self.View.ExpProgress.max = unitUpLevelExpConfig.NeedExp;

            self.View.FightPower.text = unit.FightPower.ToString();
        }

        public static void HideWindow(this FGUIHeadItemBarLayerComponent self)
        {
        }
    }
}