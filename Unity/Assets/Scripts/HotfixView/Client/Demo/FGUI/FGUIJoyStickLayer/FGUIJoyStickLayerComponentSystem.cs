/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(FGUIJoyStickLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIJoyStickLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIJoyStickLayerComponent self)
        {
            Log.Debug("RegisterUIEvent FGUIJoyStickLayerComponent");

            // self.View.BgNode.onTouchBegin.Set(self.OnTouchBegin);
            //
            // self.View.BgNode.onTouchMove.Set(self.OnTouchMove);
            //
            // self.View.BgNode.onTouchEnd.Set(self.OnTouchEnd);

            UIBaseWindow baseWindow = self.GetParent<UIBaseWindow>();

            GComponent goComponent = baseWindow.GComponent;

            goComponent.onTouchBegin.Set(self.OnTouchBegin);

            goComponent.onTouchMove.Set(self.OnTouchMove);

            goComponent.onTouchEnd.Set(self.OnTouchEnd);

            self.GComponent = goComponent;
        }

        private static void OnTouchBegin(this FGUIJoyStickLayerComponent self, EventContext context)
        {
            context.CaptureTouch();

            self.View.JostickShowState.selectedIndex = 1;

            Vector2 localPos = self.GComponent.GlobalToLocal(new Vector2(context.inputEvent.x, context.inputEvent.y));

            self.View.BgNode.position = localPos;

            self.View.JoyStickNode.position = localPos;

            if (self.StartJoyAction != null)
            {
                self.StartJoyAction.Invoke();
            }
        }

        private static void OnTouchMove(this FGUIJoyStickLayerComponent self, EventContext context)
        {
            Vector2 localPos = self.GComponent.GlobalToLocal(new Vector2(context.inputEvent.x, context.inputEvent.y));

            Vector2 bgPos = self.View.BgNode.position;

            float distance = Vector2.Distance(localPos, bgPos);

            Vector2 director = localPos - bgPos;

            if (distance < self.View.BgNode.size.x * 0.5f)
            {
                self.View.JoyStickNode.position = localPos;
            }
            else
            {
                Vector2 endPos = director.normalized * self.View.BgNode.width * 0.5f;

                self.View.JoyStickNode.position = bgPos + endPos;
            }

            if (self.JoyAction != null)
            {
                self.Direction = director.normalized;

                self.JoyAction.Invoke(self.Direction, self.Direction.magnitude);
            }
        }

        private static void OnTouchEnd(this FGUIJoyStickLayerComponent self, EventContext context)
        {
            self.View.JostickShowState.selectedIndex = 0;

            if (self.JoyAction != null)
            {
                self.JoyAction.Invoke(self.Direction, 0);
            }

            if (self.EndJoyAction != null)
            {
                self.EndJoyAction.Invoke();
            }
        }

        public static void ShowWindow(this FGUIJoyStickLayerComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIJoyStickLayerComponent self)
        {
        }
    }
}