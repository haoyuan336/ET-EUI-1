using UnityEngine;

namespace ET.Client
{
    public struct PlayAddWood
    {
        public Entity Tree;

        public int Count;
    }

    [Event(SceneType.Demo)]
    public class PlayAddWoodEventHandler : AEvent<Scene, PlayAddWood>
    {
        protected override async ETTask Run(Scene scene, PlayAddWood a)
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

            fguiFightTextLayerComponent.PlayAddMeatText(startPos, a.Count.ToString());

            await ETTask.CompletedTask;
        }
    }
}