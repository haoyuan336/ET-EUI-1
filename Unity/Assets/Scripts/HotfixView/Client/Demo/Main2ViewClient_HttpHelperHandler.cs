using UnityEngine.Networking;

namespace ET.Client
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2ViewClient_HttpHelperHandler : MessageHandler<Scene, Main2ViewClient_HttpHelper, View2MainClient_HttpHelper>
    {
        protected override async ETTask Run(Scene unit, Main2ViewClient_HttpHelper request, View2MainClient_HttpHelper response)
        {
            Log.Warning($"main2view client request {request.Url}");
            string url = request.Url;

            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
            {
                UnityWebRequestAsyncOperation asyncOperation = unityWebRequest.SendWebRequest();

                ETTask etTask = ETTask.Create();
                
                asyncOperation.completed += (result) =>
                {
                    Log.Debug("async operation completed");
                    etTask.SetResult();
                };

                await etTask.GetAwaiter();

                Log.Debug($"main 2 view client httphelper {unityWebRequest.downloadHandler.text}");
                response.Text = unityWebRequest.downloadHandler.text;
            }
        }
    }
}