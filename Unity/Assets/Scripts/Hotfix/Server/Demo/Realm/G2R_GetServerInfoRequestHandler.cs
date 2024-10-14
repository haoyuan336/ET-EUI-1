using System.Collections.Generic;

namespace ET.Server
{
    [FriendOfAttribute(typeof (ET.Server.Role))]
    [FriendOfAttribute(typeof (ET.Server.Server))]
    [MessageHandler(SceneType.Realm)]
    public class G2R_GetServerInfoRequestHandler: MessageHandler<Scene, G2R_GetServerInfoRequest, R2G_GetServerInfoResponse>
    {
        protected override async ETTask Run(Scene unit, G2R_GetServerInfoRequest request, R2G_GetServerInfoResponse response)
        {
            string userName = request.UserName;

            Log.Debug($"g2r_GetServerInfoRequest {userName}");

            AccountComponent accountComponent = unit.Root().GetComponent<AccountComponent>();

            Account account = accountComponent.Get(userName);

            if (account == null)
            {
                Log.Debug("account is null");
                
                response.Error = ErrorCode.Not_Found_Account;

                return;
            }

            RoleComponent roleComponent = account.GetComponent<RoleComponent>();

            List<RoleInfo> roleInfos = new List<RoleInfo>();
            
            foreach (var kv in roleComponent.Children)
            {
                roleInfos.Add(this.GetRoleInfo(kv.Value));
            }
            
            Log.Debug($"response roleinfos {response.RoleInfos.Count}");

            response.RoleInfos = roleInfos;

            response.ServerInfos = this.GetServerInfos(unit.Root());

            await ETTask.CompletedTask;
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
    }
}