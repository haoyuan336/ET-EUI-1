namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_CreateHeroCardByConfigIdHandler: MessageLocationHandler<Unit, C2M_CreateOneHeroByConfigId, M2C_CreateOneHeroByConfigId>
    {
        protected override async ETTask Run(Unit unit, C2M_CreateOneHeroByConfigId request, M2C_CreateOneHeroByConfigId response)
        {
            int configId = request.ConfigId;

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            HeroCard heroCard = heroCardComponent.AddChild<HeroCard, int>(configId);

            response.HeroCardInfo = heroCard.GetInfo();

            response.Error = ErrorCode.ERR_Success;

            await ETTask.CompletedTask;
        }
    }
}