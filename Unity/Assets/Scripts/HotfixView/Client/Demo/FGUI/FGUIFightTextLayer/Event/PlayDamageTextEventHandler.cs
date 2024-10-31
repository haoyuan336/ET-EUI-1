using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class PlayDamageTextEventHandler : AEvent<Scene, PlayDamageText>
    {
        protected override async ETTask Run(Scene scene, PlayDamageText a)
        {
            UIComponent uiComponent = scene.GetComponent<UIComponent>();

            FGUIFightTextLayerComponent fguiFightTextLayerComponent = uiComponent.GetDlgLogic<FGUIFightTextLayerComponent>();
        
            fguiFightTextLayerComponent.PlayDamageText(a.StartPos, a.Text);

            await ETTask.CompletedTask;
        }
    }
}