using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    [MessageHandler(SceneType.Map)]
    public class M2M_UnitTransferRequestHandler: MessageHandler<Scene, M2M_UnitTransferRequest, M2M_UnitTransferResponse>
    {
        protected override async ETTask Run(Scene scene, M2M_UnitTransferRequest request, M2M_UnitTransferResponse response)
        {
            try
            {
                UnitComponent unitComponent = scene.GetComponent<UnitComponent>();

                Unit unit = MongoHelper.Deserialize<Unit>(request.Unit);

                Log.Debug($"M2M_UnitTransferRequest unit {unit.Id}");

                Log.Debug($"unitcomponent {unitComponent.Children.Count}");

                foreach (var kv in unitComponent.Children)
                {
                    Log.Debug($"kv {kv.Key} {kv.Value}");
                }

                Unit oldUnit = unitComponent.GetChild<Unit>(unit.Id);

                if (oldUnit != null)
                {
                    unitComponent.RemoveChild(unit.Id);
                }

                unitComponent.AddChild(unit);

                unitComponent.Add(unit);

                unit.AddComponent<UnitDBSaveComponent>();

                foreach (byte[] bytes in request.Entitys)
                {
                    Entity entity = MongoHelper.Deserialize<Entity>(bytes);
                    unit.AddComponent(entity);
                }

                unit.AddComponent<MoveComponent>();
                unit.AddComponent<PathfindingComponent, string>(scene.Name);
                unit.Position = new float3(-10, 0, -10);

                unit.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.OrderedMessage);

                // 通知客户端开始切场景
                M2C_StartSceneChange m2CStartSceneChange = M2C_StartSceneChange.Create();
                m2CStartSceneChange.SceneInstanceId = scene.InstanceId;
                m2CStartSceneChange.SceneName = scene.Name;
                MapMessageHelper.SendToClient(unit, m2CStartSceneChange);

                // 通知客户端创建My Unit
                M2C_CreateMyUnit m2CCreateUnits = M2C_CreateMyUnit.Create();

                m2CCreateUnits.Unit = UnitHelper.CreateUnitInfo(unit);

                Log.Debug("获取当前的地图信息");
                m2CCreateUnits.HeroCardInfos = this.GetHeroCardInfos(unit);

                m2CCreateUnits.TroopInfos = this.GetTroopInfos(unit);

                MapMessageHelper.SendToClient(unit, m2CCreateUnits);

                // 加入aoi
                unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);

                // 解锁location，可以接收发给Unit的消息
                await scene.Root().GetComponent<LocationProxyComponent>().UnLock(LocationType.Unit, unit.Id, request.OldActorId, unit.GetActorId());
            }
            catch (Exception e)
            {
                Log.Debug($"M2M_UnitTransferRequest {e}");
            }
        }

        private List<TroopInfo> GetTroopInfos(Unit unit)
        {
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            if (troopComponent == null)
            {
                Log.Debug("troop component is null");
                troopComponent = unit.AddComponent<TroopComponent>();
            }

            List<TroopInfo> troopInfos = new List<TroopInfo>();

            foreach (var kv in troopComponent.Children)
            {
                Troop troop = kv.Value as Troop;

                troopInfos.Add(troop.GetInfo());
            }

            return troopInfos;
        }

        private List<HeroCardInfo> GetHeroCardInfos(Unit unit)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            Log.Debug($"hero card component is null {heroCardComponent == null}");
            
            if (heroCardComponent == null)
            {
                heroCardComponent = unit.AddComponent<HeroCardComponent>();
            }

            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();

            foreach (var kv in heroCardComponent.Children)
            {
                HeroCard card = kv.Value as HeroCard;

                heroCardInfos.Add(card.GetInfo());
            }

            return heroCardInfos;
        }
    }
}