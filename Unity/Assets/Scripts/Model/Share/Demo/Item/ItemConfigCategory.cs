using System.Collections.Generic;

namespace ET
{
    public partial class ItemConfigCategory
    {
        private Dictionary<ItemType, ItemConfig> ItemConfigs = new Dictionary<ItemType, ItemConfig>();

        public override void EndInit()
        {
            foreach (var config in this.GetAll().Values)
            {
                if (!string.IsNullOrEmpty(config.ItemType))
                {
                    ItemType itemType = EnumHelper.FromString<ItemType>(config.ItemType);

                    this.ItemConfigs[itemType] = config;
                }
            }
        }

        public ItemConfig GetConfigByType(ItemType itemType)
        {
            if (this.ItemConfigs.TryGetValue(itemType, out ItemConfig config))
            {
                return config;
            }

            return null;
        }
    }
}