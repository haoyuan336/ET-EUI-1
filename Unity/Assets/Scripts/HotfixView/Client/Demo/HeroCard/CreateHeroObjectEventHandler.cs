namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class CreateHeroObjectEventHandler : AEvent<Scene, CreateHeroObject>
    {
        protected override async ETTask Run(Scene scene, CreateHeroObject a)
        {
            HeroCard heroCard = a.HeroCard;

            Unit unit = a.Unit;

            if (heroCard == null)
            {
                return;
            }

            HeroCardObjectComponent heroCardObjectComponent = heroCard.GetComponent<HeroCardObjectComponent>();

            if (heroCardObjectComponent != null)
            {
                heroCardObjectComponent.Dispose();
            }

            heroCard.AddComponent<HeroCardObjectComponent, Unit>(unit);

            await ETTask.CompletedTask;
        }
    }
}