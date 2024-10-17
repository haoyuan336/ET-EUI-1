using System;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using CommandLine;
using UnityEngine;
using UnityWebSocket;

namespace ET
{
    public class Init : MonoBehaviour
    {
        private void Start()
        {
            this.StartAsync().Coroutine();

            Application.targetFrameRate = 59;
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

            Assembly assembly = Assembly.GetExecutingAssembly();
            
            World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[] { assembly });
            
            ET.Entry.Start();
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