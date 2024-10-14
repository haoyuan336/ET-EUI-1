using System;
using ET.Server;

namespace ET
{
    [FriendOfAttribute(typeof (ET.UnitDBSaveComponent))]
    public class UnitDBSaveComponentAwakeSystem: AwakeSystem<UnitDBSaveComponent>
    {
        protected override void Awake(UnitDBSaveComponent self)
        {
            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            self.Timer = timerComponent.NewRepeatedTimer(10000, TimerInvokeType.SaveChangeDBData, self);
        }
    }

    [FriendOfAttribute(typeof (ET.UnitDBSaveComponent))]
    public class UnitDBSaveComponentDestroySystem: DestroySystem<UnitDBSaveComponent>
    {
        protected override void Destroy(UnitDBSaveComponent self)
        {
            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            timerComponent.Remove(ref self.Timer);
        }
    }

    // public class UnitGetComponentSystem: GetComponentSystem<Unit>
    // {
    //     public override void GetComponent(Unit unit, Entity component)
    //     {
    //         Type type = component.GetType();
    //
    //         Log.Debug($" UnitDBSaveComponent get component {type.FullName}");
    //
    //         if (!(typeof (IUnitCache).IsAssignableFrom(type)))
    //         {
    //             return;
    //         }
    //
    //         unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
    //     }
    // }

    public static class UnitDBSaveComponentSystem
    {
        //

        [Invoke(TimerInvokeType.SaveChangeDBData)]
        public class SaveChangeDBData: ATimer<UnitDBSaveComponent>
        {
            protected override void Run(UnitDBSaveComponent self)
            {
                try
                {
                    if (self.IsDisposed || self.Parent == null)
                    {
                        return;
                    }

                    if (self.Root() == null)
                    {
                        return;
                    }

                    self?.SaveChange();
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }
        }

        public static void AddChange(this UnitDBSaveComponent self, Type t)
        {
            self.EntityChangeTypeSet.Add(t);
        }

        public static void SaveChange(this UnitDBSaveComponent self)
        {
            Log.Debug($"save change {self.EntityChangeTypeSet.Count}");
            
            if (self.EntityChangeTypeSet.Count <= 0)
            {
                return;
            }

            Log.Debug($"entity change type set {self.EntityChangeTypeSet.Count}");
            
            Unit unit = self.GetParent<Unit>();

            Other2UnitCache_AddOrUpdateUnit message = Other2UnitCache_AddOrUpdateUnit.Create();

            message.UnitId = unit.Id; 

            message.EntityTypes.Add(unit.GetType().FullName);

            message.EntityBytes.Add(MongoHelper.Serialize(unit));

            foreach (Type type in self.EntityChangeTypeSet)
            {
                Entity entity = unit.GetComponent(type);
                if (entity == null || entity.IsDisposed)
                {
                    continue;
                }

                Log.Debug("开始保存变化部分的Entity数据 : " + type.FullName);
                message.EntityTypes.Add(type.FullName);

                message.EntityBytes.Add(MongoHelper.Serialize(entity));
            }

            self.EntityChangeTypeSet.Clear();

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(self.Zone(), $"UnitCache{self.Zone()}");

            ActorId actorId = startSceneConfig.ActorId;

            self.Root().GetComponent<MessageSender>().Call(actorId, message).Coroutine();
        }
    }
}