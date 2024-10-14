using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class SceneChangeStart_AddComponent : AEvent<Scene, SceneChangeStart>
    {
        protected override async ETTask Run(Scene root, SceneChangeStart args)
        {
            try
            {
                EventSystem.Instance.Publish(root, new ShowLoadingLayer() { IsShow = true });
       
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}