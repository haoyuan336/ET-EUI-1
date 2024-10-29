using System;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class CreateFightHeroEventHandler : AEvent<Scene, CreateFightHero>
    {
        protected override async ETTask Run(Scene scene, CreateFightHero a)
        {
            long heroCardId = a.HeroCardId;

            Unit unit = a.Unit;

            int index = a.Index;

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardId);

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            if (fightManagerComponent == null)
            {
                fightManagerComponent = unit.AddComponent<FightManagerComponent>();
            }

            HeroCard fightHeroCard = fightManagerComponent.AddChildWithId<HeroCard>(heroCard.Id);

            fightHeroCard.SetInfo(heroCard.GetInfo());

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            GameObject unitObject = gameObjectComponent.GameObject;

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(heroCard.Config.PrefabName);

            Vector3 pos = unitObject.transform.position + Quaternion.Euler(0, index * RandomGenerator.RandomNumber(30, 50), 0) * Vector3.forward *
                    (RandomGenerator.RandFloat01() * 2 + 1);

            fightHeroCard.AddComponent<ObjectComponent, GameObject, Vector3>(prefab, pos);

            fightHeroCard.AddComponent<FightDataComponent, HeroCard>(fightHeroCard);

            AIComponent aiComponent = fightHeroCard.AddComponent<AIComponent>();

            fightHeroCard.AddComponent<AttackComponent>();

            fightHeroCard.AddComponent<TrackComponent>();

            int maskCode = LayerMask.GetMask("Enemy");

            fightHeroCard.AddComponent<FindEnemyComponent, int>(maskCode);

            fightHeroCard.AddComponent<AnimComponent>();

            fightHeroCard.AddComponent<MoveObjectComponent>();

            fightHeroCard.AddComponent<SkillComponent, int, int>(fightHeroCard.HeroConfigId, fightHeroCard.Level);

            gameObjectComponent.HeroCards.Add(fightHeroCard);

            aiComponent.EnterAIState(AIState.Patrol);

            await ETTask.CompletedTask;
        }
    }
}