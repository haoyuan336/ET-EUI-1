/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIFormationCardItemCellViewComponent: Entity, IAwake
    {
        public Controller  IsFormation
        {
            get
            {
                if (this._IsFormation == null)
                {
                    this._IsFormation = this.GetParent<UIBaseWindow>().GComponent.GetController("IsFormation");
                }
                return this._IsFormation;
            }
        }
        public GTextField  HeroName
        {
            get
            {
                if (this._HeroName == null)
                {
                    this._HeroName = this.GetParent<UIBaseWindow>().GComponent.GetChild("HeroName").asTextField;
                }
                return this._HeroName;
            }
        }
        public GTextField  Level
        {
            get
            {
                if (this._Level == null)
                {
                    this._Level = this.GetParent<UIBaseWindow>().GComponent.GetChild("Level").asTextField;
                }
                return this._Level;
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
        private Controller _IsFormation = null;
        private GTextField _HeroName = null;
        private GTextField _Level = null;
        private GButton _ClickButton = null;
        public void ClearBindCache()
        {
            this._IsFormation = null;
            this._HeroName = null;
            this._Level = null;
            this._ClickButton = null;
        }
    }
}