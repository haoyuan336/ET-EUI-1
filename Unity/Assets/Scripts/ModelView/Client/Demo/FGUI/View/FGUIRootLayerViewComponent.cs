/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIRootLayerViewComponent: Entity, IAwake
    {
        public  FGUIEmptyLayerComponent NormalRootLayerComponent
        {
            get
            {
                if (this._NormalRootLayerComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.EmptyLayer;

                    childBaseWindow.GComponent = gComponent.GetChild("NormalRootLayer").asCom;

                    childBaseWindow.AddComponent<FGUIEmptyLayerViewComponent>();

                    this._NormalRootLayerComponent = childBaseWindow.AddComponent<FGUIEmptyLayerComponent>();


                }
                return this._NormalRootLayerComponent;
            }
        }
        public  FGUIEmptyLayerComponent PopUpRootLayerComponent
        {
            get
            {
                if (this._PopUpRootLayerComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.EmptyLayer;

                    childBaseWindow.GComponent = gComponent.GetChild("PopUpRootLayer").asCom;

                    childBaseWindow.AddComponent<FGUIEmptyLayerViewComponent>();

                    this._PopUpRootLayerComponent = childBaseWindow.AddComponent<FGUIEmptyLayerComponent>();


                }
                return this._PopUpRootLayerComponent;
            }
        }
        public  FGUIEmptyLayerComponent FixedRootLayerComponent
        {
            get
            {
                if (this._FixedRootLayerComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.EmptyLayer;

                    childBaseWindow.GComponent = gComponent.GetChild("FixedRootLayer").asCom;

                    childBaseWindow.AddComponent<FGUIEmptyLayerViewComponent>();

                    this._FixedRootLayerComponent = childBaseWindow.AddComponent<FGUIEmptyLayerComponent>();


                }
                return this._FixedRootLayerComponent;
            }
        }
        public  FGUIEmptyLayerComponent OtherRootLayerComponent
        {
            get
            {
                if (this._OtherRootLayerComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.EmptyLayer;

                    childBaseWindow.GComponent = gComponent.GetChild("OtherRootLayer").asCom;

                    childBaseWindow.AddComponent<FGUIEmptyLayerViewComponent>();

                    this._OtherRootLayerComponent = childBaseWindow.AddComponent<FGUIEmptyLayerComponent>();


                }
                return this._OtherRootLayerComponent;
            }
        }
        private FGUIEmptyLayerComponent _NormalRootLayerComponent = null;
        private FGUIEmptyLayerComponent _PopUpRootLayerComponent = null;
        private FGUIEmptyLayerComponent _FixedRootLayerComponent = null;
        private FGUIEmptyLayerComponent _OtherRootLayerComponent = null;
        public void ClearBindCache()
        {
            this._NormalRootLayerComponent = null;
            this._PopUpRootLayerComponent = null;
            this._FixedRootLayerComponent = null;
            this._OtherRootLayerComponent = null;
        }
    }
}