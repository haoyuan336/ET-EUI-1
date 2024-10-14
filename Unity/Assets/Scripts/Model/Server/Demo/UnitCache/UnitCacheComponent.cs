using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof (Scene))]
    public class UnitCacheComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<string, EntityRef<UnitCache>> UnitCaches = new Dictionary<string, EntityRef<UnitCache>>();

        public List<string> UnitCacheKeyList = new List<string>();
    }
}