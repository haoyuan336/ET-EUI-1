using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    public static class TroopHelper
    {
        public static List<Troop> GetTroops(Scene scene)
        {
            UnitComponent unitComponent = scene.CurrentScene().GetComponent<UnitComponent>();

            Unit unit = unitComponent.MyUnit;

            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            List<Troop> troops = new List<Troop>();

            foreach (var kv in troopComponent.Children)
            {
                Troop troop = kv.Value as Troop;

                troops.Add(troop);
            }

            return troops;
        }

        public static async ETTask<int> SetHeroFormation(Scene root, long heroCardId, long troopId, int index)
        {
            C2M_SetHeroFormation request = C2M_SetHeroFormation.Create();

            request.TroopId = troopId;

            request.HeroCardId = heroCardId;

            request.Index = index;

            M2C_SetHeroFormation response = await root.GetComponent<ClientSenderComponent>().Call(request) as M2C_SetHeroFormation;

            if (response.Error == ErrorCode.ERR_Success)
            {
                Unit unit = UnitHelper.GetMyUnit(root);

                TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

                Troop troop = troopComponent.GetChild<Troop>(troopId);

                troop.HeroCardIds[index] = heroCardId;

                HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

                heroCardComponent.FormationHeroCards.Clear();

                foreach (var cardId in troop.HeroCardIds)
                {
                    HeroCard card = heroCardComponent.GetChild<HeroCard>(cardId);

                    if (card != null)
                    {
                        heroCardComponent.FormationHeroCards.Add(card);
                    }
                }

                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardId);

                EventSystem.Instance.Publish(root, new CreateHeroObject() { HeroCard = heroCard, Unit = unit, Index = index });
            }

            return response.Error;
        }

        public static async ETTask<int> UnSetHeroFormation(Scene root, long troopId, int index)
        {
            C2M_UnSetHeroFromation request = C2M_UnSetHeroFromation.Create();

            request.Index = index;

            request.TroopId = troopId;

            M2C_UnSetHeroFormation response = await root.GetComponent<ClientSenderComponent>().Call(request) as M2C_UnSetHeroFormation;

            if (response.Error == ErrorCode.ERR_Success)
            {
                Unit unit = UnitHelper.GetMyUnit(root);

                TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

                Troop troop = troopComponent.GetChild<Troop>(troopId);

                long heroCardId = troop.HeroCardIds[index];

                HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

                heroCardComponent.FormationHeroCards.Clear();

                foreach (var cardId in troop.HeroCardIds)
                {
                    HeroCard card = heroCardComponent.GetChild<HeroCard>(cardId);

                    if (card != null)
                    {
                        heroCardComponent.FormationHeroCards.Add(card);
                    }
                }

                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardId);

                EventSystem.Instance.Publish(root, new DisposeHeroObject() { HeroCard = heroCard });

                troop.HeroCardIds[index] = 0;
            }

            return response.Error;
        }
    }
}