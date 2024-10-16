using Cinemachine.Utility;
using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(HeroCardObjectComponent))]
    public static partial class HeroCardObjectComponentSystem
    {
        [EntitySystem]
        public static void Destroy(this HeroCardObjectComponent self)
        {
            GameObject.Destroy(self.GameObject);
        }

        [EntitySystem]
        public static async void Awake(this HeroCardObjectComponent self, Unit unit)
        {
            HeroCard heroCard = self.GetParent<HeroCard>();

            self.UnitObject = unit.GetComponent<GameObjectComponent>().GameObject;

            GlobalComponent globalComponent = self.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(heroCard.Config.PrefabName);

            GameObject gameObject = GameObject.Instantiate(prefab);

            self.GameObject = gameObject;

            self.GameObject.transform.position = new Vector3(1000, 0, 1000);

            self.GameObject.transform.forward = Vector3.back;

            self.CharacterController = self.GameObject.GetComponent<CharacterController>();

            self.Animator = self.GameObject.GetComponent<Animator>();

            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            while (!self.IsDisposed)
            {
                if (self.AIType == AIType.Moveing)
                {
                    await self.MoveToUnit();
                }

                await timerComponent.WaitAsync(100);
            }
        }

        public static async ETTask MoveToUnit(this HeroCardObjectComponent self)
        {
            //首先判断自己与unit之间的距离，如果距离过远，那么直接瞬移过去
            Vector3 pos = self.GameObject.transform.position;

            Vector3 unitPos = self.UnitObject.transform.position;

            float distance = (pos - unitPos).magnitude;

            if (distance > 10)
            {
                float x = (RandomGenerator.RandFloat01() + 0.5f) * 2 - 0.5f;

                float y = (RandomGenerator.RandFloat01() + 0.5f) * 2 - 0.5f;

                //随机unit周围的位置
                Vector3 endPos = self.UnitObject.transform.position + new Vector3(x, 0, y) * 2;

                self.GameObject.transform.position = endPos;
            }
            else
            {
                self.TargetPower = 1;

                self.MoveToUnitTask = ETTask.Create();

                await self.MoveToUnitTask;
            }
        }

        public static void MoveEnd(this HeroCardObjectComponent self)
        {
        }

        [EntitySystem]
        public static void Update(this HeroCardObjectComponent self)
        {
            if (self.GameObject != null)
            {
                if (self.CurrentPower < self.TargetPower)
                {
                    self.CurrentPower += Time.deltaTime * 4;

                    if (self.CurrentPower > self.TargetPower)
                    {
                        self.CurrentPower = self.TargetPower;
                    }
                }
                else if (self.CurrentPower > self.TargetPower)
                {
                    self.CurrentPower -= Time.deltaTime * 4;

                    if (self.CurrentPower < 0)
                    {
                        self.CurrentPower = 0;
                    }
                }

                self.Animator.SetFloat(self.Speed, self.CurrentPower);
            }

            if (self.MoveToUnitTask != null && !self.MoveToUnitTask.IsCompleted)
            {
                Vector3 direction = (self.UnitObject.transform.position - self.GameObject.transform.position).normalized;

                self.CharacterController.Move(direction * Time.deltaTime * ConstValue.MoveSpeed * self.CurrentPower);

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 平滑旋转到目标朝向
                self.GameObject.transform.rotation = Quaternion.Slerp(self.GameObject.transform.rotation, targetRotation, Time.deltaTime * 10);

                if (self.BeforFramePos.Equals(Vector3.zero))
                {
                    self.BeforFramePos = self.UnitObject.transform.position;
                }
                else
                {
                    float moveDis = (self.UnitObject.transform.position - self.BeforFramePos).magnitude;

                    if (moveDis == 0)
                    {
                        self.TargetPower = 0;

                        self.MoveToUnitTask.SetResult();
                    }
                }

                self.BeforFramePos = self.UnitObject.transform.position;
            }

            // if (self.CurrentPower < self.TargetPower)
            // {
            //     self.CurrentPower += Time.deltaTime * 4;
            //
            //     if (self.CurrentPower > self.TargetPower)
            //     {
            //         self.CurrentPower = self.TargetPower;
            //     }
            // }
            // else if (self.CurrentPower > self.TargetPower)
            // {
            //     self.CurrentPower -= Time.deltaTime * 4;
            //
            //     if (self.CurrentPower < 0)
            //     {
            //         self.CurrentPower = 0;
            //     }
            // }
            //
            // self.CharacterController.Move(self.MoveSpeed * Time.deltaTime * 3 * self.CurrentPower);
            //
            // self.GameObject.GetComponent<Animator>().SetFloat(self.Speed, self.CurrentPower);
        }

        // public static void Move(this HeroCardObjectComponent self, Vector2 direction, float power)
        // {
        //     self.TargetPower = power;
        //
        //     self.MoveSpeed = new Vector3(direction.x, 0, direction.y * -1);
        //
        //     // 获取当前朝向和目标朝向之间的差值
        //     Quaternion targetRotation = Quaternion.LookRotation(self.MoveSpeed);
        //
        //     // 平滑旋转到目标朝向
        //     self.GameObject.transform.rotation = Quaternion.Slerp(self.GameObject.transform.rotation, targetRotation, Time.deltaTime * 10);
        // }
    }
}