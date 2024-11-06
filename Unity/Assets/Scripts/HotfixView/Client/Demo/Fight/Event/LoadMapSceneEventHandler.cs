using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    public struct LoadMapScene
    {
        public int MapConfigId;
    }

    [Event(SceneType.Demo)]
    public class LoadMapSceneEventHandler : AEvent<Scene, LoadMapScene>
    {
        protected override async ETTask Run(Scene scene, LoadMapScene a)
        {
            Log.Debug($"load map scene {a.MapConfigId}");

            MapManagerComponent managerComponent = scene.GetComponent<MapManagerComponent>();

            MapScene mapScene = managerComponent.GetChild<MapScene>(a.MapConfigId);

            await mapScene.LoadScene();
        }
    }
}