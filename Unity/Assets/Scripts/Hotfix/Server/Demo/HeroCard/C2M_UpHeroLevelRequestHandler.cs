namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    [FriendOfAttribute(typeof (ET.HeroCard))]
    public class C2M_UpHeroLevelRequestHandler: MessageLocationHandler<Unit, C2M_UpHeroLevelRequest, M2C_UpHeroLevelResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpHeroLevelRequest request, M2C_UpHeroLevelResponse response)
        {
            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(request.HeroId);

            if (heroCard == null)
            {
                response.Error = ErrorCode.Not_Found_HeroCard;

                return;
            }

            heroCard.Level++;

            heroCard.UpdateValueData();

            response.HeroCardInfo = heroCard.GetInfo();

            await ETTask.CompletedTask;
        }
    }
}