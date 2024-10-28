using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class SceneChangeFinishEventHandler : AEvent<Scene, SceneChangeFinish>
    {
        protected override async ETTask Run(Scene scene, SceneChangeFinish a)
        {
            Log.Debug($"scene change finish {scene.SceneType}");
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();

            Unit myUnit = unitComponent.MyUnit;

            MapConfig mapConfig = MapConfigCategory.Instance.GetMainCity();

            await SceneManager.LoadSceneAsync(mapConfig.SceneName);

            EventSystem.Instance.Publish(scene.Root(), new AfterUnitCreate() { Unit = myUnit });

            EventSystem.Instance.Publish(scene.Root(), new ShowLayerById() { WindowID = WindowID.MainLayer });
            
            EventSystem.Instance.Publish(scene.Root(), new ShowLayerById(){WindowID = WindowID.FightTextLayer});

            GameObjectComponent gameObjectComponent = myUnit.GetComponent<GameObjectComponent>();

            gameObjectComponent.BindCM();

            EventSystem.Instance.Publish(scene.Root(), new StartFight() { Unit = myUnit });

            EventSystem.Instance.Publish(scene.Root(), new InitMapManager() { Unit = myUnit });

            EventSystem.Instance.Publish(scene.Root(), new ShowLoadingLayer() { IsShow = false });

            scene.Root().AddComponent<CameraComponent>();
        }
    }
}