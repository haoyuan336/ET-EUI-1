using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.WebSockets;

namespace ET.Client
{
    public class UnityWebSocketTransport : IKcpTransport
    {
        public readonly ClientWebSocket Socket;

        public WSService WService;

        public bool IsContent = false;

        private readonly DoubleMap<long, EndPoint> idEndpoints = new();

        private readonly Queue<(EndPoint, MemoryBuffer)> channelRecvDatas = new();

        private readonly Dictionary<long, long> readWriteTime = new();

        private readonly Queue<long> channelIds = new();

        public UnityWebSocketTransport()
        {
            // Log.Debug($"ip end point {addressFamily}");

            // this.socket = new Socket(addressFamily, SocketType.Dgram, ProtocolType.Udp);
            // NetworkHelper.SetSioUdpConnReset(this.socket);

            // this.Socket = new ClientWebSocket();

            this.WService = new WSService();

            this.WService.ReadCallback = this.OnRead;

            this.WService.ErrorCallback = this.OnError;
        }

        public UnityWebSocketTransport(IPEndPoint ipEndPoint)
        {
            // string address = $"http://{ipEndPoint.ToString()}/";
            //
            // Log.Debug($"websocket transport address {address}");
            //
            // this.WService = new WService(new[] { address });
            //
            // this.WService.AcceptCallback = this.OnAccept;
            //
            // this.WService.ReadCallback = this.OnRead;
            //
            // this.WService.ErrorCallback = this.OnError;
        }

        private void OnAccept(long id, IPEndPoint ipEndPoint)
        {
            // WChannel channel = this.WService.Get(id);
            // long timeNow = TimeInfo.Instance.ClientFrameTime();
            // this.readWriteTime[id] = timeNow;
            // this.channelIds.Enqueue(id);
            // this.idEndpoints.Add(id, channel.RemoteAddress);
        }

        public void OnError(long id, int error)
        {
            Log.Warning($"IKcpTransport tcp error: {id} {error}");
            this.WService.Remove(id, error);
            this.idEndpoints.RemoveByKey(id);
            this.readWriteTime.Remove(id);
        }

        private void OnRead(long id, MemoryBuffer memoryBuffer)
        {
            long timeNow = TimeInfo.Instance.ClientFrameTime();
            this.readWriteTime[id] = timeNow;
            WSChannel channel = this.WService.Get(id);
            channelRecvDatas.Enqueue((channel.RemoteAddress, memoryBuffer));
        }

        public void Dispose()
        {
            this.WService.Dispose();
        }

        public void Send(byte[] bytes, int index, int length, EndPoint endPoint)
        {
            long id = this.idEndpoints.GetKeyByValue(endPoint);

            Log.Warning($" WebSocketTransport send  {id} {endPoint}");

            if (id == 0)
            {
                id = IdGenerater.Instance.GenerateInstanceId();

                Log.Warning($"id {id}");

                this.WService.Create(id, (IPEndPoint)endPoint);
                this.idEndpoints.Add(id, endPoint);
                this.channelIds.Enqueue(id);
            }

            MemoryBuffer memoryBuffer = this.WService.Fetch();
            memoryBuffer.Write(bytes, index, length);
            memoryBuffer.Seek(0, SeekOrigin.Begin);
            this.WService.Send(id, memoryBuffer);

            long timeNow = TimeInfo.Instance.ClientFrameTime();
            this.readWriteTime[id] = timeNow;
        }

        public int Recv(byte[] buffer, ref EndPoint endPoint)
        {
            return RecvNonAlloc(buffer, ref endPoint);
        }

        public int RecvNonAlloc(byte[] buffer, ref EndPoint endPoint)
        {
            (EndPoint e, MemoryBuffer memoryBuffer) = this.channelRecvDatas.Dequeue();
            endPoint = e;
            int count = memoryBuffer.Read(buffer);
            this.WService.Recycle(memoryBuffer);
            return count;
        }

        public int Available()
        {
            return this.channelRecvDatas.Count;
        }

        public void Update()
        {
            // 检查长时间不读写的TChannel, 超时断开, 一次update检查10个
            long timeNow = TimeInfo.Instance.ClientFrameTime();
            const int MaxCheckNum = 10;
            int n = this.channelIds.Count < MaxCheckNum ? this.channelIds.Count : MaxCheckNum;
            for (int i = 0; i < n; ++i)
            {
                long id = this.channelIds.Dequeue();
                if (!this.readWriteTime.TryGetValue(id, out long rwTime))
                {
                    continue;
                }

                if (timeNow - rwTime > 30 * 1000)
                {
                    this.OnError(id, ErrorCore.ERR_KcpReadWriteTimeout);
                    continue;
                }

                this.channelIds.Enqueue(id);
            }

            this.WService.Update();
        }
    }
}