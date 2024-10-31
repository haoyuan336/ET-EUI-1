using FairyGUI;
using Spine.Unity;
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

                if (mainLayerComponent != null)
                {
                    mainLayerComponent.ReceiveHPBar(self.CellComponent);

                    self.CellComponent = null;
                }
            }

            AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

            aiComponent.EnterStateAction -= self.OnStateEnter;
        }

        [EntitySystem]
        public static async void Awake(this HPBarComponent self)
        {
            //取出来一个

            UIComponent uiComponent = self.Root().GetComponent<UIComponent>();

            FGUIMainLayerComponent mainLayerComponent = uiComponent.GetDlgLogic<FGUIMainLayerComponent>();

            self.CellComponent = mainLayerComponent.GetOneHPBar();

            if (self.CellComponent == null)
            {
                self.Dispose();

                return;
            }

            bool isSampCamp = self.Parent is HeroCard;
            
            self.ColorState = self.CellComponent.View.Progress.GetController("ColorState");

            self.ColorState.selectedIndex = isSampCamp ? 0 : 1;
            
            AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

            aiComponent.EnterStateAction += self.OnStateEnter;

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

        private static void OnStateEnter(this HPBarComponent self, AIState aiState)
        {
            if (aiState == AIState.Death)
            {
                self.Dispose();
            }
        }

        [EntitySystem]
        public static void Update(this HPBarComponent self)
        {
            if (self.CellComponent != null)
            {
                Vector3 headPos = Vector3.zero;

                headPos = self.Parent.GetComponent<ObjectComponent>().GetHeadPos();

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
            if (self.CellComponent != null) 
            {
                self.CellComponent.View.Progress.min = 0;

                self.CellComponent.View.Progress.max = maxValue;

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