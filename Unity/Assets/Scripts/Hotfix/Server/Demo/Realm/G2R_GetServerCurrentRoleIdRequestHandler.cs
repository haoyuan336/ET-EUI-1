namespace ET.Server
{
    [MessageHandler(SceneType.Realm)]
    [FriendOfAttribute(typeof (ET.Server.Role))]
    [FriendOfAttribute(typeof (ET.Server.Account))]
    public class G2R_GetServerCurrentRoleIdRequestHandler: MessageHandler<Scene, G2R_GetServerCurrentRoleIdRequest,
        R2G_GetServerCurrentRoleIdResponse>
    {
        protected override async ETTask Run(Scene unit, G2R_GetServerCurrentRoleIdRequest request, R2G_GetServerCurrentRoleIdResponse response)
        {
            Scene root = unit.Root();

            int zoneConfigId = request.ZoneConfigId;

            AccountComponent accountComponent = root.GetComponent<AccountComponent>();

            Account account = accountComponent.Get(request.Account);

            RoleComponent roleComponent = account.GetComponent<RoleComponent>();

            Role role = null;

            foreach (var kv in roleComponent.Children)
            {
                Role target = kv.Value as Role;

                if (target.ZoneConfigId == zoneConfigId)
                {
                    role = target;

                    break;
                }
            }

            if (role == null)
            {
                role = roleComponent.AddChild<Role>();

                role.ZoneConfigId = zoneConfigId;
            }

            role.LastLoginTime = TimeInfo.Instance.ServerNow();

            response.RoleId = role.Id;
            
            this.SaveAccount(account).Coroutine();

            await ETTask.CompletedTask;
        }

        private async ETTask SaveAccount(Account account)
        {
            DBManagerComponent dbManagerComponent = account.Root().GetComponent<DBManagerComponent>();

            await dbManagerComponent.GetZoneDB(account.Zone()).Save(account);

            AccountComponent accountComponent = account.GetParent<AccountComponent>();

            accountComponent.Remove(account.UserName);

            account.Dispose();
        }
    }
}