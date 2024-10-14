namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class CloseLayerByIdEventHandler: AEvent<Scene, CloseLayerById>
    {
        protected override async ETTask Run(Scene scene, CloseLayerById a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();
            
            uiComponent.CloseWindow(a.WindowID);
            
            await ETTask.CompletedTask;
        }
    }
}