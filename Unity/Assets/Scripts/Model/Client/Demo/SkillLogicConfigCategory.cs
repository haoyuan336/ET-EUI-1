using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public partial class SkillLogicConfigCategory
    {
        public List<SkillLogicConfig> GetLogicConfigs(int configId, int level)
        {
            List<SkillLogicConfig> configs = this.Dictionary[configId];

            Dictionary<string, SkillLogicConfig> dictionary = new Dictionary<string, SkillLogicConfig>();

            foreach (var config in configs)
            {
                if (config.Level <= level)
                {
                    dictionary[config.LogicCode] = config;
                }
            }

            return dictionary.Values.ToList();
        }

        public Dictionary<int, List<SkillLogicConfig>> Dictionary = new Dictionary<int, List<SkillLogicConfig>>();

        public override void EndInit()
        {
            foreach (var kv in this.dict)
            {
                SkillLogicConfig config = kv.Value;

                if (this.Dictionary.TryGetValue(config.OwnerSkillConfigId, out List<SkillLogicConfig> list))
                {
                    list.Add(config);
                }
                else
                {
                    this.Dictionary.Add(config.OwnerSkillConfigId, new List<SkillLogicConfig>() { config });
                }
            }
        }
    }
}