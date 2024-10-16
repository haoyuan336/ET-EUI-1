/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIMainLayerViewComponent: Entity, IAwake
    {
        public  FGUIJoyStickLayerComponent JoyStickLayerComponent
        {
            get
            {
                if (this._JoyStickLayerComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.JoyStickLayer;

                    childBaseWindow.GComponent = gComponent.GetChild("JoyStickLayer").asCom;

                    childBaseWindow.AddComponent<FGUIJoyStickLayerViewComponent>();

                    this._JoyStickLayerComponent = childBaseWindow.AddComponent<FGUIJoyStickLayerComponent>();


                }
                return this._JoyStickLayerComponent;
            }
        }
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
        public GButton  FormationButton
        {
            get
            {
                if (this._FormationButton == null)
                {
                    this._FormationButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("FormationButton").asButton;
                }
                return this._FormationButton;
            }
        }
        private FGUIJoyStickLayerComponent _JoyStickLayerComponent = null;
        private GButton _HeroBagButton = null;
        private GList _List = null;
        private GButton _FormationButton = null;
        public void ClearBindCache()
        {
            this._JoyStickLayerComponent = null;
            this._HeroBagButton = null;
            this._List = null;
            this._FormationButton = null;
        }
    }
}