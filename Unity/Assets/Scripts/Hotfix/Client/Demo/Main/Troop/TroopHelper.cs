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

                EventSystem.Instance.Publish(root, new CreateFightHero() { Unit = unit, HeroCardId = heroCardId, Index = index });
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

                EventSystem.Instance.Publish(root, new DisposeHeroObject() { Unit = unit, CardId = heroCardId });

                troop.HeroCardIds[index] = 0;
            }

            return response.Error;
        }
    }
}