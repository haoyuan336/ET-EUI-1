/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUITipsIconItemCellViewComponent: Entity, IAwake
    {
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
        public Transition  EnterAnim
        {
            get
            {
                if (this._EnterAnim == null)
                {
                    this._EnterAnim = this.GetParent<UIBaseWindow>().GComponent.GetTransition("EnterAnim");
                }
                return this._EnterAnim;
            }
        }
        public Transition  ExitAnim
        {
            get
            {
                if (this._ExitAnim == null)
                {
                    this._ExitAnim = this.GetParent<UIBaseWindow>().GComponent.GetTransition("ExitAnim");
                }
                return this._ExitAnim;
            }
        }
        private GButton _ClickButton = null;
        private Transition _EnterAnim = null;
        private Transition _ExitAnim = null;
        public void ClearBindCache()
        {
            this._ClickButton = null;
            this._EnterAnim = null;
            this._ExitAnim = null;
        }
    }
}