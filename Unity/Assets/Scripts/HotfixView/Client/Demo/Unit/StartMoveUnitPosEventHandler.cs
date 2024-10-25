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

            foreach (var heroCard in gameObjectComponent.HeroCards)
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