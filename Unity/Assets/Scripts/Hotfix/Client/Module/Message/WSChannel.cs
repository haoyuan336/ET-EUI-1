using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class WSChannel : AChannel
    {
        public HttpListenerWebSocketContext WebSocketContext { get; }

        private readonly WSService Service;

        private readonly UnityWebSocket.WebSocket webSocket;

        private readonly Queue<MemoryBuffer> queue = new();

        private bool isSending;

        private bool isConnected;

        private CancellationTokenSource cancellationTokenSource = new();

        // public WSChannel(long id, HttpListenerWebSocketContext webSocketContext, WService service)
        // {
        //     Log.Warning("WChannel HttpListenerWebSocketContext ");
        //     this.Service = service;
        //     this.Id = id;
        //     this.ChannelType = ChannelType.Accept;
        //     this.WebSocketContext = webSocketContext;
        //     this.webSocket = webSocketContext.WebSocket;
        //
        //     isConnected = true;
        //
        //     this.Service.ThreadSynchronizationContext.Post(() =>
        //     {
        //         
        //         this.StartRecv().Coroutine();
        //         this.StartSend().Coroutine();
        //     });
        // }

        public WSChannel(long id, UnityWebSocket.WebSocket webSocket, IPEndPoint ipEndPoint, WSService service)
        {
            Log.Warning("WChannel is new gouzao");
            this.Service = service;
            this.Id = id;
            this.ChannelType = ChannelType.Connect;
            this.webSocket = webSocket;

            this.webSocket.OnMessage += this.OnMessage;

            isConnected = false;
            this.Service.ThreadSynchronizationContext.Post(() =>
            {
                Log.Warning("ThreadSynchronizationContext posy");
                // this.ConnectAsync($"ws://{ipEndPoint}").Coroutine();
                this.ConnectAsync();
            });
        }

        public void OnMessage(object sender, UnityWebSocket.MessageEventArgs messageEventArgs)
        {
            Log.Warning($"on message {messageEventArgs.RawData} {messageEventArgs.RawData.Length} {messageEventArgs.IsBinary} ");

            foreach (var value in messageEventArgs.RawData)
            {
                Log.Warning($"on message {value}");
            }
            ValueWebSocketReceiveResult receiveResult;
            int receiveCount = messageEventArgs.RawData.Length;
            if (messageEventArgs.IsBinary)
            {
                // messageEventArgs.Data;
                // Array.Copy(this.cache, 0, messageEventArgs.RawData, 0, receiveCount);
                Array.Copy(messageEventArgs.RawData, 0, this.cache, 0, receiveCount);
            }

            Log.Warning($"receive count {receiveCount} {this.cache.Length}");

            for (int i = 0; i < receiveCount; i++)
            {
                byte value = this.cache[i];
                
                Log.Warning($"value {value}");
            }

            foreach (var value in messageEventArgs.RawData)
            {
                Log.Warning($"value {value}");
            }
            
            MemoryBuffer memoryBuffer = this.Service.Fetch(receiveCount);
            memoryBuffer.SetLength(receiveCount);
            memoryBuffer.Seek(0, SeekOrigin.Begin);
            Array.Copy(this.cache, 0, memoryBuffer.GetBuffer(), 0, receiveCount);
            this.OnRead(memoryBuffer);
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

            this.webSocket.CloseAsync();
        }

        private void ConnectAsync()
        {
            try
            {
                this.webSocket.ConnectAsync();

                isConnected = true;

                Log.Warning($"connect async completed {this.isConnected}");

                // this.StartRecv().Coroutine();

                this.StartSend();
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
                this.StartSend();
            }
        }

        private void StartSend()
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
                        Log.Debug("send over");
                        return;
                    }

                    Log.Warning($"start send  {this.queue.Count}");

                    MemoryBuffer stream = this.queue.Dequeue();

                    try
                    {
                        Log.Warning($"this.webSocket.SendAsync");

                        // await this.webSocket.SendAsync(stream.GetMemory(), WebSocketMessageType.Binary, true, cancellationTokenSource.Token);
                        byte[] buff = new byte[stream.Length];

                        int read = stream.Read(buff);

                        Log.Debug($"straam count {stream.GetBuffer().Length} buff length {buff.Length} read {read}");
                        // stream.GetMemory().;
                        this.webSocket.SendAsync(buff);

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

        // public async ETTask StartRecv()
        // {
        //     if (this.IsDisposed)
        //     {
        //         return;
        //     }
        //
        //     try
        //     {
        //         while (true)
        //         {
        //             ValueWebSocketReceiveResult receiveResult;
        //             int receiveCount = 0;
        //             do
        //             {
        //                 receiveResult = await this.webSocket.ReceiveAsync(new Memory<byte>(cache, receiveCount, this.cache.Length - receiveCount),
        //                     cancellationTokenSource.Token);
        //                 if (this.IsDisposed)
        //                 {
        //                     return;
        //                 }
        //
        //                 receiveCount += receiveResult.Count;
        //             }
        //             while (!receiveResult.EndOfMessage);
        //
        //             if (receiveResult.MessageType == WebSocketMessageType.Close)
        //             {
        //                 this.OnError(ErrorCore.ERR_WebsocketPeerReset);
        //                 return;
        //             }
        //
        //             if (receiveResult.Count > ushort.MaxValue)
        //             {
        //                 this.webSocket.CloseAsync();
        //                 this.OnError(ErrorCore.ERR_WebsocketMessageTooBig);
        //
        //                 // await this.webSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig, $"message too big: {receiveCount}",
        //                 //     cancellationTokenSource.Token);
        //                 // this.OnError(ErrorCore.ERR_WebsocketMessageTooBig);
        //                 return;
        //             }
        //
        //             MemoryBuffer memoryBuffer = this.Service.Fetch(receiveCount);
        //             memoryBuffer.SetLength(receiveCount);
        //             memoryBuffer.Seek(0, SeekOrigin.Begin);
        //             Array.Copy(this.cache, 0, memoryBuffer.GetBuffer(), 0, receiveCount);
        //             this.OnRead(memoryBuffer);
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         Log.Error(e);
        //         this.OnError(ErrorCore.ERR_WebsocketRecvError);
        //     }
        // }

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