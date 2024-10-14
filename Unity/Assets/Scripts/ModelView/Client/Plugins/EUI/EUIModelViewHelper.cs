using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    public static class EUIModelViewHelper
    {
        public static T BindTrans<T>(this EntityRef<T> self, Transform transform) where T : Entity, IAwake, IUIScrollItem<T>
        {
            T value = self;
            return value.BindTrans(transform);
        }

        public static void AddUIScrollItems<K, T>(this K self, ref Dictionary<int, EntityRef<T>> dictionary, int count) where K : Entity, IUILogic
                where T : Entity, IAwake, IUIScrollItem<T>
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<int, EntityRef<T>>();
            }

            if (count <= 0)
            {
                return;
            }

            foreach (var item in dictionary)
            {
                T value = item.Value;
                value?.Dispose();
            }

            dictionary.Clear();
            for (int i = 0; i <= count; i++)
            {
                T itemServer = self.AddChild<T>(true);
                dictionary.Add(i, itemServer);
            }
        }

        /// <summary>
        /// 增加子节点的BaseWindow绑定
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dictionary"></param>
        /// <param name="count"></param>
        /// <param name="id"></param>
        public static void AddUIListItems(this Entity self, ref Dictionary<int, UIBaseWindow> dictionary, int count, WindowID id)
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<int, UIBaseWindow>();
            }

            if (count <= 0)
            {
                return;
            }

            foreach (var item in dictionary)
            {
                item.Value.Dispose();
            }

            dictionary.Clear();
            for (int i = 0; i <= count; i++)
            {
                UIBaseWindow itemServer = self.AddChild<UIBaseWindow>(true);

                itemServer.WindowID = id;
                
                self.Root().GetComponent<UIEventComponent>().GetUIEventHandler(id).OnInitComponent(itemServer);
                // FGUIEventComponent.Instance.GetUIEventHandler(id).OnInitComponent(itemServer);
                dictionary.Add(i, itemServer);
            }
        }
    }
}