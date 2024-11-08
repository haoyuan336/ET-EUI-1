using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ET.Client;
using Microsoft.CodeAnalysis;

namespace ET
{
    [EntitySystemOf(typeof(HeroCard))]
    [FriendOfAttribute(typeof(ET.HeroCard))]
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

            // WordBarType[] wordBarTypes = new[] { WordBarType.Hp, WordBarType.Attack };

            List<WordBarConfig> wordBarConfigs = WordBarConfigCategory.Instance.GetAll().Values.ToList();

            foreach (var wordBarConfig in wordBarConfigs)
            {
                string wordBarType = wordBarConfig.WordBarType;

                string baseName = wordBarType;

                string growName = baseName + "Grow";

                PropertyInfo propertyInfo = type.GetProperty(baseName);

                if (propertyInfo == null)
                {
                    continue;
                }

                PropertyInfo growInfo = type.GetProperty(growName);

                float baseValue = Convert.ToSingle(propertyInfo.GetValue(self.Config));

                float growValue = 0;

                if (growInfo != null)
                {
                    growValue = Convert.ToSingle(growInfo.GetValue(self.Config));
                }

                float value = HeroCardHelper.GetHeroBaseDataValue(baseValue, growValue, self.Level, self.Star);

                self.Datas[baseName] = value;
            }
        }

        public static void SetInfo(this HeroCard self, HeroCardInfo info)
        {
            self.HeroConfigId = info.ConfigId;

            // for (int i = 0; i < info.DataKeys.Count; i++)
            // {
            //     string key = info.DataKeys[i];
            //
            //     float value = info.DataValues[i];
            //
            //     self.Datas[key] = value;
            // }

            self.Datas = info.Datas;

            self.Level = info.Level;

            self.Star = info.Star;
        }

        public static HeroCardInfo GetInfo(this HeroCard self)
        {
            HeroCardInfo info = HeroCardInfo.Create();

            info.ConfigId = self.HeroConfigId;

            info.HeroId = self.Id;

            // foreach (var kv in self.Datas)
            // {
            //     info.DataKeys.Add(kv.Key);
            //
            //     info.DataValues.Add(kv.Value);
            // }

            info.Datas = self.Datas;

            info.Level = self.Level;

            info.Star = self.Star;

            return info;
        }
    }
}