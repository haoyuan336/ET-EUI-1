/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUILoginLayerViewComponent: Entity, IAwake
    {
        public GButton  LoginButton
        {
            get
            {
                if (this._LoginButton == null)
                {
                    this._LoginButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("LoginButton").asButton;
                }
                return this._LoginButton;
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
        private GButton _LoginButton = null;
        private GTextInput _Account = null;
        private GTextInput _Password = null;
        public void ClearBindCache()
        {
            this._LoginButton = null;
            this._Account = null;
            this._Password = null;
        }
    }
}