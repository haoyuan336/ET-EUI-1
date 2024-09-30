using System;
using System.Net.WebSockets;
using System.Threading;
using CommandLine;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityWebSocket;

namespace ET
{
    public class Init : MonoBehaviour
    {
        private void Start()
        {

            UnityWebSocket.WebSocket ws = new UnityWebSocket.WebSocket("ws://43.159.192.184:30301");
            
            // UnityWebSocket.WebSocket ws = new UnityWebSocket.WebSocket("ws://192.168.2.18:30301");
            
            ws.ConnectAsync();
            
            ws.OnOpen += this.OnOpen;
            
            ws.OnError += this.OnError;
            
            ws.OnMessage += this.OnMessage;
            //

            // ClientWebSocket cws = new ClientWebSocket();
            //
            // CancellationToken cancellationToken = new CancellationToken();
            //
            // cws.ConnectAsync(new Uri("ws://127.0.0.1:30301"),cancellationToken);

            
            
            this.StartAsync().Coroutine();

        }

        public void OnOpen(object sender, OpenEventArgs args)
        {
            Log.Debug($"on open {args.ToString()}");
        }

        public void OnError(object sender, UnityWebSocket.ErrorEventArgs messageEventArgs)
        {
            Log.Debug($"on OnError  {messageEventArgs.Message}");
        }

        public void OnMessage(object sender, UnityWebSocket.MessageEventArgs messageEventArgs)
        {
            Log.Debug($"on message {messageEventArgs.RawData.Length}");
        }

        private void OnError(ErrorEventArgs args)
        {
        }

        private async ETTask StartAsync()
        {
            DontDestroyOnLoad(gameObject);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) => { Log.Error(e.ExceptionObject.ToString()); };

            // 命令行参数
            string[] args = "".Split(" ");
            Parser.Default.ParseArguments<Options>(args)
                    .WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
                    .WithParsed((o) => World.Instance.AddSingleton(o));
            Options.Instance.StartConfig = $"StartConfig/Localhost";

            World.Instance.AddSingleton<Logger>().Log = new UnityLogger();
            ETTask.ExceptionHandler += Log.Error;

            World.Instance.AddSingleton<TimeInfo>();
            World.Instance.AddSingleton<FiberManager>();

            await World.Instance.AddSingleton<ResourcesComponent>().CreatePackageAsync("DefaultPackage", true);

            CodeLoader codeLoader = World.Instance.AddSingleton<CodeLoader>();
            await codeLoader.DownloadAsync();

            codeLoader.Start();
        }

        private void Update()
        {
            TimeInfo.Instance.Update();
            FiberManager.Instance.Update();
        }

        private void LateUpdate()
        {
            FiberManager.Instance.LateUpdate();
        }

        private void OnApplicationQuit()
        {
            World.Instance.Dispose();
        }
    }
}