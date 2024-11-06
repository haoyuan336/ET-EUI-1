/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUITeleportLayerViewComponent: Entity, IAwake
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
        public GList  ChapterList
        {
            get
            {
                if (this._ChapterList == null)
                {
                    this._ChapterList = this.GetParent<UIBaseWindow>().GComponent.GetChild("ChapterList").asList;
                }
                return this._ChapterList;
            }
        }
        public GList  MapList
        {
            get
            {
                if (this._MapList == null)
                {
                    this._MapList = this.GetParent<UIBaseWindow>().GComponent.GetChild("MapList").asList;
                }
                return this._MapList;
            }
        }
        private GButton _BgButton = null;
        private GButton _CloseButton = null;
        private GList _ChapterList = null;
        private GList _MapList = null;
        public void ClearBindCache()
        {
            this._BgButton = null;
            this._CloseButton = null;
            this._ChapterList = null;
            this._MapList = null;
        }
    }
}