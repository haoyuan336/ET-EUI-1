namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UnitComponent : Entity, IAwake, IDestroy
    {
#if UNITY

        public Unit MyUnit;
#endif
    }
}