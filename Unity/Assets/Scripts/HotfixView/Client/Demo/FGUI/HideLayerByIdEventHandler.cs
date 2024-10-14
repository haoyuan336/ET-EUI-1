namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class HideLayerByIdEventHandler : AEvent<Scene, HideLayerById>
    {
        protected override async ETTask Run(Scene scene, HideLayerById a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            uiComponent.HideWindow(a.WindowID);

            await ETTask.CompletedTask;
        }
    }
}