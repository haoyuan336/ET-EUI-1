using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class StartFightEventHandler : AEvent<Scene, StartFight>
    {
        protected override async ETTask Run(Scene scene, StartFight a)
        {
            Log.Debug($"start fight {a.Unit.Id}");

            Unit unit = a.Unit;

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            if (fightManagerComponent == null)
            {
                fightManagerComponent = unit.AddComponent<FightManagerComponent>();
            }

            List<HeroCard> heroCards = new List<HeroCard>();

            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            Troop troop = troopComponent.Children.Values.ToList()[0] as Troop;

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            for (int i = 0; i < troop.HeroCardIds.Length; i++)
            {
                long cardId = troop.HeroCardIds[i];

                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(cardId);

                heroCards.Add(heroCard);
            }

            fightManagerComponent.HeroCards = heroCards;

            EventSystem.Instance.Publish(scene, new CreateHeroObjects() { Unit = unit });

            await ETTask.CompletedTask;
        }
    }
}