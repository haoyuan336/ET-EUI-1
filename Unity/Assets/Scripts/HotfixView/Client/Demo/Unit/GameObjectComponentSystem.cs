using System;
using Spine.Unity;
using UnityEngine;
using WeChatWASM;
using Random = UnityEngine.Random;

namespace ET.Client
{
    [EntitySystemOf(typeof(GameObjectComponent))]
    public static partial class GameObjectComponentSystem
    {
        [EntitySystem]
        private static void Destroy(this GameObjectComponent self)
        {
            UnityEngine.Object.Destroy(self.GameObject);
        }

        [EntitySystem]
        private static void Awake(this GameObjectComponent self)
        {
            GlobalComponent globalComponent = self.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>("Unit");

            GameObject unitInitPos = GameObject.Find("UnitInitPos");
            
            self.GameObject = GameObject.Instantiate(prefab, unitInitPos.transform.position, prefab.transform.rotation);
            //
            
            self.GameObject.transform.position = unitInitPos.transform.position;
            
            self.CharacterController = self.GameObject.GetComponent<CharacterController>();
            
            self.TargetPos = self.GameObject.transform.position;
        }

        public static void BindCM(this GameObjectComponent self)
        {
            // self.Camera = Camera.main;

            // self.GameObject.transform.rotation = Camera.main.transform.rotation;
        }

        public static void Move(this GameObjectComponent self, Vector3 targetPos)
        {
            self.TargetPos = targetPos;
        }

        public static void StartMove(this GameObjectComponent self)
        {
            // self.TargetPower = 1;

            // self.Animation.state.SetAnimation(0, "move", true);
        }

        public static void EndMove(this GameObjectComponent self)
        {
            // self.TargetPower = 0;

            // self.Animation.state.SetAnimation(0, "idle", true);

            self.TargetPos = self.GameObject.transform.position;
        }

        [EntitySystem]
        public static void Update(this GameObjectComponent self)
        {
            if (self.GameObject != null)
            {
                Vector3 moveSpeed = self.TargetPos - self.GameObject.transform.position;

                moveSpeed = new Vector3(moveSpeed.x, 0, moveSpeed.z).normalized;

                self.CharacterController.Move(moveSpeed * ConstValue.MoveSpeed * Time.deltaTime);

                self.GameObject.transform.position = new Vector3(self.GameObject.transform.position.x, 0, self.GameObject.transform.position.z);

                if (moveSpeed.x != 0)
                {
                    // self.GameObject.transform.localScale = new Vector3(self.LocalScale.x * moveSpeed.x / Math.Abs(moveSpeed.x) * -1,
                    //     self.LocalScale.y,
                    //     self.LocalScale.z);
                }
            }
        }
    }
}