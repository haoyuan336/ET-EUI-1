/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIHeadItemBarLayerViewComponent: Entity, IAwake
    {
        public  FGUIItemBarItemCellComponent DiamondItemComponent
        {
            get
            {
                if (this._DiamondItemComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.ItemBarItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("DiamondItem").asCom;

                    childBaseWindow.AddComponent<FGUIItemBarItemCellViewComponent>();

                    this._DiamondItemComponent = childBaseWindow.AddComponent<FGUIItemBarItemCellComponent>();


                }
                return this._DiamondItemComponent;
            }
        }
        public  FGUIItemBarItemCellComponent GoldItemComponent
        {
            get
            {
                if (this._GoldItemComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.ItemBarItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("GoldItem").asCom;

                    childBaseWindow.AddComponent<FGUIItemBarItemCellViewComponent>();

                    this._GoldItemComponent = childBaseWindow.AddComponent<FGUIItemBarItemCellComponent>();


                }
                return this._GoldItemComponent;
            }
        }
        public  FGUIItemBarItemCellComponent MeatItemComponent
        {
            get
            {
                if (this._MeatItemComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.ItemBarItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("MeatItem").asCom;

                    childBaseWindow.AddComponent<FGUIItemBarItemCellViewComponent>();

                    this._MeatItemComponent = childBaseWindow.AddComponent<FGUIItemBarItemCellComponent>();


                }
                return this._MeatItemComponent;
            }
        }
        public  FGUIItemBarItemCellComponent WoodItemComponent
        {
            get
            {
                if (this._WoodItemComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.ItemBarItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("WoodItem").asCom;

                    childBaseWindow.AddComponent<FGUIItemBarItemCellViewComponent>();

                    this._WoodItemComponent = childBaseWindow.AddComponent<FGUIItemBarItemCellComponent>();


                }
                return this._WoodItemComponent;
            }
        }
        public  FGUIItemBarItemCellComponent BlueStoneItemComponent
        {
            get
            {
                if (this._BlueStoneItemComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.ItemBarItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("BlueStoneItem").asCom;

                    childBaseWindow.AddComponent<FGUIItemBarItemCellViewComponent>();

                    this._BlueStoneItemComponent = childBaseWindow.AddComponent<FGUIItemBarItemCellComponent>();


                }
                return this._BlueStoneItemComponent;
            }
        }
        private FGUIItemBarItemCellComponent _DiamondItemComponent = null;
        private FGUIItemBarItemCellComponent _GoldItemComponent = null;
        private FGUIItemBarItemCellComponent _MeatItemComponent = null;
        private FGUIItemBarItemCellComponent _WoodItemComponent = null;
        private FGUIItemBarItemCellComponent _BlueStoneItemComponent = null;
        public void ClearBindCache()
        {
            this._DiamondItemComponent = null;
            this._GoldItemComponent = null;
            this._MeatItemComponent = null;
            this._WoodItemComponent = null;
            this._BlueStoneItemComponent = null;
        }
    }
}