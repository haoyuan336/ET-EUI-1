using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class MoveUnitPosEventHandler : AEvent<Scene, MoveUnitPos>
    {
        protected override async ETTask Run(Scene scene, MoveUnitPos a)
        {
            Vector2 direction = a.Vector2;

            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();

            Unit unit = unitComponent.MyUnit;

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            Vector3 targetPos = gameObjectComponent.GameObject.transform.position + new Vector3(direction.x, 0, -direction.y) * 4;

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            globalComponent.ArrowGameObject.transform.position = targetPos;

            gameObjectComponent.Move(targetPos);

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            foreach (var heroCard in heroCardComponent.FormationHeroCards)
            {

                if (heroCard.IsDisposed)
                {
                    continue;
                }
                HeroCardObjectComponent heroCardObjectComponent = heroCard.GetComponent<HeroCardObjectComponent>();

                heroCardObjectComponent.Move(targetPos);
            }

            await ETTask.CompletedTask;
        }
    }
}