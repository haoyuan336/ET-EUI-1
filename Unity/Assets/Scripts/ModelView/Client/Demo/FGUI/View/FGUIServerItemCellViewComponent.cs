/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIServerItemCellViewComponent: Entity, IAwake
    {
        public GTextField  ServerName
        {
            get
            {
                if (this._ServerName == null)
                {
                    this._ServerName = this.GetParent<UIBaseWindow>().GComponent.GetChild("ServerName").asTextField;
                }
                return this._ServerName;
            }
        }
        public GButton  ClickButton
        {
            get
            {
                if (this._ClickButton == null)
                {
                    this._ClickButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("ClickButton").asButton;
                }
                return this._ClickButton;
            }
        }
        private GTextField _ServerName = null;
        private GButton _ClickButton = null;
        public void ClearBindCache()
        {
            this._ServerName = null;
            this._ClickButton = null;
        }
    }
}