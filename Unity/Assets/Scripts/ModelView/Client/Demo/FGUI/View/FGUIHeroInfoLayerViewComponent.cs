/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIHeroInfoLayerViewComponent: Entity, IAwake
    {
        public GTextField  HeroName
        {
            get
            {
                if (this._HeroName == null)
                {
                    this._HeroName = this.GetParent<UIBaseWindow>().GComponent.GetChild("HeroName").asTextField;
                }
                return this._HeroName;
            }
        }
        public GLoader3D  HeroLoader
        {
            get
            {
                if (this._HeroLoader == null)
                {
                    this._HeroLoader = this.GetParent<UIBaseWindow>().GComponent.GetChild("HeroLoader").asLoader3D;
                }
                return this._HeroLoader;
            }
        }
        public GTextField  Level
        {
            get
            {
                if (this._Level == null)
                {
                    this._Level = this.GetParent<UIBaseWindow>().GComponent.GetChild("Level").asTextField;
                }
                return this._Level;
            }
        }
        public GButton  UpLevelButton
        {
            get
            {
                if (this._UpLevelButton == null)
                {
                    this._UpLevelButton = this.GetParent<UIBaseWindow>().GComponent.GetChild("UpLevelButton").asButton;
                }
                return this._UpLevelButton;
            }
        }
        public  FGUIHeroWordBarItemCellComponent AttackBarComponent
        {
            get
            {
                if (this._AttackBarComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HeroWordBarItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("AttackBar").asCom;

                    childBaseWindow.AddComponent<FGUIHeroWordBarItemCellViewComponent>();

                    this._AttackBarComponent = childBaseWindow.AddComponent<FGUIHeroWordBarItemCellComponent>();


                }
                return this._AttackBarComponent;
            }
        }
        public  FGUIHeroWordBarItemCellComponent HPBarComponent
        {
            get
            {
                if (this._HPBarComponent== null)
                {
                    UIBaseWindow fguiBaseWindow = this.GetParent<UIBaseWindow>();

                    GComponent gComponent = fguiBaseWindow.GComponent;

                    UIBaseWindow childBaseWindow = this.AddChild<UIBaseWindow>();

                    childBaseWindow.WindowID = WindowID.HeroWordBarItemCell;

                    childBaseWindow.GComponent = gComponent.GetChild("HPBar").asCom;

                    childBaseWindow.AddComponent<FGUIHeroWordBarItemCellViewComponent>();

                    this._HPBarComponent = childBaseWindow.AddComponent<FGUIHeroWordBarItemCellComponent>();


                }
                return this._HPBarComponent;
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
        private GTextField _HeroName = null;
        private GLoader3D _HeroLoader = null;
        private GTextField _Level = null;
        private GButton _UpLevelButton = null;
        private FGUIHeroWordBarItemCellComponent _AttackBarComponent = null;
        private FGUIHeroWordBarItemCellComponent _HPBarComponent = null;
        private GButton _CloseButton = null;
        public void ClearBindCache()
        {
            this._HeroName = null;
            this._HeroLoader = null;
            this._Level = null;
            this._UpLevelButton = null;
            this._AttackBarComponent = null;
            this._HPBarComponent = null;
            this._CloseButton = null;
        }
    }
}