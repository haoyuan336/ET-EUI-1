using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class AfterUnitCreate_CreateUnitView : AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            Unit unit = args.Unit;

            unit.AddComponent<GameObjectComponent>();

            await ETTask.CompletedTask;
        }
    }
}