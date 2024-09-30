using System.Net;

namespace ET.Server
{
    [Invoke((long)SceneType.Router)]
    public class FiberInit_Router: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Log.Warning($"fiber init router");
            Scene root = fiberInit.Fiber.Root;
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.Get((int)root.Id);
            
            // 开发期间使用OuterIPPort，云服务器因为本机没有OuterIP，所以要改成InnerIPPort，然后在云防火墙中端口映射到InnerIPPort
            // root.AddComponent<RouterComponent, IPEndPoint, string>(startSceneConfig.OuterIPPort, startSceneConfig.StartProcessConfig.InnerIP);

            Log.Debug($"start config {Options.Instance.StartConfig}");
            
            if (Options.Instance.StartConfig == "StartConfig/Release")
            {
                root.AddComponent<RouterComponent, IPEndPoint, string>(startSceneConfig.InnerIPPort, startSceneConfig.StartProcessConfig.InnerIP);

            }
            else
            {
                root.AddComponent<RouterComponent, IPEndPoint, string>(startSceneConfig.OuterIPPort, startSceneConfig.StartProcessConfig.InnerIP);
            }

            await ETTask.CompletedTask;
        }
    }
}