/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIMainLayerViewComponent: Entity, IAwake
    {
        public GButton  HeroBagButton
        {
            get
            {
                if (this._HeroBagButton == null)
                {
                    this._HeroBagButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("HeroBagButton").asButton;
                }
                return this._HeroBagButton;
            }
        }
        public GList  List
        {
            get
            {
                if (this._List == null)
                {
                    this._List = this.GetParent<UIBaseWindow>().GComponent.GetChild("List").asList;
                }
                return this._List;
            }
        }
        private GButton _HeroBagButton = null;
        private GList _List = null;
        public void ClearBindCache()
        {
            this._HeroBagButton = null;
            this._List = null;
        }
    }
}