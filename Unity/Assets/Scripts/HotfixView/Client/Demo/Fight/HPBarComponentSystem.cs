using FairyGUI;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(HPBarComponent))]
    public static partial class HPBarComponentSystem
    {
        [EntitySystem]
        public static void Destroy(this HPBarComponent self)
        {
            if (self.CellComponent != null)
            {
                UIComponent uiComponent = self.Root().GetComponent<UIComponent>();

                FGUIMainLayerComponent mainLayerComponent = uiComponent.GetDlgLogic<FGUIMainLayerComponent>();

                Log.Debug($"mainlayer component = {mainLayerComponent == null}");
                if (mainLayerComponent != null)
                {
                    mainLayerComponent.ReceiveHPBar(self.CellComponent);

                    self.CellComponent = null;
                }
            }
        }

        [EntitySystem]
        public static async void Awake(this HPBarComponent self, GameObject gameObject)
        {
            //取出来一个

            self.GameObject = gameObject;

            UIComponent uiComponent = self.Root().GetComponent<UIComponent>();

            FGUIMainLayerComponent mainLayerComponent = uiComponent.GetDlgLogic<FGUIMainLayerComponent>();

            self.CellComponent = mainLayerComponent.GetOneHPBar();

            Log.Debug($"awake {self.CellComponent == null}");

            if (self.CellComponent == null)
            {
                self.Dispose();

                return;
            }

            self.ColorState = self.CellComponent.View.Progress.GetController("ColorState");

            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            while (!self.IsDisposed)
            {
                await timerComponent.WaitAsync(2000);

                if (TimeInfo.Instance.ClientNow() > self.OutTime)
                {
                    self.Dispose();
                }
            }
        }

        [EntitySystem]
        public static void Update(this HPBarComponent self)
        {
            if (self.CellComponent != null)
            {
                Vector3 headPos = self.Parent.GetComponent<ObjectComponent>().GetHeadPos();
                // Collider sphereCollider = self.GameObject.GetComponent<Collider>();
                //
                // Vector3 position = self.GameObject.transform.position + Vector3.up * 1.5f * sphereCollider.bounds.size.y;

                //3D视图下的位置，转化到屏幕上之后的位置
                Vector3 pos = Camera.main.WorldToScreenPoint(headPos);
                //Unity初始位置在左下角，FGUI在左上角，所以需要取反
                pos.y = Screen.height - pos.y;

                Vector2 pt = GRoot.inst.GlobalToLocal(pos);

                // self.CellComponent.View.Progress.position = pt;

                self.CellComponent.GetParent<UIBaseWindow>().GComponent.position = pt;
            }
        }

        public static void SetBarValue(this HPBarComponent self, float currentValue, float maxValue)
        {
            Log.Debug($"set bar value {currentValue} {maxValue} {self.OutTime}, {TimeInfo.Instance.ClientNow()}");
            if (self.CellComponent != null)
            {
                self.CellComponent.View.Progress.min = 0;

                self.CellComponent.View.Progress.max = maxValue;

                self.CellComponent.View.Progress.value = currentValue;

                self.OutTime = TimeInfo.Instance.ClientNow() + 5000;

                self.ColorState.selectedIndex = currentValue >= maxValue / 2.0f ? 0 : 1;
            }
            else
            {
                self.Dispose();
            }
        }
    }
}