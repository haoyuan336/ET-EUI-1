/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIGameLoseLayerViewComponent: Entity, IAwake
    {
        public GButton  BackMainButton
        {
            get
            {
                if (this._BackMainButton == null)
                {
                    this._BackMainButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("BackMainButton").asButton;
                }
                return this._BackMainButton;
            }
        }
        public GButton  RetryButton
        {
            get
            {
                if (this._RetryButton == null)
                {
                    this._RetryButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("RetryButton").asButton;
                }
                return this._RetryButton;
            }
        }
        private GButton _BackMainButton = null;
        private GButton _RetryButton = null;
        public void ClearBindCache()
        {
            this._BackMainButton = null;
            this._RetryButton = null;
        }
    }
}