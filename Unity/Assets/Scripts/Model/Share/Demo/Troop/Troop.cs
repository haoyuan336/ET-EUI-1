using NativeCollection.UnsafeType;

namespace ET
{
    [ChildOf(typeof(TroopComponent))]
#if UNITY
    public class Troop : Entity, IAwake
#else
    public class Troop: Entity, IAwake, ISerializeToEntity
#endif
    {
        public long[] HeroCardIds = new long[10];

        public int ConfigId = 0;

        public TroopConfig Config => TroopConfigCategory.Instance.Get(this.ConfigId);
    }
}