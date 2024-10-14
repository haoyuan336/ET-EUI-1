/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIAddHeroItemCellViewComponent: Entity, IAwake
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
        public GTextField  Label
        {
            get
            {
                if (this._Label == null)
                {
                    this._Label = this.GetParent<UIBaseWindow>().GComponent.GetChild("Label").asTextField;
                }
                return this._Label;
            }
        }
        private GButton _ClickButton = null;
        private GTextField _Label = null;
        public void ClearBindCache()
        {
            this._ClickButton = null;
            this._Label = null;
        }
    }
}