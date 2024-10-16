using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class EndMoveUnitPosEventHandler : AEvent<Scene, EndMoveUnitPos>
    {
        protected override async ETTask Run(Scene scene, EndMoveUnitPos a)
        {
            Unit unit = scene.GetComponent<UnitComponent>().MyUnit;

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            Vector3 unitPos = gameObjectComponent.GameObject.transform.position;
            
            
             

            await ETTask.CompletedTask;
        }
    }
}