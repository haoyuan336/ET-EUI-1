using System.Collections.Generic;

namespace ET
{
    public partial class MapConfigCategory
    {
        private MapConfig MainCity = null;

        public Dictionary<string, List<MapConfig>> ChapterList = new Dictionary<string, List<MapConfig>>();

        public override void EndInit()
        {
            this.MainCity = this.Get(1000);

            foreach (var kv in this.dict)
            {
                MapConfig mapConfig = kv.Value;

                if (this.ChapterList.TryGetValue(mapConfig.ChapterName, out List<MapConfig> list))
                {
                    list.Add(mapConfig);
                }
                else
                {
                    this.ChapterList.Add(mapConfig.ChapterName, new List<MapConfig>() { mapConfig });
                }
            }
        }

        public MapConfig GetMainCity()
        {
            return this.MainCity;
        }
    }
}