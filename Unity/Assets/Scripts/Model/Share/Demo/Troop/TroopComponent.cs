namespace ET
{
    [ComponentOf(typeof (Unit))]
#if UNITY
    public class TroopComponent : Entity, IAwake, IDeserialize
#else
    public class TroopComponent: Entity, IAwake, IUnitCache, IDeserialize, ITransfer
#endif
    {
    }
}