/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUITeleportMapItemCellViewComponent: Entity, IAwake
    {
        public Controller  IsCurrentMap
        {
            get
            {
                if (this._IsCurrentMap == null)
                {
                    this._IsCurrentMap = this.GetParent<UIBaseWindow>().GComponent.GetController("IsCurrentMap");
                }
                return this._IsCurrentMap;
            }
        }
        public Controller  button
        {
            get
            {
                if (this._button == null)
                {
                    this._button = this.GetParent<UIBaseWindow>().GComponent.GetController("button");
                }
                return this._button;
            }
        }
        public GTextField  MapTitle
        {
            get
            {
                if (this._MapTitle == null)
                {
                    this._MapTitle = this.GetParent<UIBaseWindow>().GComponent.GetChild("MapTitle").asTextField;
                }
                return this._MapTitle;
            }
        }
        public GTextField  FightPower
        {
            get
            {
                if (this._FightPower == null)
                {
                    this._FightPower = this.GetParent<UIBaseWindow>().GComponent.GetChild("FightPower").asTextField;
                }
                return this._FightPower;
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
        private Controller _IsCurrentMap = null;
        private Controller _button = null;
        private GTextField _MapTitle = null;
        private GTextField _FightPower = null;
        private GButton _ClickButton = null;
        public void ClearBindCache()
        {
            this._IsCurrentMap = null;
            this._button = null;
            this._MapTitle = null;
            this._FightPower = null;
            this._ClickButton = null;
        }
    }
}