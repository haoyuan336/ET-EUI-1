using System.Collections.Generic;
using System.Linq;
using ET.Server;

namespace ET
{
    [EntitySystemOf(typeof (ItemComponent))]
    [FriendOfAttribute(typeof (ET.ItemComponent))]
    public static partial class ItemComponentSystem
    {
        [EntitySystem]
        public static void Awake(this ItemComponent self)
        {
        }
#if !UNITY

        [EntitySystem]
        private static void Deserialize(this ItemComponent self)
        {
            List<int> keys = ItemConfigCategory.Instance.GetAll().Keys.ToList();

            Log.Debug($"itemcomponent deserialize {keys.Count}");
            
            List<int> removeList = new List<int>();

            foreach (var kv in self.Items)
            {
                if (!keys.Contains(int.Parse(kv.Key)))
                {
                    removeList.Add(int.Parse(kv.Key));
                }
            }

            foreach (var key in removeList)
            {
                self.Items.Remove(key.ToString());
            }

            foreach (var key in keys)
            {
                if (!self.Items.ContainsKey(key.ToString()))
                {
                    ItemConfig itemConfig = ItemConfigCategory.Instance.Get(key);

                    self.Items[key.ToString()] = itemConfig.DefaultCount;
                }
            }
        }

        public static (List<string>, List<int>) GetItemsInfo(this ItemComponent self)
        {
            List<string> keys = self.Items.Keys.ToList();

            List<int> values = self.Items.Values.ToList();

            return (keys, values);
        }
#endif
    }
}