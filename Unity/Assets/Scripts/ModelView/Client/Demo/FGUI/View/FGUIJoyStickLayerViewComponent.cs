/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIJoyStickLayerViewComponent: Entity, IAwake
    {
        public Controller  JostickShowState
        {
            get
            {
                if (this._JostickShowState == null)
                {
                    this._JostickShowState = this.GetParent<UIBaseWindow>().GComponent.GetController("JostickShowState");
                }
                return this._JostickShowState;
            }
        }
        public GGraph  BgNode
        {
            get
            {
                if (this._BgNode == null)
                {
                    this._BgNode = this.GetParent<UIBaseWindow>().GComponent.GetChild("BgNode").asGraph;
                }
                return this._BgNode;
            }
        }
        public GGraph  JoyStickNode
        {
            get
            {
                if (this._JoyStickNode == null)
                {
                    this._JoyStickNode = this.GetParent<UIBaseWindow>().GComponent.GetChild("JoyStickNode").asGraph;
                }
                return this._JoyStickNode;
            }
        }
        private Controller _JostickShowState = null;
        private GGraph _BgNode = null;
        private GGraph _JoyStickNode = null;
        public void ClearBindCache()
        {
            this._JostickShowState = null;
            this._BgNode = null;
            this._JoyStickNode = null;
        }
    }
}