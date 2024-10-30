using System.Collections.Generic;

namespace ET.Client
{
    public static class HeroCardHelper
    {
#if UNITY

        public static async ETTask<int> UpLevel(HeroCard heroCard)
        {
            C2M_UpHeroLevelRequest request = C2M_UpHeroLevelRequest.Create();

            request.HeroId = heroCard.Id;

            M2C_UpHeroLevelResponse response = await heroCard.Root().GetComponent<ClientSenderComponent>().Call(request) as M2C_UpHeroLevelResponse;

            if (response.Error == ErrorCode.ERR_Success)
            {
                heroCard.SetInfo(response.HeroCardInfo);

                EventSystem.Instance.Publish(heroCard.Root(), new HeroLevelUp() { HeroCard = heroCard });
            }

            return response.Error;
        }

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

        public static async ETTask<int> DestroyHeroCard(HeroCard heroCard)
        {
            if (heroCard == null)
            {
                return 0;
            }

            C2M_DestroyHeroByIdRequest request = new C2M_DestroyHeroByIdRequest()
            {
                HeroId = heroCard.Id
            };

            M2C_DestroyHeroByIdResponse response =
                    await heroCard.Root().GetComponent<ClientSenderComponent>().Call(request) as M2C_DestroyHeroByIdResponse;

            Scene scene = heroCard.Root();

            if (response.Error == ErrorCode.ERR_Success)
            {
                long heroId = heroCard.Id;

                heroCard.Dispose();

                List<Troop> troops = TroopHelper.GetTroops(scene);

                Troop troop = troops[0];

                int index = 0;

                for (int i = 0; i < troop.HeroCardIds.Length; i++)
                {
                    if (troop.HeroCardIds[i] == heroId)
                    {
                        index = i;

                        break;
                    }
                }

                await TroopHelper.UnSetHeroFormation(scene, troop.Id, index);
            }

            return response.Error;
        }
#endif

        /// <summary>
        /// 获取英雄的等级 星级最终值
        /// </summary>
        /// <param name="baseValue">基础数值</param>
        /// <param name="grow">成长值</param>
        /// <param name="level">等级</param>
        /// <param name="star">星级</param>
        /// <returns></returns>
        public static int GetHeroBaseDataValue(int baseValue, int grow, int level, int star)
        {
            return baseValue + grow * level + grow * star * 10;
        }


    }
}