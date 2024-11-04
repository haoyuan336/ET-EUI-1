using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(InteractivePoint))]
    public static partial class InteractivePointSystem
    {
        [EntitySystem]
        public static void Destroy(this InteractivePoint self)
        {
            if (self.ColliderAction != null)
            {
                self.ColliderAction.OnTriggerEnterAction -= self.OnEnterInteractivePointRange;

                self.ColliderAction.OnTriggerExitAction -= self.OnExitInteractivePointRange;
            }

            if (self.InteractivePointItemCellComponent != null && self.FightTextLayerComponent != null)
            {
                self.FightTextLayerComponent.ReceiveObjectToPool(self.InteractivePointItemCellComponent);
            }

            self.InteractivePointItemCellComponent = null;

            self.FightTextLayerComponent = null;

            self.GComponent = null;
        }

        public static void BindObject(this InteractivePoint self, GameObject gameObject)
        {
            self.BindObject = gameObject;

            ColliderAction colliderAction = self.BindObject.GetComponent<ColliderAction>();

            colliderAction.OnTriggerEnterAction += self.OnEnterInteractivePointRange;

            colliderAction.OnTriggerExitAction += self.OnExitInteractivePointRange;

            self.ColliderAction = colliderAction;
        }

        [EntitySystem]
        public static void Update(this InteractivePoint self)
        {
            if (self.GComponent != null)
            {
                Vector3 startPos = self.BindObject.transform.position + self.BindObject.GetComponent<SphereCollider>().radius * Vector3.up;

                Vector2 pt = ConvertToPosHelper.ConvertToPos(startPos);

                self.GComponent.position = pt;
            }
        }

        public static async void OnEnterInteractivePointRange(this InteractivePoint self, GameObject collider)
        {
            UIComponent uiComponent = self.Root().GetComponent<UIComponent>();

            FGUIFightTextLayerComponent fightTextLayerComponent = uiComponent.GetDlgLogic<FGUIFightTextLayerComponent>();

            self.FightTextLayerComponent = fightTextLayerComponent;

            FGUIInteractivePointItemCellComponent interactivePointItemCellComponent =
                    fightTextLayerComponent.GetPoolObjectByType<FGUIInteractivePointItemCellComponent>();

            if (interactivePointItemCellComponent == null)
            {
                return;
            }

            self.InteractivePointItemCellComponent = interactivePointItemCellComponent;

            self.GComponent = interactivePointItemCellComponent.GetParent<UIBaseWindow>().GComponent;

            interactivePointItemCellComponent.View.EnterAnim.Play();

            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            await timerComponent.WaitAsync(200);

            interactivePointItemCellComponent.View.ClickButton.SetListener(self.OnButtonClick);
        }

        private static void OnButtonClick(this InteractivePoint self)
        {
            Log.Debug($"on button click {self.Id}");

            InteractivePointConfig interactivePointConfig = InteractivePointConfigCategory.Instance.Get((int)self.Id);

            switch (interactivePointConfig.InteractveType)
            {
                case "Teleport":
                    
                    
                    break;
            }
        }

        private static async void OnExitInteractivePointRange(this InteractivePoint self, GameObject collider)
        {
            if (self.FightTextLayerComponent != null)
            {
                self.FightTextLayerComponent.ReceiveObjectToPool(self.InteractivePointItemCellComponent);

                self.InteractivePointItemCellComponent.View.ExitAnim.Play();

                TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

                await timerComponent.WaitAsync(200);

                self.FightTextLayerComponent = null;

                self.InteractivePointItemCellComponent = null;

                self.GComponent = null;
            }
        }
    }
}