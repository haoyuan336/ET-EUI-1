using System.Diagnostics;

namespace ET
{
    [ChildOf(typeof(UnitComponent))]
    [DebuggerDisplay("ViewName,nq")]
#if UNITY
    public partial class Unit : Entity, IAwake<int>, IGetComponentSys
#else
    public partial class Unit: Entity, IAwake<int>, IGetComponentSys
#endif
    {
        public int ConfigId { get; set; } //配置表id

        public int Level = 1;

        public int CurrentExp;

        public int FightPower = 0;

#if UNITY
        public int CurrentMapConfigId = MapConfigCategory.Instance.GetMainCity().Id;
#endif
    }
}