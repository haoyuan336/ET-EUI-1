using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ET.Client
{
    public static partial class SceneChangeHelper
    {
        // 场景切换协程
        public static async ETTask SceneChangeTo(Scene root, string sceneName, long sceneInstanceId)
        {
            root.RemoveComponent<AIComponent>();

            CurrentScenesComponent currentScenesComponent = root.GetComponent<CurrentScenesComponent>();

            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的

            Scene currentScene = CurrentSceneFactory.Create(sceneInstanceId, sceneName, currentScenesComponent);

            UnitComponent unitComponent = currentScene.AddComponent<UnitComponent>();

            // 可以订阅这个事件中创建Loading界面
            EventSystem.Instance.Publish(root, new SceneChangeStart());
            // 等待CreateMyUnit的消息
            Wait_CreateMyUnit waitCreateMyUnit = await root.GetComponent<ObjectWait>().Wait<Wait_CreateMyUnit>();
            
            M2C_CreateMyUnit m2CCreateMyUnit = waitCreateMyUnit.Message;
            
            Unit unit = UnitFactory.Create(currentScene, m2CCreateMyUnit.Unit);
            unitComponent.Add(unit);
            unitComponent.MyUnit = unit;
            root.RemoveComponent<AIComponent>();

            InitUnit(unit, m2CCreateMyUnit);

            InitTroopInfo(unit, m2CCreateMyUnit);

            EventSystem.Instance.Publish(currentScene, new SceneChangeFinish());
            // 通知等待场景切换的协程
            root.GetComponent<ObjectWait>().Notify(new Wait_SceneChangeFinish());
        }

        private static void InitUnit(Unit unit, M2C_CreateMyUnit createMyUnit)
        {
            List<HeroCardInfo> heroCardInfos = createMyUnit.HeroCardInfos;

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            if (heroCardComponent == null)
            {
                heroCardComponent = unit.AddComponent<HeroCardComponent>();
            }

            foreach (var info in heroCardInfos)
            {
                Log.Debug($"info hero id {info.HeroId}");
                HeroCard heroCard = heroCardComponent.AddChildWithId<HeroCard>(info.HeroId);

                heroCard.SetInfo(info);
            }
        }

        private static void InitTroopInfo(Unit unit, M2C_CreateMyUnit createMyUnit)
        {
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            if (troopComponent == null)
            {
                troopComponent = unit.AddComponent<TroopComponent>();
            }

            List<TroopInfo> troopInfos = createMyUnit.TroopInfos;

            foreach (var troopInfo in troopInfos)
            {
                Troop troop = troopComponent.AddChildWithId<Troop>(troopInfo.TroopId);

                troop.SetInfo(troopInfo);
            }
        }
    }
}