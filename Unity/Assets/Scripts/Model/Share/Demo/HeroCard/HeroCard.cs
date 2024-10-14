namespace ET
{
    [ChildOf(typeof (HeroCardComponent))]

#if UNITY
    public class HeroCard : Entity, IAwake
#else
    public class HeroCard: Entity, IAwake, ISerializeToEntity
#endif
    {
        public int HeroConfigId { get; set; }
    }
}