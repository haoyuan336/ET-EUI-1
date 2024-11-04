/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUICallLayerViewComponent: Entity, IAwake
    {
        public GButton  BgButton
        {
            get
            {
                if (this._BgButton == null)
                {
                    this._BgButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("BgButton").asButton;
                }
                return this._BgButton;
            }
        }
        public GLoader3D  ShowHeroSpine
        {
            get
            {
                if (this._ShowHeroSpine == null)
                {
                    this._ShowHeroSpine = this.GetParent<UIBaseWindow>().GComponent.GetChild("ShowHeroSpine").asLoader3D;
                }
                return this._ShowHeroSpine;
            }
        }
        private GButton _BgButton = null;
        private GLoader3D _ShowHeroSpine = null;
        public void ClearBindCache()
        {
            this._BgButton = null;
            this._ShowHeroSpine = null;
        }
    }
}