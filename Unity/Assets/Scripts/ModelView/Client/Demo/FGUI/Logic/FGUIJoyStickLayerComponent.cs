/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(UIBaseWindow))]
    [ChildOf(typeof(UIBaseWindow))]
    public class FGUIJoyStickLayerComponent : Entity, IAwake, IUILogic
    {
        public FGUIJoyStickLayerViewComponent View => this.GetParent<UIBaseWindow>().GetComponent<FGUIJoyStickLayerViewComponent>();

        public GComponent GComponent = null;

        public Action<Vector2, float> JoyAction = null;

        public Action StartJoyAction = null;

        public Action EndJoyAction = null;

        public Vector2 Direction = Vector2.one;
    }
}