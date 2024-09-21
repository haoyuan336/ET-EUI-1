using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using UnityEngine.Networking;

namespace ET.Client
{
    public static partial class HttpClientHelper
    {
        // public static async ETTask<string> Get(Fiber fiber, string link)
        // {
        //     try
        //     {
        //         using HttpClient httpClient = new();
        //         HttpResponseMessage response =  await httpClient.GetAsync(link);
        //         string result = await response.Content.ReadAsStringAsync();
        //         return result;
        //     }
        //     catch (Exception e)
        //     {
        //         throw new Exception($"http request fail: {link.Substring(0,link.IndexOf('?'))}\n{e}");
        //     }
        // }

        public static async ETTask<string> Get(Fiber fiber, string link)
        {
            Main2ViewClient_HttpHelper request = Main2ViewClient_HttpHelper.Create();
        
            request.Url = link;
        
            View2MainClient_HttpHelper response =
                    await fiber.Root.GetComponent<ProcessInnerSender>().Call(new ActorId(fiber.Process, fiber.Id), request) as
                            View2MainClient_HttpHelper;
        
            Log.Debug($"response text {response.Text}");
            return response.Text;
        }
        
        // public static async ETTask<string> Get(Fiber fiber, string link)
        // {
        //
        //     Log.Debug($"http client helper {link}");
        //     using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(link))
        //     {
        //         UnityWebRequestAsyncOperation asyncOperation = unityWebRequest.SendWebRequest();
        //
        //         ETTask etTask = ETTask.Create();
        //
        //         asyncOperation.completed += (result) =>
        //         {
        //             Log.Debug("async operation completed");
        //             etTask.SetResult();
        //         };
        //
        //         await etTask.GetAwaiter();
        //
        //         Log.Debug($"main 2 view client httphelper {unityWebRequest.downloadHandler.text}");
        //
        //         return unityWebRequest.downloadHandler.text;
        //     }
        // }
    }
}