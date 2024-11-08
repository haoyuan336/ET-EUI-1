using UnityEngine;

namespace ET.Client
{
    public struct PlayCutWoodDamage
    {
        public Entity Tree;

        public int Damage;
    }

    [Event(SceneType.Demo)]
    public class PlayCutWoodDamageEventHandler : AEvent<Scene, PlayCutWoodDamage>
    {
        protected override async ETTask Run(Scene scene, PlayCutWoodDamage a)
        {
            GameObject gameObject = null;

            if (a.Tree is Tree)
            {
                Tree tree = a.Tree as Tree;

                gameObject = tree.TreeObject;
            }

            if (gameObject == null)
            {
                return;
            }

            Vector3 startPos = gameObject.transform.position;

            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIFightTextLayerComponent fguiFightTextLayerComponent = uiComponent.GetDlgLogic<FGUIFightTextLayerComponent>();

            fguiFightTextLayerComponent.PlayAddMeatText(startPos, a.Damage.ToString());

            await ETTask.CompletedTask;
        }
    }
}