namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class ShowLayerByIdEventHandler : AEvent<Scene, ShowLayerById>
    {
        protected override async ETTask Run(Scene scene, ShowLayerById a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            WindowID windowID = a.WindowID;

            uiComponent.ShowWindow(windowID);

            await ETTask.CompletedTask;
        }
    }
}