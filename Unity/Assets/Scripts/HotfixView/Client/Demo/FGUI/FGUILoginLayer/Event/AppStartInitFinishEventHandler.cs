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

            // GRoot.inst.SetContentScaleFactor(1080, 1920, UIContentScaler.ScreenMatchMode.MatchWidth);

            // 注册字体
            FontManager.RegisterFont(new DynamicFont("SIMHEI", Resources.Load<Font>("Fonts/SIMHEI")));
            // 设置为默认字体
            UIConfig.defaultFont = "SIMHEI";

            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            scene.Root().GetComponent<GlobalComponent>().Init();

            uiComponent.ShowWindow(WindowID.LoginLayer);

            await ETTask.CompletedTask;
        }
    }
}