using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class HeroLevelUpEventHandler1 : AEvent<Scene, HeroLevelUp>
    {
        protected override async ETTask Run(Scene scene, HeroLevelUp a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIHeroInfoLayerComponent fguiHeroInfoLayerComponent = uiComponent.GetDlgLogic<FGUIHeroInfoLayerComponent>();

            if (fguiHeroInfoLayerComponent != null)
            {
                fguiHeroInfoLayerComponent.SetHeroInfo(a.HeroCard);
            }

            await ETTask.CompletedTask;
        }
    }
}