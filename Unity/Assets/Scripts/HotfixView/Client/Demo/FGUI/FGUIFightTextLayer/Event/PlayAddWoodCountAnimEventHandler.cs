using UnityEngine;

namespace ET.Client
{
    public struct PlayAddWoodCountAnim
    {
        public Tree Tree;
    }

    [Event(SceneType.Demo)]
    public class PlayAddWoodCountAnimEventHandler : AEvent<Scene, PlayAddWoodCountAnim>
    {
        protected override async ETTask Run(Scene scene, PlayAddWoodCountAnim a)
        {
            if (a.Tree.IsAwardItem)
            {
                return;
            }

            a.Tree.IsAwardItem = true;

            int count = a.Tree.TreeConfig.AwardCount;

            this.PlayTextAnim(a.Tree, count);

            TreeConfig treeConfig = a.Tree.TreeConfig;

            await ItemHelper.AddItemCount(scene, treeConfig.AwardConfigId, treeConfig.AwardCount);
        }

        private void PlayTextAnim(Tree tree, int count)
        {
            if (tree.TreeObject == null)
            {
                return;
            }

            GameObject gameObject = tree.TreeObject;

            Vector3 startPos = gameObject.transform.position;

            UIComponent uiComponent = tree.Root().GetComponent<UIComponent>();

            FGUIFightTextLayerComponent fguiFightTextLayerComponent = uiComponent.GetDlgLogic<FGUIFightTextLayerComponent>();

            fguiFightTextLayerComponent.PlayAddMeatText(startPos, count.ToString());
        }
    }
}