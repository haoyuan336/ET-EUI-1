using UnityEngine;
using YooAsset;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            // // Unit View层
            string assetsName = $"Player1";

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

            GameObject prefab = await scene.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            // GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");
            // //
            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();
            
            GameObject go = UnityEngine.Object.Instantiate(prefab, globalComponent.Unit, true);
            
            go.transform.position = unit.Position;
            
            unit.AddComponent<GameObjectComponent>().GameObject = go;
            // unit.AddComponent<AnimatorComponent>();
            // await ETTask.CompletedTask;
        }
    }
}