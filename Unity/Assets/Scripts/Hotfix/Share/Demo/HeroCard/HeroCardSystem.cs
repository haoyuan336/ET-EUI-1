using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using ET.Client;
using Microsoft.CodeAnalysis;

namespace ET
{
    [EntitySystemOf(typeof (HeroCard))]
    [FriendOfAttribute(typeof (ET.HeroCard))]
    public static partial class HeroCardSystem
    {
        [EntitySystem]
        public static void Awake(this HeroCard self, int heroConfigId)
        {
            self.HeroConfigId = heroConfigId;

            self.UpdateValueData();
        }

        public static void UpdateValueData(this HeroCard self)
        {
            Type type = self.Config.GetType();

            WordBarType[] wordBarTypes = new[] { WordBarType.Hp, WordBarType.Attack };

            foreach (var wordBarType in wordBarTypes)
            {
                string baseName = wordBarType.ToString();

                string growName = baseName + "Grow";

                PropertyInfo propertyInfo = type.GetProperty(baseName);

                PropertyInfo growPInfo = type.GetProperty(growName);

                int baseValue = Convert.ToInt32(propertyInfo.GetValue(self.Config));

                int growValue = Convert.ToInt32(growPInfo.GetValue(self.Config));

                int value = HeroCardHelper.GetHeroBaseDataValue(baseValue, growValue, self.Level, self.Star);

                self.Datas[baseName] = value;
            }
        }

        public static void SetInfo(this HeroCard self, HeroCardInfo info)
        {
            self.HeroConfigId = info.ConfigId;

            for (int i = 0; i < info.DataKeys.Count; i++)
            {
                string key = info.DataKeys[i];

                float value = info.DataValues[i];

                self.Datas[key] = value;
            }

            self.Level = info.Level;

            self.Star = info.Star;
        }

        public static HeroCardInfo GetInfo(this HeroCard self)
        {
            HeroCardInfo info = HeroCardInfo.Create();

            info.ConfigId = self.HeroConfigId;

            info.HeroId = self.Id;

            foreach (var kv in self.Datas)
            {
                info.DataKeys.Add(kv.Key);

                info.DataValues.Add(kv.Value);
            }

            info.Level = self.Level;

            info.Star = self.Star;

            return info;
        }
    }
}