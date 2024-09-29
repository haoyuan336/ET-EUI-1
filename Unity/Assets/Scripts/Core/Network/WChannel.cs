using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ET
{
    public class WChannel: AChannel
    {
        public HttpListenerWebSocketContext WebSocketContext { get; }

        private readonly WService Service;

        private readonly WebSocket webSocket;

        private readonly Queue<MemoryBuffer> queue = new();

        private bool isSending;

        private bool isConnected;

        private CancellationTokenSource cancellationTokenSource = new();

        public WChannel(long id, HttpListenerWebSocketContext webSocketContext, WService service)
        {
            Log.Warning("WChannel HttpListenerWebSocketContext ");
            this.Service = service;
            this.Id = id;
            this.ChannelType = ChannelType.Accept;
            this.WebSocketContext = webSocketContext;
            this.webSocket = webSocketContext.WebSocket;

            isConnected = true;

            this.Service.ThreadSynchronizationContext.Post(() =>
            {
                this.StartRecv().Coroutine();
                this.StartSend().Coroutine();
            });
        }

        public WChannel(long id, ClientWebSocket webSocket, IPEndPoint ipEndPoint, WService service)
        {
            Log.Warning("WChannel is new gouzao");
            this.Service = service;
            this.Id = id;
            this.ChannelType = ChannelType.Connect;
            this.webSocket = webSocket;
            isConnected = false;
            this.Service.ThreadSynchronizationContext.Post(() =>
            {
                Log.Warning("ThreadSynchronizationContext posy");
                this.ConnectAsync($"ws://{ipEndPoint}").Coroutine();
            });
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.cancellationTokenSource.Cancel();
            this.cancellationTokenSource.Dispose();
            this.cancellationTokenSource = null;

            this.webSocket.Dispose();
        }

        private async ETTask ConnectAsync(string url)
        {
            url = "ws://192.168.2.18:8080";
            Log.Warning($"wchannel connect async {url} {this.isConnected} {this.webSocket.GetType()} ");
            try
            {
                Uri uri = new Uri(url);

                Log.Warning($"connect async {uri}");

                // ClientWebSocket ws = new ClientWebSocket();

                // ws.ConnectAsync();

                // await ws.ConnectAsync(uri, this.cancellationTokenSource.Token);
                await ((ClientWebSocket)this.webSocket).ConnectAsync(uri, cancellationTokenSource.Token);

                isConnected = true;

                Log.Warning($"connect async completed {this.isConnected}");

                this.StartRecv().Coroutine();

                this.StartSend().Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e);
                this.OnError(ErrorCore.ERR_WebsocketConnectError);
            }
        }

        public void Send(MemoryBuffer memoryBuffer)
        {
            Log.Warning($"wchannel send {memoryBuffer.Length}");

            this.queue.Enqueue(memoryBuffer);

            Log.Warning($"wchannel send {this.queue.Count} {this.isConnected}");

            if (this.isConnected)
            {
                this.StartSend().Coroutine();
            }
        }

        private async ETTask StartSend()
        {
            if (this.IsDisposed)
            {
                return;
            }

            try
            {
                if (this.isSending)
                {
                    return;
                }

                this.isSending = true;

                Log.Warning("start send ");
                while (true)
                {
                    Log.Warning($"start send  {this.queue.Count}");

                    if (this.queue.Count == 0)
                    {
                        this.isSending = false;
                        return;
                    }

                    Log.Warning($"start send  {this.queue.Count}");

                    MemoryBuffer stream = this.queue.Dequeue();

                    try
                    {
                        Log.Warning($"this.webSocket.SendAsync");

                        byte[] buffer = new byte[stream.Length];

                        int bytesRead = stream.Read(buffer);

                        Log.Debug($"bytes first {buffer[0]} {stream.Length}, {stream.GetMemory().Length}");

                        ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer, 0, bytesRead);

                        foreach (var value in arraySegment)
                        {
                            Log.Debug($"value {value}");
                        }
                        
                        await this.webSocket.SendAsync(arraySegment, WebSocketMessageType.Binary, true,
                            CancellationToken.None);

                        // await this.webSocket.SendAsync(stream.GetMemory(), WebSocketMessageType.Binary, true, cancellationTokenSource.Token);

                        Log.Warning($"this.webSocket.SendAsync over");

                        this.Service.Recycle(stream);

                        if (this.IsDisposed)
                        {
                            return;
                        }
                    }
                    catch (TaskCanceledException e)
                    {
                        Log.Warning(e.ToString());
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                        this.OnError(ErrorCore.ERR_WebsocketSendError);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private readonly byte[] cache = new byte[ushort.MaxValue];

        public async ETTask StartRecv()
        {
            if (this.IsDisposed)
            {
                return;
            }

            try
            {
                while (true)
                {
                    ValueWebSocketReceiveResult receiveResult;
                    int receiveCount = 0;
                    do
                    {
                        receiveResult = await this.webSocket.ReceiveAsync(new Memory<byte>(cache, receiveCount, this.cache.Length - receiveCount),
                            cancellationTokenSource.Token);

                        if (this.IsDisposed)
                        {
                            return;
                        }

                        receiveCount += receiveResult.Count;
                    }
                    while (!receiveResult.EndOfMessage);

                    Log.Debug($"receive count {receiveCount} {this.cache.Length}");

                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        this.OnError(ErrorCore.ERR_WebsocketPeerReset);
                        return;
                    }

                    if (receiveResult.Count > ushort.MaxValue)
                    {
                        await this.webSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig, $"message too big: {receiveCount}",
                            cancellationTokenSource.Token);
                        this.OnError(ErrorCore.ERR_WebsocketMessageTooBig);
                        return;
                    }

                    MemoryBuffer memoryBuffer = this.Service.Fetch(receiveCount);
                    memoryBuffer.SetLength(receiveCount);
                    memoryBuffer.Seek(0, SeekOrigin.Begin);
                    Array.Copy(this.cache, 0, memoryBuffer.GetBuffer(), 0, receiveCount);

                    Log.Debug($"memoty buffer length {memoryBuffer.Length}");
                    this.OnRead(memoryBuffer);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                this.OnError(ErrorCore.ERR_WebsocketRecvError);
            }
        }

        private void OnRead(MemoryBuffer memoryStream)
        {
            Log.Warning($"on read {memoryStream.Length} {this.Service}");

            Log.Warning($"read call back {this.Service.ReadCallback}");
            try
            {
                this.Service.ReadCallback(this.Id, memoryStream);
            }
            catch (Exception e)
            {
                Log.Error(e);
                this.OnError(ErrorCore.ERR_PacketParserError);
            }
        }

        private void OnError(int error)
        {
            Log.Info($"WChannel error: {error} {this.RemoteAddress}");

            long channelId = this.Id;

            this.Service.Remove(channelId);

            this.Service.ErrorCallback(channelId, error);
        }
    }
}