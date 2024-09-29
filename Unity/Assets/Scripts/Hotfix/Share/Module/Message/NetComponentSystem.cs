using System.Net;
using System.Net.Sockets;

namespace ET
{
    [EntitySystemOf(typeof(NetComponent))]
    [FriendOf(typeof(NetComponent))]
    public static partial class NetComponentSystem
    {
        [EntitySystem]
        private static void Awake(this NetComponent self, IPEndPoint address, NetworkProtocol protocol)
        {
            self.AService = new KService(address, protocol, ServiceType.Outer);
            self.AService.AcceptCallback = self.OnAccept;
            self.AService.ReadCallback = self.OnRead;
            self.AService.ErrorCallback = self.OnError;
        }
        //
        // [EntitySystem]
        // private static void Awake(this NetComponent self, IPEndPoint address)
        // {
        //     self.AService = new WService();
        //     long id = IdGenerater.Instance.GenerateId();
        //     self.AService.Create(id, address);
        //     self.AService.AcceptCallback = self.OnAccept;
        //     self.AService.ReadCallback = self.OnRead;
        //     self.AService.ErrorCallback = self.OnError;
        //
        // }

        [EntitySystem]
        private static void Awake(this NetComponent self, AddressFamily addressFamily, NetworkProtocol protocol)
        {
            self.AService = new KService(addressFamily, protocol, ServiceType.Outer);
            self.AService.ReadCallback = self.OnRead;
            self.AService.ErrorCallback = self.OnError;
        }

        [EntitySystem]
        private static void Update(this NetComponent self)
        {
            self.AService.Update();
        }

        [EntitySystem]
        private static void Destroy(this NetComponent self)
        {
            self.AService.Dispose();
        }

        public static void OnError(this NetComponent self, long channelId, int error)
        {
            Session session = self.GetChild<Session>(channelId);
            if (session == null)
            {
                return;
            }

            session.Error = error;
            session.Dispose();
        }

        // 这个channelId是由CreateAcceptChannelId生成的
        private static void OnAccept(this NetComponent self, long channelId, IPEndPoint ipEndPoint)
        {
            Session session = self.AddChildWithId<Session, AService>(channelId, self.AService);
            session.RemoteAddress = ipEndPoint;

            if (self.IScene.SceneType != SceneType.BenchmarkServer)
            {
                // 挂上这个组件，5秒就会删除session，所以客户端验证完成要删除这个组件。该组件的作用就是防止外挂一直连接不发消息也不进行权限验证
                session.AddComponent<SessionAcceptTimeoutComponent>();
                // 客户端连接，2秒检查一次recv消息，10秒没有消息则断开
                session.AddComponent<SessionIdleCheckerComponent>();
            }
        }

        public static void OnRead(this NetComponent self, long channelId, MemoryBuffer memoryBuffer)
        {
            Session session = self.GetChild<Session>(channelId);
            if (session == null)
            {
                return;
            }

            session.LastRecvTime = TimeInfo.Instance.ClientNow();

            (ActorId _, object message) = MessageSerializeHelper.ToMessage(self.AService, memoryBuffer);
            self.AService.Recycle(memoryBuffer);

            LogMsg.Instance.Debug(self.Fiber(), message);

            EventSystem.Instance.Invoke((long)self.IScene.SceneType, new NetComponentOnRead() { Session = session, Message = message });
        }

        public static Session Create(this NetComponent self, IPEndPoint realIPEndPoint)
        {
            Log.Warning($"net component create {realIPEndPoint}");
            long channelId = NetServices.Instance.CreateConnectChannelId();
            Log.Warning($"channel id {channelId}");
            Session session = self.AddChildWithId<Session, AService>(channelId, self.AService);
            session.RemoteAddress = realIPEndPoint;
            if (self.IScene.SceneType != SceneType.BenchmarkClient)
            {
                session.AddComponent<SessionIdleCheckerComponent>();
            }

            self.AService.Create(session.Id, session.RemoteAddress);

            return session;
        }

        public static Session Create(this NetComponent self, IPEndPoint routerIPEndPoint, IPEndPoint realIPEndPoint, uint localConn)
        {
            Log.Warning($"net component create {localConn} {routerIPEndPoint} {realIPEndPoint}");
            long channelId = localConn;
            Session session = self.AddChildWithId<Session, AService>(channelId, self.AService);
            session.RemoteAddress = realIPEndPoint;
            if (self.IScene.SceneType != SceneType.BenchmarkClient)
            {
                session.AddComponent<SessionIdleCheckerComponent>();
            }

            self.AService.Create(session.Id, routerIPEndPoint);
            return session;
        }

        [EntitySystem]
        private static void Awake(this ET.NetComponent self, System.Net.IPEndPoint args2)
        {
        }

        [EntitySystem]
        private static void Awake(this ET.NetComponent self, ET.NetworkProtocol args2)
        {
        }
        [EntitySystem]
        private static void Awake(this ET.NetComponent self)
        {

        }
    }
}