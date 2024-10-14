/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUILoginServerLayerViewComponent: Entity, IAwake
    {
        public  FGUIServerItemCellComponent CurrentServerComponent
        {
            get
            {
                if (this._CurrentServerComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.ServerItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("CurrentServer").asCom;

                    childBaseWindow.AddComponent<FGUIServerItemCellViewComponent>();

                    this._CurrentServerComponent = childBaseWindow.AddComponent<FGUIServerItemCellComponent>();


                }
                return this._CurrentServerComponent;
            }
        }
        public GButton  LoginButton
        {
            get
            {
                if (this._LoginButton == null)
                {
                    this._LoginButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("LoginButton").asButton;
                }
                return this._LoginButton;
            }
        }
        private FGUIServerItemCellComponent _CurrentServerComponent = null;
        private GButton _LoginButton = null;
        public void ClearBindCache()
        {
            this._CurrentServerComponent = null;
            this._LoginButton = null;
        }
    }
}