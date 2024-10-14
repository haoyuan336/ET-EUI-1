using System.Collections.Generic;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOfAttribute(typeof (Account))]
    [FriendOfAttribute(typeof (Role))]
    [FriendOfAttribute(typeof (ET.Server.Server))]
    public class C2R_LoginHandler: MessageSessionHandler<C2R_Login, R2C_Login>
    {
        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
        {
            //从数据库取出来数据

            DBManagerComponent dbManagerComponent = session.Root().GetComponent<DBManagerComponent>();

            DBComponent dbComponent = dbManagerComponent.GetZoneDB(session.Zone());

            AccountComponent accountComponent = session.Root().GetComponent<AccountComponent>();

            Account account = accountComponent.Get(request.Account);

            if (account == null)
            {
                Log.Debug("account is null ");

                List<Account> accounts = await dbComponent.Query<Account>(a => a.UserName.Equals(request.Account));

                Log.Debug($"account count {accounts.Count}");

                if (accounts.Count == 0)
                {
                    //数据库里面没有此玩家，注册一下先

                    account = accountComponent.AddChild<Account>();

                    account.UserName = request.Account;

                    account.Passwd = request.Password;

                    account.RegisterTime = TimeInfo.Instance.ServerNow();

                    account.AddComponent<RoleComponent>();
                }
                else
                {
                    account = accounts[0];

                    accountComponent.AddChild(account);

                    Log.Debug($"component {account.Components.Count}");

                    foreach (var kv in account.Components)
                    {
                        Log.Debug($"component {kv.Key} {kv.Value.GetType()}");
                    }
                }

                accountComponent.Add(account.UserName, account);
            }

            Log.Debug($"C2R_Login {request.Account} ,{request.Password}");

            // 随机分配一个Gate
            StartSceneConfig config = RealmGateAddressHelper.GetGate(session.Zone(), request.Account);
            Log.Debug($"gate address: {config}");

            // 向gate请求一个key,客户端可以拿着这个key连接gate
            R2G_GetLoginKey r2GGetLoginKey = R2G_GetLoginKey.Create();
            r2GGetLoginKey.Account = request.Account;
            G2R_GetLoginKey g2RGetLoginKey =
                    (G2R_GetLoginKey)await session.Fiber().Root.GetComponent<MessageSender>().Call(config.ActorId, r2GGetLoginKey);

            response.Address = config.InnerIPPort.ToString();
            response.Key = g2RGetLoginKey.Key;
            response.GateId = g2RGetLoginKey.GateId;

            List<ServerInfo> serverInfos = this.GetServerInfos(session.Root());

            response.ServerInfos = serverInfos;

            RoleComponent roleComponent = account.GetComponent<RoleComponent>();

            List<RoleInfo> roleInfos = new List<RoleInfo>();

            foreach (var kv in roleComponent.Children)
            {
                roleInfos.Add(this.GetRoleInfo(kv.Value));
            }

            response.RoleInfos = roleInfos;

            CloseSession(session).Coroutine();
        }

        private async ETTask CloseSession(Session session)
        {
            await session.Root().GetComponent<TimerComponent>().WaitAsync(1000);
            session.Dispose();
        }

        private RoleInfo GetRoleInfo(Entity entity)
        {
            Role role = entity as Role;

            RoleInfo roleInfo = RoleInfo.Create();

            roleInfo.RoleId = role.Id;

            roleInfo.RoleName = role.RoleName;

            roleInfo.HeroConfigId = role.HeroConfigId;

            roleInfo.ZoneConfigId = role.ZoneConfigId;

            return roleInfo;
        }

        private List<ServerInfo> GetServerInfos(Scene scene)
        {
            List<ServerInfo> serverInfos = new List<ServerInfo>();

            ServerManagerComponent serverManagerComponent = scene.GetComponent<ServerManagerComponent>();

            Log.Debug($"server manager component {serverManagerComponent.Children.Count}");

            foreach (var kv in serverManagerComponent.Children)
            {
                ServerInfo serverInfo = this.GetServerInfo(kv.Value);

                serverInfos.Add(serverInfo);
            }

            return serverInfos;
        }

        private ServerInfo GetServerInfo(Entity entity)
        {
            Server server = entity as Server;

            ServerInfo serverInfo = ServerInfo.Create();

            serverInfo.ZoneConfigId = server.ZoneConfigId;

            serverInfo.ServerName = server.ServerName;

            serverInfo.ServerState = server.ServerState;

            serverInfo.StartTime = server.StartTimeStamp;

            return serverInfo;
        }
    }
}