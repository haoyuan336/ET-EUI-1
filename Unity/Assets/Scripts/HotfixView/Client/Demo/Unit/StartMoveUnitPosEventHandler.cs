using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class StartMoveUnitPosEventHandler : AEvent<Scene, StartMoveUnitPos>
    {
        protected override async ETTask Run(Scene scene, StartMoveUnitPos a)
        {
            Unit unit = scene.GetComponent<UnitComponent>().MyUnit;

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            await ETTask.CompletedTask;
        }
    }
}