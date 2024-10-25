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
        public  FGUIHPProgressItemCellComponent HpBar0Component
        {
            get
            {
                if (this._HpBar0Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar0").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar0Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar0Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar1Component
        {
            get
            {
                if (this._HpBar1Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar1").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar1Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar1Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar2Component
        {
            get
            {
                if (this._HpBar2Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar2").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar2Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar2Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar3Component
        {
            get
            {
                if (this._HpBar3Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar3").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar3Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar3Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar4Component
        {
            get
            {
                if (this._HpBar4Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar4").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar4Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar4Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar5Component
        {
            get
            {
                if (this._HpBar5Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar5").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar5Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar5Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar6Component
        {
            get
            {
                if (this._HpBar6Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar6").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar6Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar6Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar7Component
        {
            get
            {
                if (this._HpBar7Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar7").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar7Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar7Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar8Component
        {
            get
            {
                if (this._HpBar8Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar8").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar8Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar8Component;
            }
        }
        public  FGUIHPProgressItemCellComponent HpBar9Component
        {
            get
            {
                if (this._HpBar9Component== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HPProgressItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HpBar9").asCom;

                    childBaseWindow.AddComponent<FGUIHPProgressItemCellViewComponent>();

                    this._HpBar9Component = childBaseWindow.AddComponent<FGUIHPProgressItemCellComponent>();


                }
                return this._HpBar9Component;
            }
        }
        private FGUIJoyStickLayerComponent _JoyStickLayerComponent = null;
        private GButton _HeroBagButton = null;
        private GList _List = null;
        private GButton _FormationButton = null;
        private FGUIHPProgressItemCellComponent _HpBar0Component = null;
        private FGUIHPProgressItemCellComponent _HpBar1Component = null;
        private FGUIHPProgressItemCellComponent _HpBar2Component = null;
        private FGUIHPProgressItemCellComponent _HpBar3Component = null;
        private FGUIHPProgressItemCellComponent _HpBar4Component = null;
        private FGUIHPProgressItemCellComponent _HpBar5Component = null;
        private FGUIHPProgressItemCellComponent _HpBar6Component = null;
        private FGUIHPProgressItemCellComponent _HpBar7Component = null;
        private FGUIHPProgressItemCellComponent _HpBar8Component = null;
        private FGUIHPProgressItemCellComponent _HpBar9Component = null;
        public void ClearBindCache()
        {
            this._JoyStickLayerComponent = null;
            this._HeroBagButton = null;
            this._List = null;
            this._FormationButton = null;
            this._HpBar0Component = null;
            this._HpBar1Component = null;
            this._HpBar2Component = null;
            this._HpBar3Component = null;
            this._HpBar4Component = null;
            this._HpBar5Component = null;
            this._HpBar6Component = null;
            this._HpBar7Component = null;
            this._HpBar8Component = null;
            this._HpBar9Component = null;
        }
    }
}