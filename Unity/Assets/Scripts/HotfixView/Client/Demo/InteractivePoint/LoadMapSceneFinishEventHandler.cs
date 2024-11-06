namespace ET.Client
{
    public struct LoadMapSceneFinish
    {
        public int MapConfigId;
    }

    [Event(SceneType.Demo)]
    public class LoadMapSceneFinishEventHandler : AEvent<Scene, LoadMapSceneFinish>
    {
        protected override async ETTask Run(Scene scene, LoadMapSceneFinish a)
        {
            Log.Debug($"LoadMapSceneFinish scene finish {a.MapConfigId}");

            InteractivePointComponent interactivePointComponent = scene.GetComponent<InteractivePointComponent>();

            if (interactivePointComponent == null)
            {
                interactivePointComponent = scene.AddComponent<InteractivePointComponent>();
            }

            interactivePointComponent.ChangeMapScene();

            await ETTask.CompletedTask;
        }
    }
}