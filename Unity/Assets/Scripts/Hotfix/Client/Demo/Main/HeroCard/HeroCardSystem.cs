using System.Runtime.CompilerServices;

namespace ET.Client
{
    public static class HeroCardSystem
    {
        public static void SetInfo(this HeroCard self, HeroCardInfo info)
        {
            self.HeroConfigId = info.ConfigId;
        }

        public static HeroCardInfo GetInfo(this HeroCard self)
        {
            HeroCardInfo info = HeroCardInfo.Create();

            info.ConfigId = self.HeroConfigId;

            return info;
        }
    }
}