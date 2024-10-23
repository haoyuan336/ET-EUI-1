using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class CreateHeroObjectsEventHandler : AEvent<Scene, CreateHeroObjects>
    {
        protected override async ETTask Run(Scene scene, CreateHeroObjects a)
        {
            Unit unit = a.Unit;

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            for (int i = 0; i < fightManagerComponent.HeroCards.Count; i++)
            {
                HeroCard heroCard = fightManagerComponent.HeroCards[i];

                if (heroCard != null)
                {
                    EventSystem.Instance.Publish(scene, new CreateHeroObject() { HeroCard = heroCard, Unit = unit, Index = i });
                }
            }

            EventSystem.Instance.Publish(scene, new DiffuseHero() { Unit = unit });

            await ETTask.CompletedTask;
        }
    }
}