using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof (ServerManagerComponent))]
    [FriendOfAttribute(typeof (ET.Server.ServerManagerComponent))]
    [FriendOfAttribute(typeof (ET.Server.Server))]
    public static partial class ServerManagerComponentSystem
    {
        [Invoke(TimerInvokeType.AutoSaveServerInfo)]
        public class AutoSaveServerInfo: ATimer<ServerManagerComponent>
        {
            protected override void Run(ServerManagerComponent t)
            {
                t.AutoSave().Coroutine();
            }
        }

        public static async ETTask AutoSave(this ServerManagerComponent self)
        {
            if (!self.IsChanged)
            {
                return;
            }

            DBManagerComponent dbManagerComponent = self.Root().GetComponent<DBManagerComponent>();

            await dbManagerComponent.GetZoneDB(self.Zone()).Save(self);

            self.IsChanged = false;
        }

        [EntitySystem]
        public static void Awake(this ServerManagerComponent self)
        {
            //查找
            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            int random = RandomGenerator.RandomNumber(1000, 2000);

            timerComponent.NewRepeatedTimer(10000 + random, TimerInvokeType.AutoSaveServerInfo, self);

            self.InitZone();
        }

        public static void InitZone(this ServerManagerComponent self)
        {
            List<StartSceneConfig> configs = new List<StartSceneConfig>();

            foreach (var kv in StartSceneConfigCategory.Instance.GetAll())
            {
                if (kv.Value.SceneType == SceneType.Map.ToString())
                {
                    configs.Add(kv.Value);
                }
            }

            Log.Debug($"init zone {configs.Count}");

            List<Server> servers = new List<Server>();

            List<Server> rmServers = new List<Server>();
            foreach (var kv in self.Children)
            {
                Server server = kv.Value as Server;

                if (!configs.Exists(a => a.Id == server.ZoneConfigId))
                {
                    rmServers.Add(server);
                }
                else
                {
                    servers.Add(server);
                }
            }

            foreach (var server in rmServers)
            {
                server.Dispose();
            }

            foreach (var config in configs)
            {
                if (!servers.Exists(a => a.ZoneConfigId == config.Id))
                {
                    Server server = self.AddChild<Server>();

                    server.ZoneConfigId = config.Id;

                    server.ServerName = config.ServerName;

                    server.ServerState = config.ServerState;

                    server.StartTimeStamp = TimeInfo.Instance.ServerNow();
                }
            }

            Log.Debug($"child count {self.Children.Count}");

            servers.Clear();

            self.IsChanged = true;
        }
    }
}