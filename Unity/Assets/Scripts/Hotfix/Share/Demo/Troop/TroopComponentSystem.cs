using System.Collections.Generic;
using System.Linq;

namespace ET
{
    [EntitySystemOf(typeof (TroopComponent))]
    [FriendOfAttribute(typeof (ET.Troop))]
    public static partial class TroopComponentSystem
    {
        [EntitySystem]
        public static void Deserialize(this TroopComponent self)
        {
            Log.Debug("TroopComponent Deserialize");
            
            List<TroopConfig> troopConfigs = TroopConfigCategory.Instance.GetAll().Values.ToList();

            foreach (var troopConfig in troopConfigs)
            {
                bool isCon = false;

                foreach (var entity in self.Children.Values)
                {
                    Troop troop = entity as Troop;

                    if (troop.ConfigId == troopConfig.Id)
                    {
                        isCon = true;

                        continue;
                    }
                }

                if (!isCon)
                {
                    Troop troop = self.AddChild<Troop>();

                    troop.ConfigId = troopConfig.Id;
                }
            }

            List<Troop> disposeEntitys = new List<Troop>();

            foreach (var kv in self.Children)
            {
                Troop troop = kv.Value as Troop;

                if (!troopConfigs.Exists(a => a.Id == troop.ConfigId))
                {
                    disposeEntitys.Add(troop);
                }
            }

            Log.Debug($"disponse entity {disposeEntitys.Count}");

            foreach (var entity in disposeEntitys)
            {
                entity.Dispose();
            }
        }

        [EntitySystem]
        public static void Awake(this TroopComponent self)
        {
        }
    }
}