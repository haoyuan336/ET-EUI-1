using System;
using Cinemachine.Utility;
using Spine.Unity;
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

            self.Animation = gameObject.GetComponent<SkeletonAnimation>();

            self.Animation.state.SetAnimation(0, "idle", true);

            self.GameObject.transform.rotation = Camera.main.transform.rotation;

            self.CharacterController = self.GameObject.GetComponent<CharacterController>();

            self.LocalScale = self.GameObject.transform.localScale;

            // Vector3 pos = Quaternion.Euler(0, index * 36, 0) * Vector3.forward * 2;

            Vector3 pos = Vector3.left * 2 * self.Index;

            self.GameObject.transform.position = self.UnitObject.transform.position + pos;

            self.TargetPos = self.GameObject.transform.position;
        }

        public static void Move(this HeroCardObjectComponent self, Vector3 targetPos)
        {
            self.TargetPos = targetPos;
        }

        public static void StartMove(this HeroCardObjectComponent self)
        {
            self.Animation.state.SetAnimation(0, "move", true);

            self.TargetPower = 1;
        }

        public static void MoveEnd(this HeroCardObjectComponent self)
        {
            self.Animation.state.SetAnimation(0, "idle", true);

            self.TargetPower = 0;

            self.TargetPos = self.GameObject.transform.position;
        }

        [EntitySystem]
        public static void Update(this HeroCardObjectComponent self)
        {
            Vector3 moveSpeed = self.TargetPos - self.GameObject.transform.position;

            moveSpeed = new Vector3(moveSpeed.x, 0, moveSpeed.z).normalized;

            int heroMask = LayerMask.GetMask("Hero");

            bool isHited = Physics.Raycast(self.GameObject.transform.position, moveSpeed, out RaycastHit hit, 1, heroMask);

            Vector3 dir = Vector3.zero;

            if (isHited)
            {
                dir = self.GameObject.transform.position - hit.transform.position;
            }

            self.CharacterController.Move((moveSpeed.normalized + dir.normalized) * ConstValue.MoveSpeed * Time.deltaTime);

            self.GameObject.transform.position = new Vector3(self.GameObject.transform.position.x, 0, self.GameObject.transform.position.z);
            
            if (moveSpeed.x != 0)
            {
                self.GameObject.transform.localScale = new Vector3(self.LocalScale.x * moveSpeed.x / Math.Abs(moveSpeed.x) * -1,
                    self.LocalScale.y,
                    self.LocalScale.z);
            }
        }
    }
}