/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class LoginLayerViewComponent: Entity, IAwake
    {
        public  BgLayerComponent BgLayerComponent
        {
            get
            {
                if (this._BgLayerComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.BgLayer;

                    childBaseWindow.GComponent = gComponent.GetChild("BgLayer").asCom;

                    childBaseWindow.AddComponent<BgLayerViewComponent>();

                    this._BgLayerComponent = childBaseWindow.AddComponent<BgLayerComponent>();


                }
                return this._BgLayerComponent;
            }
        }
        public GTextInput  Account
        {
            get
            {
                if (this._Account == null)
                {
                    this._Account = this.GetParent<UIBaseWindow>().GComponent.GetChild("Account").asTextInput;
                }
                return this._Account;
            }
        }
        public GTextInput  Password
        {
            get
            {
                if (this._Password == null)
                {
                    this._Password = this.GetParent<UIBaseWindow>().GComponent.GetChild("Password").asTextInput;
                }
                return this._Password;
            }
        }
        private BgLayerComponent _BgLayerComponent = null;
        private GTextInput _Account = null;
        private GTextInput _Password = null;
        public void ClearBindCache()
        {
            this._BgLayerComponent = null;
            this._Account = null;
            this._Password = null;
        }
    }
}