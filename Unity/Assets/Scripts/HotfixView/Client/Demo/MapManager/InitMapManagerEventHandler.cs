namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class InitMapManagerEventHandler : AEvent<Scene, InitMapManager>
    {
        protected override async ETTask Run(Scene scene, InitMapManager a)
        {
            MapManagerComponent managerComponent = scene.AddComponent<MapManagerComponent>();

            await ETTask.CompletedTask;
        }
    }
}