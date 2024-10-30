/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIHeroWordBarItemCellViewComponent: Entity, IAwake
    {
        public GLoader  IconLoader
        {
            get
            {
                if (this._IconLoader == null)
                {
                    this._IconLoader = this.GetParent<UIBaseWindow>().GComponent.GetChild("IconLoader").asLoader;
                }
                return this._IconLoader;
            }
        }
        public GTextField  CurrentValue
        {
            get
            {
                if (this._CurrentValue == null)
                {
                    this._CurrentValue = this.GetParent<UIBaseWindow>().GComponent.GetChild("CurrentValue").asTextField;
                }
                return this._CurrentValue;
            }
        }
        public GTextField  NextAddValue
        {
            get
            {
                if (this._NextAddValue == null)
                {
                    this._NextAddValue = this.GetParent<UIBaseWindow>().GComponent.GetChild("NextAddValue").asTextField;
                }
                return this._NextAddValue;
            }
        }
        private GLoader _IconLoader = null;
        private GTextField _CurrentValue = null;
        private GTextField _NextAddValue = null;
        public void ClearBindCache()
        {
            this._IconLoader = null;
            this._CurrentValue = null;
            this._NextAddValue = null;
        }
    }
}