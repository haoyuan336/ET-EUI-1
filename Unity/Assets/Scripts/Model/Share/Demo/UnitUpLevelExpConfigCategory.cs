using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public partial class UnitUpLevelExpConfigCategory
    {
        private Dictionary<int, UnitUpLevelExpConfig> Dictionary = new Dictionary<int, UnitUpLevelExpConfig>();

        public override void EndInit()
        {
            foreach (var unitUpLevelExpConfig in this.dict.Values.ToList())
            {
                this.Dictionary[unitUpLevelExpConfig.NextLevel] = unitUpLevelExpConfig;
            }
        }

        public UnitUpLevelExpConfig GetByLevel(int level)
        {
            if (this.Dictionary.TryGetValue(level, out UnitUpLevelExpConfig config))
            {
                return config;
            }

            return null;
        }
    }
}