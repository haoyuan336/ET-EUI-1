namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class PushLayerByIdEventHandler : AEvent<Scene, PushLayerById>
    {
        protected override async ETTask Run(Scene scene, PushLayerById a)
        {
            Log.Debug($"push layer by id {a.WindowID}");
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            uiComponent.ShowStackWindowWithId(a.WindowID, a.ShowWindowData);

            await ETTask.CompletedTask;
        }
    }
}