namespace ET.Client
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2NetClient_LoginGateHandler : MessageHandler<Scene, Main2NetClient_LoginGate, NetClient2Main_LoginGate>
    {
        protected override async ETTask Run(Scene scene, Main2NetClient_LoginGate request, NetClient2Main_LoginGate response)
        {
            long key = request.Key;

            long gateId = request.GateId;

            string address = request.Address;

            string account = request.Account;

            string password = request.Password;

            int zoneConfigId = request.ZoneConfigId;

            NetComponent netComponent = scene.GetComponent<NetComponent>();
            // 创建一个gate Session,并且保存到SessionComponent中
            Session gateSession = await netComponent.CreateRouterSession(NetworkHelper.ToIPEndPoint(address), account, password);

            gateSession.AddComponent<ClientSessionErrorComponent>();

            scene.Root().AddComponent<SessionComponent>().Session = gateSession;

            C2G_LoginGate c2GLoginGate = C2G_LoginGate.Create();

            c2GLoginGate.Key = key;

            c2GLoginGate.GateId = gateId;

            c2GLoginGate.ZoneConfigId = zoneConfigId;

            G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(c2GLoginGate);

            response.PlayerId = g2CLoginGate.PlayerId;

            await ETTask.CompletedTask;
        }
    }
}