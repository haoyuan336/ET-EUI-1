using MemoryPack;
using System.Collections.Generic;

namespace ET
{
    [MemoryPackable]
    [Message(ClientMessage.Main2NetClient_Login)]
    [ResponseType(nameof(NetClient2Main_Login))]
    public partial class Main2NetClient_Login : MessageObject, IRequest
    {
        public static Main2NetClient_Login Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(Main2NetClient_Login), isFromPool) as Main2NetClient_Login;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int OwnerFiberId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [MemoryPackOrder(2)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MemoryPackOrder(3)]
        public string Password { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.OwnerFiberId = default;
            this.Account = default;
            this.Password = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.NetClient2Main_Login)]
    public partial class NetClient2Main_Login : MessageObject, IResponse
    {
        public static NetClient2Main_Login Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(NetClient2Main_Login), isFromPool) as NetClient2Main_Login;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(4)]
        public List<ServerInfo> ServerInfos { get; set; } = new();

        [MemoryPackOrder(5)]
        public List<RoleInfo> RoleInfos { get; set; } = new();

        [MemoryPackOrder(6)]
        public string Address { get; set; }

        [MemoryPackOrder(7)]
        public long GateId { get; set; }

        [MemoryPackOrder(8)]
        public long LoginGateKey { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.ServerInfos.Clear();
            this.RoleInfos.Clear();
            this.Address = default;
            this.GateId = default;
            this.LoginGateKey = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.Main2ViewClient_HttpHelper)]
    [ResponseType(nameof(View2MainClient_HttpHelper))]
    public partial class Main2ViewClient_HttpHelper : MessageObject, IRequest
    {
        public static Main2ViewClient_HttpHelper Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(Main2ViewClient_HttpHelper), isFromPool) as Main2ViewClient_HttpHelper;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public string Url { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Url = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.View2MainClient_HttpHelper)]
    public partial class View2MainClient_HttpHelper : MessageObject, IResponse
    {
        public static View2MainClient_HttpHelper Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(View2MainClient_HttpHelper), isFromPool) as View2MainClient_HttpHelper;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public string Text { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.Text = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.Main2NetClient_LoginGate)]
    [ResponseType(nameof(NetClient2Main_LoginGate))]
    public partial class Main2NetClient_LoginGate : MessageObject, IRequest
    {
        public static Main2NetClient_LoginGate Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(Main2NetClient_LoginGate), isFromPool) as Main2NetClient_LoginGate;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public long Key { get; set; }

        [MemoryPackOrder(2)]
        public long GateId { get; set; }

        [MemoryPackOrder(3)]
        public string Address { get; set; }

        [MemoryPackOrder(4)]
        public string Account { get; set; }

        [MemoryPackOrder(5)]
        public string Password { get; set; }

        [MemoryPackOrder(6)]
        public int ZoneConfigId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Key = default;
            this.GateId = default;
            this.Address = default;
            this.Account = default;
            this.Password = default;
            this.ZoneConfigId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    [MemoryPackable]
    [Message(ClientMessage.NetClient2Main_LoginGate)]
    public partial class NetClient2Main_LoginGate : MessageObject, IResponse
    {
        public static NetClient2Main_LoginGate Create(bool isFromPool = false)
        {
            return ObjectPool.Instance.Fetch(typeof(NetClient2Main_LoginGate), isFromPool) as NetClient2Main_LoginGate;
        }

        [MemoryPackOrder(0)]
        public int RpcId { get; set; }

        [MemoryPackOrder(1)]
        public int Error { get; set; }

        [MemoryPackOrder(2)]
        public string Message { get; set; }

        [MemoryPackOrder(3)]
        public long PlayerId { get; set; }

        public override void Dispose()
        {
            if (!this.IsFromPool)
            {
                return;
            }

            this.RpcId = default;
            this.Error = default;
            this.Message = default;
            this.PlayerId = default;

            ObjectPool.Instance.Recycle(this);
        }
    }

    public static class ClientMessage
    {
        public const ushort Main2NetClient_Login = 1001;
        public const ushort NetClient2Main_Login = 1002;
        public const ushort Main2ViewClient_HttpHelper = 1003;
        public const ushort View2MainClient_HttpHelper = 1004;
        public const ushort Main2NetClient_LoginGate = 1005;
        public const ushort NetClient2Main_LoginGate = 1006;
    }
}