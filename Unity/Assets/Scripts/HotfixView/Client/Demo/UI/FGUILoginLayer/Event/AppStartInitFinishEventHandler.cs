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
            // Stage.keyboardInput = true;

            GRoot.inst.SetContentScaleFactor(720, 1280, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
            
            // 注册字体
            FontManager.RegisterFont(new DynamicFont("SIMHEI", Resources.Load<Font>("Fonts/SIMHEI")));
            // 设置为默认字体
            UIConfig.defaultFont = "SIMHEI";

            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            uiComponent.ShowWindow(WindowID.LoginLayer);

            GlobalComponent globalComponent = scene.GetComponent<GlobalComponent>();

            float width = globalComponent.UIPanel.ui.width;

            Log.Debug($"width {width} {globalComponent.NormalRoot.width}");


            await ETTask.CompletedTask;
        }
    }
}