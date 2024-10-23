namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class InitMapManagerEventHandler : AEvent<Scene, InitMapManager>
    {
        protected override async ETTask Run(Scene scene, InitMapManager a)
        {
            Unit unit = a.Unit;

            MapManagerComponent managerComponent = unit.AddComponent<MapManagerComponent>();

            await ETTask.CompletedTask;
        }
    }
}