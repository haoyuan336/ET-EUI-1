using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class CreateHeroObjectsEventHandler : AEvent<Scene, CreateHeroObjects>
    {
        protected override async ETTask Run(Scene scene, CreateHeroObjects a)
        {
            Unit unit = UnitHelper.GetMyUnit(scene);

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            List<Troop> troops = TroopHelper.GetTroops(scene);

            Troop troop = troops[0];

            for (int i = 0; i < troop.HeroCardIds.Length; i++)
            {
                long heroCardId = troop.HeroCardIds[i];
                
                HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(heroCardId);

                if (heroCard != null)
                {
                    EventSystem.Instance.Publish(scene, new CreateHeroObject() { HeroCard = heroCard, Unit = unit, Index = i });
                }
            }

            await ETTask.CompletedTask;
        }
    }
}