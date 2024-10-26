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
                Vector3 position = self.GameObject.transform.position;

                //3D视图下的位置，转化到屏幕上之后的位置
                Vector3 pos = Camera.main.WorldToScreenPoint(position);
                //Unity初始位置在左下角，FGUI在左上角，所以需要取反
                pos.y = Screen.height - pos.y;

                Vector2 pt = GRoot.inst.GlobalToLocal(pos);

                Log.Debug($"pt {pt}");
                // self.CellComponent.View.Progress.position = pt;

                self.CellComponent.GetParent<UIBaseWindow>().GComponent.position = pt;
            }
        }

        public static void SetBarValue(this HPBarComponent self, float max, float currentValue)
        {
            Log.Debug($"set bar value {max} {currentValue} {self.OutTime}, {TimeInfo.Instance.ClientNow()}");
            if (self.CellComponent != null)
            {
                self.CellComponent.View.Progress.max = max;

                self.CellComponent.View.Progress.value = currentValue;

                self.OutTime = TimeInfo.Instance.ClientNow() + 5000;
            }
            else
            {
                self.Dispose();
            }
        }
    }
}