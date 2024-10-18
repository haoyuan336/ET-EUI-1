using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView : AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            Unit unit = args.Unit;
          
            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>("Hero1");

            GameObject go = UnityEngine.Object.Instantiate(prefab, globalComponent.Unit, true);

            unit.AddComponent<GameObjectComponent, GameObject>(go);
            
            await ETTask.CompletedTask;
        }
    }
}