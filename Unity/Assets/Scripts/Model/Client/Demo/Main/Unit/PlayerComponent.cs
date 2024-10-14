using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class PlayerComponent: Entity, IAwake
    {
        public long MyId { get; set; }

        public List<ServerInfo> ServerInfos;

        public List<RoleInfo> RoleInfos;

        public string Address;

        public string Account;

        public string Password;

        public long LoginGateKey;

        public long GateId;
    }
}