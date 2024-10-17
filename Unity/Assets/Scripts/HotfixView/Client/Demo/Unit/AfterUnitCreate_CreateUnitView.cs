using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView : AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            // // Unit View层
            string assetsName = $"Cube";

            // AssetHandle handle = YooAssets.LoadAssetAsync<GameObject>(assetsName);
            //
            // handle.Completed += (result) =>
            // {
            //     Log.Debug($"result {result.LastError}");
            //     
            //     GameObject prefab = result.AssetObject as GameObject;
            //
            //     GameObject gameObject = GameObject.Instantiate(prefab);
            //
            //     gameObject.transform.position = unit.Position;
            //     
            //     unit.AddComponent<GameObjectComponent>().GameObject = gameObject;
            //
            //
            // };

            // GameObject prefab = Resources.Load("Player1") as GameObject;

            // GameObject prefab = await scene.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            // GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");
            // GameObject prefab = scene.Root().GetComponent<GlobalComponent>().Prefabs.PlayerPrefab;
            // //
            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>("Hero1");

            // GameObject prefab = globalComponent.Prefabs.PlayerPrefab;

            GameObject go = UnityEngine.Object.Instantiate(prefab, globalComponent.Unit, true);
            //
            // go.transform.position = unit.Position;
            //
            unit.AddComponent<GameObjectComponent, GameObject>(go);
            
            // unit.AddComponent<AnimatorComponent>();
            
            await ETTask.CompletedTask;
        }
    }
}