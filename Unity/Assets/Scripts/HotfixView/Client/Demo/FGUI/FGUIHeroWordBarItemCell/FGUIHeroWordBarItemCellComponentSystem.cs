/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Reflection;
using FairyGUI;
using WeChatWASM;

namespace ET.Client
{
    [FriendOf(typeof(FGUIHeroWordBarItemCellComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIHeroWordBarItemCellComponentSystem
    {
        public static void RegisterUIEvent(this FGUIHeroWordBarItemCellComponent self)
        {
        }

        public static void ShowWindow(this FGUIHeroWordBarItemCellComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIHeroWordBarItemCellComponent self)
        {
        }

        public static void SetInfo(this FGUIHeroWordBarItemCellComponent self, WordBarType wordBarType, HeroCard heroCard)
        {
            if (heroCard.Datas.TryGetValue(wordBarType.ToString(), out float value))
            {
                self.View.CurrentValue.text = value.ToString();

                Type type = heroCard.Config.GetType();

                PropertyInfo baseInfo = type.GetProperty(wordBarType.ToString());

                PropertyInfo growInfo = type.GetProperty(wordBarType.ToString() + "Grow");

                if (baseInfo == null || growInfo == null)
                {
                    return;
                }

                int baseValue = Convert.ToInt32(baseInfo.GetValue(heroCard.Config));

                int growValue = Convert.ToInt32(growInfo.GetValue(heroCard.Config));

                int nextValue = HeroCardHelper.GetHeroBaseDataValue(baseValue, growValue, heroCard.Level + 1, heroCard.Star);

                int addValue = nextValue - (int)value;

                self.View.NextAddValue.SetVar("value", addValue.ToString()).FlushVars();
            }
        }
    }
}