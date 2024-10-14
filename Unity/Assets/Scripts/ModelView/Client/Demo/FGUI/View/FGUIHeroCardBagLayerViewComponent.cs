/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIHeroCardBagLayerViewComponent: Entity, IAwake
    {
        public GButton  CloseButton
        {
            get
            {
                if (this._CloseButton == null)
                {
                    this._CloseButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("CloseButton").asButton;
                }
                return this._CloseButton;
            }
        }
        public GList  HeroCardList
        {
            get
            {
                if (this._HeroCardList == null)
                {
                    this._HeroCardList = this.GetParent<UIBaseWindow>().GComponent.GetChild("HeroCardList").asList;
                }
                return this._HeroCardList;
            }
        }
        private GButton _CloseButton = null;
        private GList _HeroCardList = null;
        public void ClearBindCache()
        {
            this._CloseButton = null;
            this._HeroCardList = null;
        }
    }
}