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

            gameObjectComponent.Move(direction, a.Power);

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            // TimerComponent timerComponent = scene.Root().GetComponent<TimerComponent>();

            // foreach (var heroCard in heroCardComponent.FormationHeroCards)
            // {
            //     await timerComponent.WaitAsync(200);
            //     
            //     EventSystem.Instance.Publish(scene, new MoveHeroCard() { HeroCard = heroCard, MoveSpeed = direction, Power = a.Power });
            // }
        }
    }
}