namespace ET.Client
{
    public struct UpdateUnitInfoUI
    {
        public Unit Unit;
    }

    [Event(SceneType.Demo)]
    public class UpdateUnitInfoUIEventHandler : AEvent<Scene, UpdateUnitInfoUI>
    {
        protected override async ETTask Run(Scene scene, UpdateUnitInfoUI a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIHeadItemBarLayerComponent itemBarLayerComponent = uiComponent.GetDlgLogic<FGUIHeadItemBarLayerComponent>();

            itemBarLayerComponent.SetUnitInfo(a.Unit);

            await ETTask.CompletedTask;
        }
    }
}