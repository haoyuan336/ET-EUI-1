using UnityEditor.Searcher;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class PopLayerEventHandler : AEvent<Scene, PopLayer>
    {
        protected override async ETTask Run(Scene scene, PopLayer a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            uiComponent.PopStackWindow();

            await ETTask.CompletedTask;
        }
    }
}