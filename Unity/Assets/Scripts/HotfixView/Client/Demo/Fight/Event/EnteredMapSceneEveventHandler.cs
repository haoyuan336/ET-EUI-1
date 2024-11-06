namespace ET.Client
{
    public struct EnteredMapScene
    {
        public int MapConfigId;
    }

    [Event(SceneType.Demo)]
    public class EnteredMapSceneEveventHandler : AEvent<Scene, EnteredMapScene>
    {
        protected override async ETTask Run(Scene scene, EnteredMapScene a)
        {
            Log.Debug($"enter map scene {a.MapConfigId}");

            Unit unit = UnitHelper.GetMyUnit(scene);

            unit.CurrentMapConfigId = a.MapConfigId;

            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIMainLayerComponent fguiMainLayerComponent = uiComponent.GetDlgLogic<FGUIMainLayerComponent>();

            if (fguiMainLayerComponent != null)
            {
                fguiMainLayerComponent.ChangedMap();
            }

            FGUIFightTextLayerComponent fightTextLayerComponent = uiComponent.GetDlgLogic<FGUIFightTextLayerComponent>();

            fightTextLayerComponent.ShowMapName(a.MapConfigId);

            await ETTask.CompletedTask;
        }
    }
}