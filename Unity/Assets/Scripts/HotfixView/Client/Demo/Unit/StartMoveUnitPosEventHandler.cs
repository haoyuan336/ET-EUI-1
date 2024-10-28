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
                    AIComponent aiComponent = heroCard.GetComponent<AIComponent>();

                    if (aiComponent.GetCurrentState() == AIState.Death)
                    {
                        continue;
                    }
                    aiComponent.EnterAIState(AIState.Moveing);

                    AnimComponent animComponent = heroCard.GetComponent<AnimComponent>();

                    animComponent.PlayAnim("move", true).Coroutine();
                }
            }

            await ETTask.CompletedTask;
        }
    }
}