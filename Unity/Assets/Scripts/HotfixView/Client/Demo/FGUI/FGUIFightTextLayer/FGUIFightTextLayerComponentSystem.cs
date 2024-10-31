/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Collections.Generic;
using System.Reflection;
using FairyGUI;
using UnityEditor.Media;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(FGUIFightTextLayerComponent))]
    [FriendOf(typeof(UIBaseWindow))]
    public static class FGUIFightTextLayerComponentSystem
    {
        public static void RegisterUIEvent(this FGUIFightTextLayerComponent self)
        {
            Type type = self.View.GetType();

            List<string> typeNames = new List<string>()
            {
                typeof(FGUIDamageTextItemCellComponent).Name, typeof(FGUIAddExpTextItemCellComponent).Name,
                typeof(FGUIAddMeatTextItemCellComponent).Name
            };

            for (int i = 0; i < 50; i++)
            {
                foreach (var typeName in typeNames)
                {
                    string name = typeName.Replace("FGUI", "").Replace("Component", "") + i + "Component";

                    Log.Debug($"RegisterUIEvent name {name}");

                    PropertyInfo info = type.GetProperty(name);

                    if (info == null)
                    {
                        Log.Debug("info is null");
                        continue;
                    }

                    Entity fguiDamageTextItemCellComponent = info.GetValue(self.View) as Entity;

                    if (self.TextItemCellComponents.ContainsKey(typeName))
                    {
                        self.TextItemCellComponents[typeName].Push(fguiDamageTextItemCellComponent);
                    }
                    else
                    {
                        Stack<Entity> stack = new Stack<Entity>();

                        stack.Push(fguiDamageTextItemCellComponent);

                        self.TextItemCellComponents[typeName] = stack;
                    }
                }
            }
        }

        private static T GetPoolObjectByType<T>(this FGUIFightTextLayerComponent self) where T : Entity
        {
            string typeName = typeof(T).Name;

            // Log.Debug($"type name {typeName}");
            //
            // foreach (var kv in self.TextItemCellComponents)
            // {
            //     Log.Debug($"kv {kv.Key} {kv.Value.Count}");
            // }

            if (self.TextItemCellComponents.ContainsKey(typeName))
            {
                var stack = self.TextItemCellComponents[typeName];

                if (stack.Count > 0)
                {
                    return stack.Pop() as T;
                }
            }
            else
            {
                Log.Debug("get pool object by trype is null");
            }

            return null;
        }

        private static void ReceiveObjectToPool(this FGUIFightTextLayerComponent self, Entity entity)
        {
            string typeName = entity.GetType().Name;

            if (self.TextItemCellComponents.ContainsKey(typeName))
            {
                Stack<Entity> stack = self.TextItemCellComponents[typeName];

                stack.Push(entity);
            }
        }

        public static void ShowWindow(this FGUIFightTextLayerComponent self, Entity contextData = null)
        {
        }

        public static void HideWindow(this FGUIFightTextLayerComponent self)
        {
        }

        public static async void PlayAddMeatText(this FGUIFightTextLayerComponent self, Vector3 startPos, string text)
        {
            FGUIAddMeatTextItemCellComponent addMeatTextItemCellComponent = self.GetPoolObjectByType<FGUIAddMeatTextItemCellComponent>();

            if (addMeatTextItemCellComponent == null)
            {
                return;
            }

            Vector2 pos = self.ConvertToPos(startPos);

            addMeatTextItemCellComponent.GetParent<UIBaseWindow>().GComponent.position = pos;

            addMeatTextItemCellComponent.View.Label.text = text.ToString();

            addMeatTextItemCellComponent.View.ShowAnim.Play();

            await self.Root().GetComponent<TimerComponent>().WaitAsync(1000);

            self.ReceiveObjectToPool(addMeatTextItemCellComponent);
        }

        public static async void PlayAddExpText(this FGUIFightTextLayerComponent self, Vector3 startPos, string text)
        {
            Log.Debug($"play add exp text {text}");
            FGUIAddExpTextItemCellComponent addExpTextItemCellComponent = self.GetPoolObjectByType<FGUIAddExpTextItemCellComponent>();

            if (addExpTextItemCellComponent == null)
            {
                Log.Debug($"add exp text item cell is null");

                return;
            }

            Vector2 pos = self.ConvertToPos(startPos);

            addExpTextItemCellComponent.GetParent<UIBaseWindow>().GComponent.position = pos;

            addExpTextItemCellComponent.View.Label.SetVar("Value", text).FlushVars();

            addExpTextItemCellComponent.View.ShowAnim.Play();

            await self.Root().GetComponent<TimerComponent>().WaitAsync(1000);

            self.ReceiveObjectToPool(addExpTextItemCellComponent);
        }

        public static async void PlayDamageText(this FGUIFightTextLayerComponent self, Vector3 startPos, string text)
        {
            FGUIDamageTextItemCellComponent damageTextItemCellComponent = self.GetPoolObjectByType<FGUIDamageTextItemCellComponent>();

            if (damageTextItemCellComponent == null)
            {
                return;
            }

            Vector2 pos = self.ConvertToPos(startPos);

            damageTextItemCellComponent.GetParent<UIBaseWindow>().GComponent.position = pos;

            damageTextItemCellComponent.View.Label.text = text;

            damageTextItemCellComponent.View.ShowAnim.Play();

            await self.Root().GetComponent<TimerComponent>().WaitAsync(1000);

            self.ReceiveObjectToPool(damageTextItemCellComponent);
        }

        public static Vector2 ConvertToPos(this FGUIFightTextLayerComponent self, Vector3 startPos)
        {
            //3D视图下的位置，转化到屏幕上之后的位置
            Vector3 pos = Camera.main.WorldToScreenPoint(startPos);
            //Unity初始位置在左下角，FGUI在左上角，所以需要取反
            pos.y = Screen.height - pos.y;
            //
            Vector2 pt = GRoot.inst.GlobalToLocal(pos);

            return pt;
        }
    }
}