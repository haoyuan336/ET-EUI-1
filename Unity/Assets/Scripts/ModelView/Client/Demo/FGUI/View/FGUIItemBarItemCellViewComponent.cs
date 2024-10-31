/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIItemBarItemCellViewComponent: Entity, IAwake
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
        public GTextField  Title
        {
            get
            {
                if (this._Title == null)
                {
                    this._Title = this.GetParent<UIBaseWindow>().GComponent.GetChild("Title").asTextField;
                }
                return this._Title;
            }
        }
        public GTextField  Value
        {
            get
            {
                if (this._Value == null)
                {
                    this._Value = this.GetParent<UIBaseWindow>().GComponent.GetChild("Value").asTextField;
                }
                return this._Value;
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
        private GProgressBar _Progress = null;
        private GTextField _Title = null;
        private GTextField _Value = null;
        private GButton _ClickButton = null;
        public void ClearBindCache()
        {
            this._Progress = null;
            this._Title = null;
            this._Value = null;
            this._ClickButton = null;
        }
    }
}