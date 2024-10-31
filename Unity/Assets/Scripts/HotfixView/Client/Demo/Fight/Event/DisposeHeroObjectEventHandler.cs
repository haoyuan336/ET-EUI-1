namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class DisposeHeroObjectEventHandler : AEvent<Scene, DisposeHeroObject>
    {
        protected override async ETTask Run(Scene scene, DisposeHeroObject a)
        {
            Unit unit = a.Unit;

            long cardId = a.CardId;

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            if (fightManagerComponent == null)
            {
                return;
            }

            HeroCard heroCard = fightManagerComponent.GetChild<HeroCard>(cardId);

            if (heroCard != null)
            {
                heroCard.Dispose();
            }

            await ETTask.CompletedTask;
        }
    }
}