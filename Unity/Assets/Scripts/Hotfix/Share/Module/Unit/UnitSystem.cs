using System;
using ET.Server;

namespace ET
{
    [EntitySystemOf(typeof (Unit))]
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
            if (!(typeof (IUnitCache).IsAssignableFrom(args2)))
            {
                return;
            }

            UnitDBSaveComponent dbManagerComponent = self.GetComponent<UnitDBSaveComponent>();

            dbManagerComponent.AddChange(args2);
#endif
        }
        // public class UnitAddComponentSystem : AddComponentSystem<Unit>
        // {
        //     public override void AddComponent(Unit unit, Entity component)
        //     {
        //         Type type = component.GetType();
        //         if (!(typeof (IUnitCache).IsAssignableFrom(type)) )
        //         {
        //             return;
        //         }
        //         unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        //     }
        // }
        // //
        // public class UnitGetComponentSystem : GetComponentSystem<Unit>
        // {
        //     public override void GetComponent(Unit unit, Entity component)
        //     {
        //         Type type = component.GetType();
        //         if (!(typeof (IUnitCache).IsAssignableFrom(type)) )
        //         {
        //             return;
        //         }
        //         unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        //     }
        // }
    }
}