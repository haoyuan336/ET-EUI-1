// using System;
// using System.IO;
// using System.Net.Http;
// using System.Threading;
//
// namespace ET.Client
// {
//     [MessageHandler(SceneType.Robot)]
//     public class Main2ViewClient_HttpHelperHandler: MessageHandler<Scene, Main2ViewClient_HttpHelper, View2MainClient_HttpHelper>
//     {
//         protected override async ETTask Run(Scene unit, Main2ViewClient_HttpHelper request, View2MainClient_HttpHelper response)
//         {
//             try
//             {
//                 using HttpClient httpClient = new();
//
//                 HttpResponseMessage responseMessage = await httpClient.GetAsync(request.Url);
//
//                 string result = await responseMessage.Content.ReadAsStringAsync();
//
//                 response.Text = result;
//             }
//             catch (Exception e)
//             {
//                 throw new Exception($"http request fail: {request.Url.Substring(0, request.Url.IndexOf('?'))}\n{e}");
//             }
//         }
//     }
// }