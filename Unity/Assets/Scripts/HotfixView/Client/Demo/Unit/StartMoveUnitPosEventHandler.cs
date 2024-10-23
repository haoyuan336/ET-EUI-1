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

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            globalComponent.ArrowGameObject.SetActive(true);

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            foreach (var heroCard in fightManagerComponent.HeroCards)
            {
                if (heroCard != null && !heroCard.IsDisposed)
                {
                    MoveObjectComponent moveObjectComponent = heroCard.GetComponent<MoveObjectComponent>();

                    if (moveObjectComponent != null && !moveObjectComponent.IsDisposed)
                    {
                        moveObjectComponent.StartMove();
                    }
                }
            }
            
            await ETTask.CompletedTask;
        }
    }
}