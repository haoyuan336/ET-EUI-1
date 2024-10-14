namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class ShowLoadingLayerEventHandler : AEvent<Scene, ShowLoadingLayer>
    {
        protected override async ETTask Run(Scene scene, ShowLoadingLayer a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            bool isShow = a.IsShow;

            if (isShow)
            {
                uiComponent.ShowWindow(WindowID.LoadingLayer);
            }
            else
            {
                uiComponent.HideWindow(WindowID.LoadingLayer);
            }
        }
    }
}