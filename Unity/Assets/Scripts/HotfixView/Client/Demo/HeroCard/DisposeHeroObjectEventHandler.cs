namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class DisposeHeroObjectEventHandler : AEvent<Scene, DisposeHeroObject>
    {
        protected override async ETTask Run(Scene scene, DisposeHeroObject a)
        {
            HeroCard heroCard = a.HeroCard;

            if (heroCard == null)
            {
                return;
            }

            HeroCardObjectComponent heroCardObjectComponent = heroCard.GetComponent<HeroCardObjectComponent>();

            if (heroCardObjectComponent != null)
            {
                heroCardObjectComponent.Dispose();
            }

            await ETTask.CompletedTask;
        }
    }
}