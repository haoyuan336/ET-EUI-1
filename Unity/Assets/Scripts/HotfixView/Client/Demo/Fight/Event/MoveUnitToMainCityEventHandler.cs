using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class MoveUnitToMainCityEventHandler : AEvent<Scene, MoveUnitToMainCity>
    {
        protected override async ETTask Run(Scene scene, MoveUnitToMainCity a)
        {
            Log.Debug("move unit to main city ");

            EventSystem.Instance.Publish(scene, new ShowLayerById() { WindowID = WindowID.MoveingLayer });

            Unit unit = UnitHelper.GetMyUnit(scene);

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            List<Troop> troops = TroopHelper.GetTroops(scene);

            Troop troop = troops[0];

            TimerComponent timerComponent = scene.GetComponent<TimerComponent>();

            for (int i = 0; i < troop.HeroCardIds.Length; i++)
            {
                long cardId = troop.HeroCardIds[i];

                HeroCard heroCard = fightManagerComponent.GetChild<HeroCard>(cardId);

                if (heroCard == null || heroCard.IsDisposed)
                {
                    continue;
                }

                await timerComponent.WaitAsync(500);

                AnimComponent animComponent = heroCard.GetComponent<AnimComponent>();

                animComponent.PlayAnim("idle", true).Coroutine();
            }

            await gameObjectComponent.MoveUnitToMainCity();

            GameObject unitObject = gameObjectComponent.GameObject;

            //将英雄传送过来

            for (int i = 0; i < troop.HeroCardIds.Length; i++)
            {
                long cardId = troop.HeroCardIds[i];

                HeroCard heroCard = fightManagerComponent.GetChild<HeroCard>(cardId);

                if (heroCard == null || heroCard.IsDisposed)
                {
                    continue;
                }

                MoveObjectComponent moveObjectComponent = heroCard.GetComponent<MoveObjectComponent>();

                AIComponent aiComponent = heroCard.GetComponent<AIComponent>();

                Vector3 pos = unitObject.transform.position + Quaternion.Euler(0, i * RandomGenerator.RandomNumber(30, 50), 0) * Vector3.forward *
                        (RandomGenerator.RandFloat01() * 2 + 1);

                moveObjectComponent.SetPos(pos);

                aiComponent.EnterAIState(AIState.Patrol);
            }

            EventSystem.Instance.Publish(scene, new CloseLayerById() { WindowID = WindowID.MoveingLayer });

            await ETTask.CompletedTask;
        }
    }
}