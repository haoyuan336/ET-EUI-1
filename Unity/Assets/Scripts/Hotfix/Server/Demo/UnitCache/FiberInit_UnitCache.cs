using System.ComponentModel.Design;
using ET.Server;

namespace ET
{
    [Invoke((long)SceneType.UnitCache)]
    public class FiberInit_UnitCache: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;

            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);

            root.AddComponent<TimerComponent>();

            root.AddComponent<CoroutineLockComponent>();

            root.AddComponent<ProcessInnerSender>();

            root.AddComponent<MessageSender>();

            root.AddComponent<UnitCacheComponent>();

            DBManagerComponent dbManagerComponent = root.AddComponent<DBManagerComponent>();

            Log.Debug("create unitcache scene");

            // Entity entity = await dbManagerComponent.GetZoneDB(root.Zone()).Query<Entity>(1217814309371910, "HeroCardComponent");

            // Log.Debug($"entity is null {entity}");

            await ETTask.CompletedTask;
        }
    }
}