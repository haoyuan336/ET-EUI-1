using System;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class CreateHeroObjectEventHandler : AEvent<Scene, CreateHeroObject>
    {
        protected override async ETTask Run(Scene scene, CreateHeroObject a)
        {
            HeroCard heroCard = a.HeroCard;

            Unit unit = a.Unit;

            if (heroCard == null)
            {
                return;
            }

            // heroCard.AddComponent<MoveObjectComponent, Unit, int>(unit, a.Index);

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            GameObject unitObject = gameObjectComponent.GameObject;

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(heroCard.Config.PrefabName);

            Vector3 pos = unitObject.transform.position + Quaternion.Euler(0, a.Index * RandomGenerator.RandomNumber(30, 50), 0) * Vector3.forward *
                    (RandomGenerator.RandFloat01() * 2 + 1);

            heroCard.AddComponent<ObjectComponent, GameObject, Vector3>(prefab, pos);

            heroCard.AddComponent<MoveObjectComponent>();

            await ETTask.CompletedTask;
        }
    }
}