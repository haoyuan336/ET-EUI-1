using UnityEngine.Networking;

namespace ET.Client
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2ViewClient_HttpHelperHandler : MessageHandler<Scene, Main2ViewClient_HttpHelper, View2MainClient_HttpHelper>
    {
        protected override async ETTask Run(Scene unit, Main2ViewClient_HttpHelper request, View2MainClient_HttpHelper response)
        {
            string url = request.Url;

            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
            {
                UnityWebRequestAsyncOperation asyncOperation = unityWebRequest.SendWebRequest();

                ETTask etTask = ETTask.Create();
                
                asyncOperation.completed += (result) =>
                {
                    etTask.SetResult();
                };

                await etTask.GetAwaiter();

                response.Text = unityWebRequest.downloadHandler.text;
            }
        }
    }
}