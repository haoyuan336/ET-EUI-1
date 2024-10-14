using System.Collections.Generic;

namespace ET.Client
{
    public static class LoginHelper
    {
        public static async ETTask Login(Scene root, string account, string password)
        {
            root.RemoveComponent<ClientSenderComponent>();

            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();

            NetClient2Main_Login response = await clientSenderComponent.LoginAsync(account, password);

            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();

            // playerComponent.MyId = playerId;

            playerComponent.ServerInfos = response.ServerInfos;

            playerComponent.RoleInfos = response.RoleInfos;

            playerComponent.Address = response.Address;

            playerComponent.Account = account;

            playerComponent.Password = password;

            playerComponent.LoginGateKey = response.LoginGateKey;

            playerComponent.GateId = response.GateId;

            // await EnterMapHelper.EnterMapAsync(root);
        }

        public static async ETTask LoginGate(Scene root, int zoneConfigId)
        {
            PlayerComponent playerComponent = root.GetComponent<PlayerComponent>();

            ClientSenderComponent clientSenderComponent = root.GetComponent<ClientSenderComponent>();

            playerComponent.MyId = await clientSenderComponent.LoginGateAsync(playerComponent, zoneConfigId);
        }
    }
}