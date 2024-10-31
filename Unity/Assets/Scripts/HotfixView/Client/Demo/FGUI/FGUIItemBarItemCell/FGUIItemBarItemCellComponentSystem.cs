/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using UnityEditor;

namespace ET.Client
{
    [FriendOf(typeof(FGUIItemBarItemCellComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIItemBarItemCellComponentSystem
    {
        public static void RegisterUIEvent(this FGUIItemBarItemCellComponent self)
        {
        }

        public static void ShowWindow(this FGUIItemBarItemCellComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIItemBarItemCellComponent self)
        {
        }

        public static void SetInfo(this FGUIItemBarItemCellComponent self, int key, int count)
        {
            self.View.Value.text = count.ToString();

            self.View.ClickButton.SetListenerAsync(self, self.OnClickButton);

            self.Key = key;

            ItemConfig itemConfig = ItemConfigCategory.Instance.Get(key);

            self.View.Title.text = itemConfig.Name;

            if (!string.IsNullOrEmpty(itemConfig.ColorType))
            {
                self.View.Progress.GetController("ColorState").selectedPage = itemConfig.ColorType;

                if (itemConfig.MaxLimited != 0)
                {
                    self.View.Progress.value = count;

                    self.View.Progress.max = itemConfig.MaxLimited;

                    self.View.Progress.min = 0;
                }
            }
        }

        public static async ETTask OnClickButton(this FGUIItemBarItemCellComponent self)
        {
            Log.Debug($"key click {self.Key}");

            int errorCode = await ItemHelper.AddItemCount(self.Root(), self.Key, 1000);

            if (errorCode == ErrorCode.ERR_Success)
            {
               
            }
        }
    }
}