using System;
using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class AppStartInitFinishEventHandler : AEvent<Scene, AppStartInitFinish>
    {
        protected override async ETTask Run(Scene scene, AppStartInitFinish a)
        {
            Log.Debug("app start init finish");

            UnitConfig unitConfig = UnitConfigCategory.Instance.Get(1001);

            Log.Debug($"unit config name {unitConfig.Name}");

            // UIPackage.AddPackage("FGUI/RootPackage/RootPackage");
            //
            // GlobalComponent globalComponent = scene.GetComponent<GlobalComponent>();
            //
            // UIPanel uiPanel = globalComponent.UIPanel.GetComponent<UIPanel>();
            //
            // GComponent gComponent = uiPanel.ui;
            //
            // gComponent.Dispose();
            //
            // uiPanel.packageName = "RootPackage";
            // //
            // uiPanel.componentName = "RootLayer";

            // EventSystem.Instance.Publish(scene, new ShowLoginLayer());

            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            uiComponent.ShowWindow(WindowID.LoginLayer);

            await ETTask.CompletedTask;
        }
    }
}