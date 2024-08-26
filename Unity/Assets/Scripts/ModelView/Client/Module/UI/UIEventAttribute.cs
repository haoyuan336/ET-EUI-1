using System;

namespace ET.Client
{
    public class UIEventAttribute: BaseAttribute
    {
        public WindowID UIType { get; }

        public UIEventAttribute(WindowID uiType)
        {
            this.UIType = uiType;
        }
    }
}