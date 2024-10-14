using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof (Scene))]
    public class AccountComponent: Entity, IAwake
    {
        public Dictionary<string, EntityRef<Account>> Accounts = new Dictionary<string, EntityRef<Account>>();
    }
}