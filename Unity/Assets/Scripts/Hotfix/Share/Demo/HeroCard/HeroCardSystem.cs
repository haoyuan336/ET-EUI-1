using System.Runtime.CompilerServices;
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

            self.Datas[WordBarType.Hp.ToString()] = self.Config.Hp;

            self.Datas[WordBarType.Attack.ToString()] = self.Config.Attack;
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

            return info;
        }
    }
}