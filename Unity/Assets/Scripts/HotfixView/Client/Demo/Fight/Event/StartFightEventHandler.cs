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
            TroopComponent troopComponent = unit.GetComponent<TroopComponent>();

            Troop troop = troopComponent.Children.Values.ToList()[0] as Troop;

            for (int i = 0; i < troop.HeroCardIds.Length; i++)
            {
                long cardId = troop.HeroCardIds[i];

                EventSystem.Instance.Publish(scene, new CreateFightHero() { Unit = unit, HeroCardId = cardId, Index = i });
            }

            await ETTask.CompletedTask;
        }
    }
}