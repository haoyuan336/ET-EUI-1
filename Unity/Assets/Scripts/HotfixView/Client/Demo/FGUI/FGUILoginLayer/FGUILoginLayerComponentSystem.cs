/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEngine.Windows;
using WeChatWASM;

namespace ET.Client
{
    [FriendOf(typeof(FGUILoginLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUILoginLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUILoginLayerComponent self)
        {
            self.View.LoginButton.onClick.Set(self.OnClickButton);

            // self.View.Account.onTouchBegin.Set(self.OnAccountTouchBegin);

            self.View.Account.onTouchBegin.Set(self.OnAccountFocusIn);

            self.View.Password.onTouchBegin.Set(self.OnPasswordFocusIn);
        }

        private static void OnPasswordFocusIn(this FGUILoginLayerComponent self)
        {
            self.InputType = "Password";

            WX.ShowKeyboard(new ShowKeyboardOption
            {
                confirmHold = true,
                confirmType = "done",
            });
        }

        private static void OnAccountFocusIn(this FGUILoginLayerComponent self)
        {
            self.InputType = "Account";
            Log.Debug("on account touch begin");
            WX.ShowKeyboard(new ShowKeyboardOption
            {
                confirmHold = true,
                confirmType = "done",
                defaultValue = self.View.Account.text
            });
        }

        private static async void OnClickButton(this FGUILoginLayerComponent self)
        {
            Log.Debug("on click button click");
            string account = self.View.Account.text;

            string password = self.View.Password.text;

            await LoginHelper.Login(self.Root(), account, password);

            UIComponent uiComponent = self.Root().GetComponent<UIComponent>();

            uiComponent.CloseWindow(WindowID.LoginLayer);
            
            uiComponent.ShowWindow(WindowID.LoginServerLayer);
            // uiComponent.HideWindow(WindowID.LoginLayer);
        }

        public static void ShowWindow(this FGUILoginLayerComponent self, Entity contextData = null)
        {
            // WX.OnKeyboardInput(self.OnKeyBoardInput);

            // WX.OnKeyboardConfirm(self.OnWXKeyboardConfirm);
            
            WX.OnKeyboardComplete(self.OnWXKeyboardConfirm);
        }

        public static void OnWXKeyboardConfirm(this FGUILoginLayerComponent self, OnKeyboardInputListenerResult result)
        {
            Log.Debug($"on key board input {result.value} {self.InputType}");
            switch (self.InputType)
            {
                case "Account":
                    self.View.Account.text = result.value;
                    break;

                case "Password":

                    self.View.Password.text = result.value;
                    break;
            }

            WX.HideKeyboard(new HideKeyboardOption());
        }

        public static void HideWindow(this FGUILoginLayerComponent self)
        {
            // WX.OffKeyboardConfirm(self.OnWXKeyboardConfirm);
            
            WX.OffKeyboardComplete(self.OnWXKeyboardConfirm);
            
        }
    }
}