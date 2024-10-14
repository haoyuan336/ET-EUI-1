using System.Collections.Generic;
using FairyGUI;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class UIEventComponent : Entity,IAwake,IDestroy
    {
      
        public readonly Dictionary<WindowID, IAUIEventHandler> UIEventHandlers = new Dictionary<WindowID, IAUIEventHandler>();
        public bool IsClicked { get; set; }
        
        public Dictionary<GButton, bool> IsClickedMap = new Dictionary<GButton, bool>();

    }
}