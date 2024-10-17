using System;
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
        public static void Awake(this HeroCardObjectComponent self, Unit unit, int index)
        {
            self.Index = index;

            self.Unit = unit;

            HeroCard heroCard = self.GetParent<HeroCard>();

            self.UnitObject = unit.GetComponent<GameObjectComponent>().GameObject;

            GlobalComponent globalComponent = self.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(heroCard.Config.PrefabName);

            GameObject gameObject = GameObject.Instantiate(prefab);

            self.GameObject = gameObject;

            self.GameObject.transform.rotation = Camera.main.transform.rotation;

            self.CharacterController = self.GameObject.GetComponent<CharacterController>();

            // self.Animator = self.GameObject.GetComponent<Animator>();

            GameObject beforObject = self.GetBeforGameObject();

            self.GameObject.transform.position = beforObject.transform.position + Vector3.back * ConstValue.MoveDistance;

            self.TargetPos = self.GameObject.transform.position;

            self.OldPos = self.GameObject.transform.position;
        }

        public static GameObject GetBeforGameObject(this HeroCardObjectComponent self)
        {
            if (self.Index == 0)
            {
                return self.UnitObject;
            }

            HeroCardComponent heroCardComponent = self.Unit.GetComponent<HeroCardComponent>();

            HeroCard card = heroCardComponent.FormationHeroCards[self.Index - 1];

            HeroCardObjectComponent heroCardObjectComponent = card.GetComponent<HeroCardObjectComponent>();

            return heroCardObjectComponent.GameObject;
        }

        public static Vector3 GetBeforPos(this HeroCardObjectComponent self)
        {
            Vector3 oldPos = Vector3.zero;

            if (self.Index == 0)
            {
                oldPos = self.Unit.GetComponent<GameObjectComponent>().OldPos;
            }

            else
            {
                HeroCardComponent heroCardComponent = self.Unit.GetComponent<HeroCardComponent>();

                HeroCard card = heroCardComponent.FormationHeroCards[self.Index - 1];

                HeroCardObjectComponent heroCardObjectComponent = card.GetComponent<HeroCardObjectComponent>();

                oldPos = heroCardObjectComponent.OldPos;
            }

            return oldPos;
        }

        public static void MoveEnd(this HeroCardObjectComponent self)
        {
        }

        [EntitySystem]
        public static void Update(this HeroCardObjectComponent self)
        {
            if (self.GameObject != null)
            {
                Vector3 targetPos = self.GetBeforPos();

                float distance = (self.GameObject.transform.position - targetPos).magnitude;

                if (distance > ConstValue.MoveDistance)
                {
                    self.TargetPos = targetPos;

                    self.OldPos = self.GameObject.transform.position;

                    self.TargetPower = 1;
                }
                else
                {
                    self.TargetPower = 0;
                }

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

                Vector3 moveSpeed = (self.TargetPos - self.GameObject.transform.position).normalized;

                moveSpeed.y = 0;

                self.CharacterController.Move(moveSpeed * Time.deltaTime * ConstValue.MoveSpeed * self.CurrentPower);

                if (!moveSpeed.Equals(Vector3.zero))
                {
                    // Quaternion targetRotation = Quaternion.LookRotation(moveSpeed);
                    // 平滑旋转到目标朝向
                    // self.GameObject.transform.rotation = Quaternion.Slerp(self.GameObject.transform.rotation, targetRotation, Time.deltaTime * 10);

                    // self.Animator.SetFloat(self.Speed, self.CurrentPower);
                }
            }
        }
    }
}