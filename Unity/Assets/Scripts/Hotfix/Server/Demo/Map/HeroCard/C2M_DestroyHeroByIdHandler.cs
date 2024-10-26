namespace ET.Server;

[MessageLocationHandler(SceneType.Map)]
public class C2M_DestroyHeroByIdHandler: MessageLocationHandler<Unit, C2M_DestroyHeroByIdRequest, M2C_DestroyHeroByIdResponse>
{
    protected override async ETTask Run(Unit unit, C2M_DestroyHeroByIdRequest request, M2C_DestroyHeroByIdResponse response)
    {
        HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

        long cardId = request.HeroId;

        HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(cardId);

        if (heroCard != null)
        {
            heroCard.Dispose();
        }

        response.Error = ErrorCode.ERR_Success;

        await ETTask.CompletedTask;
    }
}