using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [FriendOfAttribute(typeof (ET.Troop))]
    public static partial class TroopSystem
    {
        public static TroopInfo GetInfo(this Troop self)
        {
            TroopInfo troopInfo = TroopInfo.Create();

            troopInfo.TroopId = self.Id;

            troopInfo.HeroCardIds = self.HeroCardIds.ToList();

            return troopInfo;
        }

        public static void SetInfo(this Troop self, TroopInfo troopInfo)
        {
            self.HeroCardIds = troopInfo.HeroCardIds.ToArray();
        }
    }
}