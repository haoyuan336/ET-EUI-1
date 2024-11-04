using CommandLine;

namespace ET.Client
{
    public static partial class UnitHelper
    {
#if UNITY

        public static Unit GetMyUnitFromClientScene(Scene root)
        {
            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();
            Scene currentScene = root.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static Unit GetMyUnit(Scene root)
        {
            return root.CurrentScene().GetComponent<UnitComponent>().MyUnit;
        }

        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            PlayerComponent playerComponent = currentScene.Root().GetComponent<PlayerComponent>();
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static async ETTask<int> AddExp(Scene root, int exp)
        {
            C2M_AddExpCountRequest request = C2M_AddExpCountRequest.Create();

            request.ExpCount = exp;

            request.Sign = SignHelper.GetSign(request, TimeInfo.Instance.ServerNow());

            M2C_AddExpCountResponse response = await root.GetComponent<ClientSenderComponent>().Call(request) as M2C_AddExpCountResponse;

            if (response.Error == ErrorCode.ERR_Success)
            {
                Unit unit = UnitHelper.GetMyUnit(root);

                unit.Level = response.UnitLevel;

                unit.CurrentExp = response.Exp;

                EventSystem.Instance.Publish(root, new UpdateUnitInfoUI()
                {
                    Unit = unit
                });
            }

            return 0;
        }
#endif
    }
}