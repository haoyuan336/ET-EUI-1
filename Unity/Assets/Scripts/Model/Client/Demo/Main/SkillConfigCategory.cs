using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public partial class SkillConfigCategory
    {
        private Dictionary<int, List<SkillConfig>> Dictionary = new Dictionary<int, List<SkillConfig>>();

        public List<SkillConfig> GetByOnwerConfigs(int configId)
        {
            return this.Dictionary[configId];
        }

        public override void EndInit()
        {
            foreach (var kv in this.dict)
            {
                SkillConfig config = kv.Value;

                int ownerRoleConfigId = config.OwnerRoleConfigId;

                if (this.Dictionary.TryGetValue(ownerRoleConfigId, out List<SkillConfig> configs))
                {
                    configs.Add(config);
                }
                else
                {
                    this.Dictionary.Add(ownerRoleConfigId, new List<SkillConfig>() { config });
                }
            }
        }
    }
}