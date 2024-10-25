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

            HeroCard heroCard = fightManagerComponent.GetChild<HeroCard>(cardId);

            heroCard.Dispose();

            await ETTask.CompletedTask;
        }
    }
}