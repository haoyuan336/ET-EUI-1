/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIFormationCardItemCellViewComponent: Entity, IAwake
    {
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
        private GTextField _HeroName = null;
        private GTextField _Level = null;
        public void ClearBindCache()
        {
            this._HeroName = null;
            this._Level = null;
        }
    }
}