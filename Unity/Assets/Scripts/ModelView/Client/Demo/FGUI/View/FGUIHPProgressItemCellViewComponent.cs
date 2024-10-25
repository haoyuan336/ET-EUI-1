/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIHPProgressItemCellViewComponent: Entity, IAwake
    {
        public GProgressBar  Progress
        {
            get
            {
                if (this._Progress == null)
                {
                    this._Progress = this.GetParent<UIBaseWindow>().GComponent.GetChild("Progress").asProgress;
                }
                return this._Progress;
            }
        }
        private GProgressBar _Progress = null;
        public void ClearBindCache()
        {
            this._Progress = null;
        }
    }
}