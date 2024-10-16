/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIFormationLayerViewComponent: Entity, IAwake
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
        public GList  FormationList
        {
            get
            {
                if (this._FormationList == null)
                {
                    this._FormationList = this.GetParent<UIBaseWindow>().GComponent.GetChild("FormationList").asList;
                }
                return this._FormationList;
            }
        }
        public GList  CardList
        {
            get
            {
                if (this._CardList == null)
                {
                    this._CardList = this.GetParent<UIBaseWindow>().GComponent.GetChild("CardList").asList;
                }
                return this._CardList;
            }
        }
        private GButton _CloseButton = null;
        private GList _FormationList = null;
        private GList _CardList = null;
        public void ClearBindCache()
        {
            this._CloseButton = null;
            this._FormationList = null;
            this._CardList = null;
        }
    }
}