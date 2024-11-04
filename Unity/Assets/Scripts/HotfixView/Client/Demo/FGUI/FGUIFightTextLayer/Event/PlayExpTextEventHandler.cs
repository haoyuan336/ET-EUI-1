using UnityEngine;

namespace ET.Client
{
    public struct PlayAddExpText
    {
        public Entity BeAttackEntity;

        public int Exp;
    }

    [Event(SceneType.Demo)]
    public class PlayExpTextEventHandler : AEvent<Scene, PlayAddExpText>
    {
        protected override async ETTask Run(Scene scene, PlayAddExpText a)
        {
            Log.Debug("play add exp text");
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIFightTextLayerComponent fightTextLayerComponent = uiComponent.GetDlgLogic<FGUIFightTextLayerComponent>();

            Entity beAttackEntity = a.BeAttackEntity;

            ObjectComponent objectComponent = beAttackEntity.GetComponent<ObjectComponent>();

            if (objectComponent == null || objectComponent.IsDisposed)
            {
                Log.Debug("object component is null");
                return;
            }

            if (objectComponent.GameObject == null)
            {
                return;
            }

            Vector3 position = objectComponent.GameObject.transform.position;

            fightTextLayerComponent.PlayAddExpText(position, a.Exp.ToString());

            await ETTask.CompletedTask;
        }
    }
}