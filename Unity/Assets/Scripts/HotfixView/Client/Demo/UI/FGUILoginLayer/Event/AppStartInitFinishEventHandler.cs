using System;
using FairyGUI;
using UnityEngine;
using YooAsset;

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

            uiComponent.ShowWindow(WindowID.LoginLayer);

            GlobalComponent globalComponent = scene.GetComponent<GlobalComponent>();

            float width = globalComponent.UIPanel.ui.width;

            Log.Debug($"width {width} {globalComponent.NormalRoot.width}");

            AssetHandle assetHandle = YooAssets.LoadAssetAsync<GameObject>("Cube");

            assetHandle.Completed += (result) =>
            {
                GameObject prefab = result.AssetObject as GameObject;

                GameObject go = GameObject.Instantiate(prefab);
            };

            await ETTask.CompletedTask;
        }
    }
}