using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class EndMoveUnitPosEventHandler : AEvent<Scene, EndMoveUnitPos>
    {
        protected override async ETTask Run(Scene scene, EndMoveUnitPos a)
        {
            Unit unit = scene.GetComponent<UnitComponent>().MyUnit;

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            gameObjectComponent.EndMove();

            // FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            List<HeroCard> remnoveCard = new List<HeroCard>();

            foreach (var heroCard in gameObjectComponent.HeroCards)
            {
                if (heroCard != null && !heroCard.IsDisposed)
                {
                    AIComponent aiComponent = heroCard.GetComponent<AIComponent>();

                    if (aiComponent != null && !aiComponent.IsDisposed)
                    {
                        if (aiComponent.GetCurrentState() == AIState.Death)
                        {
                            continue;
                        }

                        // aiComponent.EnterAIState(AIState.Patrol);

                        aiComponent.PopAIState();

                        AnimComponent animComponent = heroCard.GetComponent<AnimComponent>();

                        animComponent.PlayAnim("idle", true).Coroutine();
                    }
                }
                else
                {
                    remnoveCard.Add(heroCard);
                }
            }

            foreach (var card in remnoveCard)
            {
                gameObjectComponent.HeroCards.Remove(card);
            }

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            globalComponent.ArrowGameObject.SetActive(false);

            await ETTask.CompletedTask;
        }
    }
}