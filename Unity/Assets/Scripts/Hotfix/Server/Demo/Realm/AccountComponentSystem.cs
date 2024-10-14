using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ET.Server
{
    [FriendOfAttribute(typeof (ET.Server.AccountComponent))]
    public static class AccountComponentSystem
    {
        public static void Add(this AccountComponent self, string key, Account account)
        {
            self.Accounts.Add(key, account);
        }

        public static void Remove(this AccountComponent self, string key)
        {
            self.Accounts.Remove(key);
        }

        public static Account Get(this AccountComponent self, string key)
        {
            if (self.Accounts.TryGetValue(key, out EntityRef<Account> account))
            {
                return account;
            }

            return null;
        }
    }
}