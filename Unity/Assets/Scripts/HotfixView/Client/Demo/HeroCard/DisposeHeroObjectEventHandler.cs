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

            MoveObjectComponent moveObjectComponent = heroCard.GetComponent<MoveObjectComponent>();

            if (moveObjectComponent != null)
            {
                moveObjectComponent.Dispose();
            }

            await ETTask.CompletedTask;
        }
    }
}