using UnityEngine;

namespace ET.Client
{
    public struct PlayAddMeat
    {
        public Entity BeEntity;

        public int Count;
    }

    [Event(SceneType.Demo)]
    public class PlayAddMeatEventHandler : AEvent<Scene, PlayAddMeat>
    {
        protected override async ETTask Run(Scene scene, PlayAddMeat a)
        {
            ObjectComponent objectComponent = a.BeEntity.GetComponent<ObjectComponent>();

            if (objectComponent == null || objectComponent.IsDisposed)
            {
                return;
            }

            Vector3 startPos = objectComponent.GameObject.transform.position;

            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIFightTextLayerComponent fguiFightTextLayerComponent = uiComponent.GetDlgLogic<FGUIFightTextLayerComponent>();

            fguiFightTextLayerComponent.PlayAddMeatText(startPos, a.Count.ToString());

            await ETTask.CompletedTask;
        }
    }
}