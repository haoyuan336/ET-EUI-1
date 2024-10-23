using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class UpdateTroopEventHandler : AEvent<Scene, UpdateTroop>
    {
        protected override async ETTask Run(Scene scene, UpdateTroop a)
        {
            Unit unit = a.Unit;

            Troop troop = a.Troop;

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            foreach (var cardId in troop.HeroCardIds)
            {
                if (cardId == 0)
                {
                    continue;
                }

                if (!fightManagerComponent.HeroCards.Exists(card => card.Id == cardId))
                {
                    HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(cardId);

                    fightManagerComponent.HeroCards.Add(heroCard);

                    EventSystem.Instance.Publish(scene.Root(), new CreateHeroObject() { Unit = unit, HeroCard = heroCard });
                }
            }

            List<long> cardsId = troop.HeroCardIds.ToList();

            List<HeroCard> removeList = new List<HeroCard>();

            foreach (var card in fightManagerComponent.HeroCards)
            {
                if (!cardsId.Contains(card.Id))
                {
                    removeList.Add(card);
                }
            }

            foreach (var card in removeList)
            {
                fightManagerComponent.HeroCards.Remove(card);

                EventSystem.Instance.Publish(scene.Root(), new DisposeHeroObject() { HeroCard = card });
            }

            await ETTask.CompletedTask;
        }
    }
}