using System.Collections.Generic;

namespace ET.Client
{
    public static class HeroCardHelper
    {
        public static async ETTask<HeroCard> CreateNewHeroCardByConfigId(Scene root, int configId)
        {
            C2M_CreateOneHeroByConfigId request = C2M_CreateOneHeroByConfigId.Create();

            request.ConfigId = configId;

            M2C_CreateOneHeroByConfigId response = await root.GetComponent<ClientSenderComponent>().Call(request) as M2C_CreateOneHeroByConfigId;

            if (response.Error == ErrorCode.ERR_Success)
            {
                HeroCardInfo info = response.HeroCardInfo;

                UnitComponent unitComponent = root.CurrentScene().GetComponent<UnitComponent>();

                Unit unit = unitComponent.MyUnit;

                HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

                HeroCard heroCard = heroCardComponent.AddChildWithId<HeroCard>(info.HeroId);

                heroCard.SetInfo(info);

                return heroCard;
            }

            return null;
        }

        public static List<HeroCard> GetHeroCards(Scene root)
        {
            Unit unit = root.CurrentScene().GetComponent<UnitComponent>().MyUnit;

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            List<HeroCard> heroCards = new List<HeroCard>();

            foreach (var kv in heroCardComponent.Children)
            {
                HeroCard heroCard = kv.Value as HeroCard;
                
                heroCards.Add(heroCard);
            }
            
            return heroCards;
        }
}
}