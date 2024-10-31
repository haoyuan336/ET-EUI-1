namespace ET.Client
{
    public struct UpdateItemCount
    {
        public string Key;

        public int Count;
    }

    [Event(SceneType.Demo)]
    public class UpdateItemCountEventHandler : AEvent<Scene, UpdateItemCount>
    {
        protected override async ETTask Run(Scene scene, UpdateItemCount a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIHeadItemBarLayerComponent fguiHeadItemBarLayerComponent = uiComponent.GetDlgLogic<FGUIHeadItemBarLayerComponent>();

            if (fguiHeadItemBarLayerComponent != null)
            {
                fguiHeadItemBarLayerComponent.UpdateItemCount(a.Key, a.Count);
            }

            await ETTask.CompletedTask;
        }
    }
}