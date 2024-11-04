using System;
using ET.Server;

namespace ET
{
    [EntitySystemOf(typeof(Unit))]
    [FriendOfAttribute(typeof(ET.Unit))]
    public static partial class UnitSystem
    {
        // [EntitySystem]
        // public class UnitAddComponentSystem : AddComponentSystem<Unit>
        // {
        //     public override void AddComponent(Unit unit, Entity component)
        //     {
        //         Type type = component.GetType();
        //
        //         Log.Debug("add component");
        //         if (!(typeof(IUnitCache).IsAssignableFrom(type)))
        //         {
        //             return;
        //         }
        //
        //         unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        //     }
        // }

        [EntitySystem]
        private static void Awake(this Unit self, int configId)
        {
            self.ConfigId = configId;
        }

        public static UnitConfig Config(this Unit self)
        {
            return UnitConfigCategory.Instance.Get(self.ConfigId);
        }

        public static UnitType Type(this Unit self)
        {
            return (UnitType)self.Config().Type;
        }

        [EntitySystem]
        private static void GetComponentSys(this Unit self, System.Type args2)
        {
#if !UNITY
            if (!(typeof(IUnitCache).IsAssignableFrom(args2)))
            {
                return;
            }

            UnitDBSaveComponent dbManagerComponent = self.GetComponent<UnitDBSaveComponent>();

            dbManagerComponent.AddChange(args2);
#endif
        }

        public static UnitInfo GetUnitInfo(this Unit self)
        {
            UnitInfo unitInfo = UnitInfo.Create();

            unitInfo.UnitId = self.Id;

            unitInfo.Level = self.Level;

            unitInfo.FightPower = self.FightPower;

            unitInfo.CurrentExp = self.CurrentExp;

            return unitInfo;
        }

        public static void SetUnitInfo(this Unit self, UnitInfo unitInfo)
        {
            self.Level = unitInfo.Level;

            self.FightPower = unitInfo.FightPower;

            self.CurrentExp = unitInfo.CurrentExp;
        }
    }
}