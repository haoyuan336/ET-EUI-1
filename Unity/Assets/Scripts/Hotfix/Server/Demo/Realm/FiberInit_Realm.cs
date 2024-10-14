using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ET.Server
{
    [Invoke((long)SceneType.Realm)]
    public class FiberInit_Realm: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<MessageSender>();
            root.AddComponent<DBManagerComponent>();
            root.AddComponent<AccountComponent>();
            await this.AddServerManagerComponent(root);
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.Get(root.Fiber.Id);
            root.AddComponent<NetComponent, IPEndPoint, NetworkProtocol>(startSceneConfig.InnerIPPort, NetworkProtocol.UDP);

            await ETTask.CompletedTask;
        }

        private async ETTask AddServerManagerComponent(Scene scene)
        {
            DBManagerComponent dbManagerComponent = scene.GetComponent<DBManagerComponent>();

            ServerManagerComponent serverManagerComponent = await dbManagerComponent.GetZoneDB(scene.Zone()).Query<ServerManagerComponent>(scene.Id);

            if (serverManagerComponent == null)
            {
                scene.AddComponent<ServerManagerComponent>();
            }
            else
            {
                scene.AddComponent(serverManagerComponent);
                
                serverManagerComponent.Awake();
            }

            await ETTask.CompletedTask;
        }
    }
}