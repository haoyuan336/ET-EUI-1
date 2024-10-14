namespace ET.Server
{
    public static class HeroCardSystem
    {
        public static HeroCardInfo GetInfo(this HeroCard self)
        {
            HeroCardInfo heroCardInfo = HeroCardInfo.Create();

            heroCardInfo.HeroId = self.Id;

            heroCardInfo.ConfigId = self.HeroConfigId;
            
            return heroCardInfo;
        }
    }
}