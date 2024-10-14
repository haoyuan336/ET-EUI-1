using System;
using System.Collections.Generic;
using ET.Client;
using FairyGUI;
using UnityEngine.Events;

namespace ET
{
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIHelper
    {
        //AwaitAllQueueWindowClose--Start
        // public static ETTask AwaitAllQueueWindowClose(Entity entity)
        // {
        //     //通过一个实体，判断UI界面是否没有正在等待的队列
        //     Scene scene = entity.ZoneScene();
        //     FGUIComponent fguiComponent = scene.GetComponent<FGUIComponent>();
        //     ETTask task = ETTask.Create();
        //     fguiComponent.Reg_WaitAllWindowClose(task);
        //     return task; //创造任务，返回任务，自己不需要进行等待，节省中间过程
        // }

        // public static ETTask AwaitAllQueueWindowClose(FGUIComponent fguiComponent)
        // {
        //     ETTask task = ETTask.Create();
        //     fguiComponent.Reg_WaitAllWindowClose(task);
        //     return task;
        // }
        //AwaitAllQueueWindowClose--End

        public static void AddListener(this GButton self, Action<int> action, int para)
        {
            self.onClick.Add(() => { action.Invoke(para); });
        }

        public static void AddListenerAsync(this GButton self, Func<int, ETTask> action, int para)
        {
            async ETTask clickActionAsync()
            {
                // UIEventComponent.Instance.SetUIClicked(self, true);
                await action(para);
                // UIEventComponent.Instance.SetUIClicked(self, false);
            }

            self.onClick.Add(() =>
            {
                // if (UIEventComponent.Instance.GetUIClicked(self))
                // {
                //     return;
                // }

                clickActionAsync().Coroutine();
            });
        }

        public static void SetListener<T>(this GButton self, Action<T> action, T para)
        {
            self.onClick.Set(() => { action.Invoke(para); });
        }

        public static void SetListener(this GButton self, UnityAction clickEventHandler)
        {
            self.onClick.Clear();

            self.onClick.Add(() => { clickEventHandler(); });
        }

        public static void SetListenerAsync(this GButton self, Entity target, Func<ETTask> action)
        {
            self.onClick.Clear();

            self.AddListenerAsync(target, action);
        }

        // public static void SetListenerAsync<T>(this GButton self, Func<T, ETTask> action, T param)
        // {
        //     self.onClick.Clear();
        //
        //     // self.AddListenerAsync(action, param);
        //
        //     async ETTask clickActionAsync()
        //     {
        //         UIEventComponent.Instance.SetUIClicked(self, true);
        //         await action(param);
        //         UIEventComponent.Instance.SetUIClicked(self, false);
        //     }
        //
        //     self.onClick.Add(() =>
        //     {
        //         if (UIEventComponent.Instance.GetUIClicked(self))
        //         {
        //             return;
        //         }
        //
        //         clickActionAsync().Coroutine();
        //     });
        // }

        public static void AddListenerAsync(this GButton self, Entity target, Func<ETTask> action)
        {
            // self.onClick.RemoveAllListeners();

            UIEventComponent uiEventComponent = target.Root().GetComponent<UIEventComponent>();

            async ETTask clickActionAsync()
            {
                uiEventComponent.SetUIClicked(self, true);
                await action();
                uiEventComponent.SetUIClicked(self, false);
            }

            self.onClick.Add(() =>
            {
                if (uiEventComponent.GetUIClicked(self))
                {
                    return;
                }

                clickActionAsync().Coroutine();
            });
        }

        // public static void RemoveListener(this GButton self, Action action)
        // {
        //     self.onClick.RemoveCapture();
        // }

        public static EventCallback0 AddListener(this GButton button, UnityAction clickEventHandler)
        {
            EventCallback0 callback0 = () =>
            {
                if (clickEventHandler != null)
                {
                    clickEventHandler.Invoke();
                }
            };

            button.onClick.Add(callback0);

            return callback0;
        }

        public static void RemoveListener(this GButton gButton, EventCallback0 callback0)
        {
            gButton.onClick.Remove(callback0);
        }

        public static void AddListener(this GComboBox controller, UnityAction<int, string> changIndex)
        {
            controller.onChanged.Add(() => { changIndex.Invoke(controller.selectedIndex, controller.value); });
        }

        public static void AddListener(this Controller controller, UnityAction<int, string> changIndex)
        {
            controller.onChanged.Add(() => { changIndex.Invoke(controller.selectedIndex, controller.selectedPage); });
        }

        public static void AddListener(this Controller controller, UnityAction<string> changIndex)
        {
            controller.onChanged.Add(() => { changIndex.Invoke(controller.selectedPage); });
        }

        public static void AddListener(this Controller controller, UnityAction<int> changIndex)
        {
            controller.onChanged.Add(() => { changIndex.Invoke(controller.selectedIndex); });
        }

        public static void SetListener(this Controller controller, UnityAction<int> changeIndex)
        {
            controller.onChanged.Set(() => { changeIndex.Invoke(controller.selectedIndex); });
        }

        // public static void ClearLis(this Controller controller)
        // {
        //     controller.onChanged.Clear();
        // }
        //
        // /// <summary>
        // /// 增加子节点的BaseWindow绑定
        // /// </summary>
        // /// <param name="self"></param>
        // /// <param name="dictionary"></param>
        // /// <param name="count"></param>
        // /// <param name="id"></param>
        // public static void AddUIListItems(this Entity self, ref Dictionary<int, FGUIBaseWindow> dictionary, int count, FGUIComponentID id)
        // {
        //     if (dictionary == null)
        //     {
        //         dictionary = new Dictionary<int, FGUIBaseWindow>();
        //     }
        //
        //     if (count <= 0)
        //     {
        //         return;
        //     }
        //
        //     foreach (var item in dictionary)
        //     {
        //         item.Value.Dispose();
        //     }
        //
        //     dictionary.Clear();
        //     for (int i = 0; i <= count; i++)
        //     {
        //         FGUIBaseWindow itemServer = self.AddChild<FGUIBaseWindow>(true);
        //         FGUIEventComponent.Instance.GetUIEventHandler(id).OnInitComponent(itemServer);
        //         dictionary.Add(i, itemServer);
        //     }
        // }

        // /// <summary>
        // /// 获取子节点的组件
        // /// </summary>
        // /// <param name="self"></param>
        // /// <param name="name">子节点在 fgui 里面的名称</param>
        // /// <returns></returns>
        // public static GComponent GetChildComponent(this Entity self, string name)
        // {
        //     GComponent gComponent = self.GetParent<FGUIBaseWindow>().GComponent;
        //
        //     return gComponent.GetChild(name).asCom;
        // }

        // /// <summary>
        // /// 获取子节点的矩形边缘
        // /// </summary>
        // /// <param name="self"></param>
        // /// <param name="name"></param>
        // /// <returns></returns>
        // public static float[] GetChildRect(this Entity self, string name)
        // {
        //     GComponent gComponent = self.GetChildComponent(name);
        //
        //     return new[] { gComponent.x, gComponent.y, gComponent.width, gComponent.height };
        // }

        // public static void AddFGUIComponent(this Entity self, FGUIComponentID id)
        // {
        //     GComponent gComponent = self.GetParent<FGUIBaseWindow>().GComponent;
        //
        //     string uiName = id.ToString().Replace("FGUI", "");
        //
        //     GComponent childComponent = UIPackage.CreateObject(ConstValue.FGUIPackageName, uiName).asCom;
        //
        //     childComponent.MakeFullScreen();
        //
        //     gComponent.AddChild(childComponent);
        //
        //     FGUIBaseWindow fguiBaseWindow = self.AddChild<FGUIBaseWindow>();
        //
        //     fguiBaseWindow.GComponent = gComponent;
        //     
        //     // fguiBaseWindow.AddComponent<>()
        // }
        //
        // public static void RemoveFGUIComponent(this Entity self, FGUIComponentID componentId, long id)
        // {
        // }
    }
}