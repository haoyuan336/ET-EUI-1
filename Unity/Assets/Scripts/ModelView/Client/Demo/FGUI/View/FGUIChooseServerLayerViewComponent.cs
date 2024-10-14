/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using ET.Client;
namespace ET
{
    [FriendOf(typeof(ET.Client.UIBaseWindow)), EnableMethod, ComponentOf(typeof(ET.Client.UIBaseWindow))]
    public class FGUIChooseServerLayerViewComponent: Entity, IAwake
    {
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
        private GList _List = null;
        public void ClearBindCache()
        {
            this._List = null;
        }
    }
}