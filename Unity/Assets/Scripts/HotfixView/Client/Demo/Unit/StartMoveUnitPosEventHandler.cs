using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class StartMoveUnitPosEventHandler : AEvent<Scene, StartMoveUnitPos>
    {
        protected override async ETTask Run(Scene scene, StartMoveUnitPos a)
        {
            Unit unit = scene.GetComponent<UnitComponent>().MyUnit;

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            gameObjectComponent.StartMove();

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            foreach (var card in heroCardComponent.FormationHeroCards)
            {
                HeroCardObjectComponent heroCardObjectComponent = card.GetComponent<HeroCardObjectComponent>();

                heroCardObjectComponent.StartMove();
            }

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            globalComponent.ArrowGameObject.SetActive(true);

            await ETTask.CompletedTask;
        }
    }
}