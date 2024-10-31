/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIAddExpTextItemCellViewComponent: Entity, IAwake
    {
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
        public Transition  ShowAnim
        {
            get
            {
                if (this._ShowAnim == null)
                {
                    this._ShowAnim = this.GetParent<UIBaseWindow>().GComponent.GetTransition("ShowAnim");
                }
                return this._ShowAnim;
            }
        }
        private GTextField _Label = null;
        private Transition _ShowAnim = null;
        public void ClearBindCache()
        {
            this._Label = null;
            this._ShowAnim = null;
        }
    }
}