namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class PushLayerByIdEventHandler : AEvent<Scene, PushLayerById>
    {
        protected override async ETTask Run(Scene scene, PushLayerById a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            uiComponent.ShowStackWindowWithId(a.WindowID, a.ShowWindowData);

            await ETTask.CompletedTask;
        }
    }
}