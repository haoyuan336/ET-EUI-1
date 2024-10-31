using System.Collections.Generic;

namespace ET
{
    public enum ItemType
    {
        Gold = 1,
        Diamond = 2,
        Meat = 3,
        Wood = 4,
        BlueStone = 5,
        RedStone = 6
    }

    [ComponentOf(typeof(Unit))]
#if UNITY
    public class ItemComponent : Entity, IAwake
#else
    public class ItemComponent: Entity, IAwake, IUnitCache, ITransfer, IDeserialize
#endif
    {
        public Dictionary<string, int> Items = new Dictionary<string, int>();
    }
}