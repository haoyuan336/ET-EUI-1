namespace ET.Client
{
    public struct ChangeMapSceneFinish
    {
        public string SceneName;
    }

    [Event(SceneType.Demo)]
    public class ChangeMapSceneFinishEventHandler : AEvent<Scene, ChangeMapSceneFinish>
    {
        protected override async ETTask Run(Scene scene, ChangeMapSceneFinish a)
        {
            Log.Debug($"Change map scene finish {a.SceneName}");

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