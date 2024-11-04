namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    [FriendOfAttribute(typeof (ET.Unit))]
    public class C2M_AddExpCountRequestHandler: MessageLocationHandler<Unit, C2M_AddExpCountRequest, M2C_AddExpCountResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_AddExpCountRequest request, M2C_AddExpCountResponse response)
        {
            bool checkSign = SignHelper.CheckSign(request);

            if (!checkSign)
            {
                response.Error = ErrorCode.Sign_Error;

                return;
            }

            int expCount = request.ExpCount;

            unit.CurrentExp += expCount;

            while (true)
            {
                UnitUpLevelExpConfig levelExpConfig = UnitUpLevelExpConfigCategory.Instance.GetByLevel(unit.Level + 1);

                int needExp = levelExpConfig.NeedExp;

                if (unit.CurrentExp > needExp)
                {
                    unit.CurrentExp -= needExp;

                    unit.Level++;
                }
                else
                {
                    break;
                }
            }

            response.UnitLevel = unit.Level;

            response.Exp = unit.CurrentExp;

            await ETTask.CompletedTask;
        }
    }
}