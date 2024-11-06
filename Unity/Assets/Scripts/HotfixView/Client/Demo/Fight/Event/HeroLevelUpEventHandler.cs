using UnityEditor.UI;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class HeroLevelUpEventHandler : AEvent<Scene, HeroLevelUp>
    {
        protected override async ETTask Run(Scene scene, HeroLevelUp a)
        {
            HeroCard heroCard = a.HeroCard;

            Unit unit = UnitHelper.GetMyUnit(scene);

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            HeroCard fightCard = fightManagerComponent.GetChild<HeroCard>(heroCard.Id);

            fightCard.SetInfo(heroCard.GetInfo());

            FightDataComponent fightDataComponent = fightCard.GetComponent<FightDataComponent>();

            fightDataComponent.Datas = fightCard.Datas;

            SkillComponent skillComponent = fightCard.GetComponent<SkillComponent>();

            skillComponent.UpdateLevel(heroCard.Level);

            await ETTask.CompletedTask;
        }
    }
}