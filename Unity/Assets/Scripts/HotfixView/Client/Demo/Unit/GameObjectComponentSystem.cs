using System;
using Cinemachine;
using UnityEngine;

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
        private static void Awake(this GameObjectComponent self, GameObject gameObject)
        {
            self.GameObject = gameObject;

            self.GameObject.transform.forward = Vector3.back;

            self.CharacterController = self.GameObject.GetComponent<CharacterController>();
        }

        public static void BindCM(this GameObjectComponent self)
        {
            self.Camera = Camera.main;
        }

        public static void Move(this GameObjectComponent self, Vector2 direction, float power)
        {
            self.TargetPower = power;

            self.MoveSpeed = new Vector3(direction.x, 0, direction.y * -1);

            // 获取当前朝向和目标朝向之间的差值
            Quaternion targetRotation = Quaternion.LookRotation(self.MoveSpeed);

            // 平滑旋转到目标朝向
            self.GameObject.transform.rotation = Quaternion.Slerp(self.GameObject.transform.rotation, targetRotation, Time.deltaTime * 10);
        }

        [EntitySystem]
        public static void Update(this GameObjectComponent self)
        {
            // Vector3 worldTargetDirection = self.GameObject.transform.TransformDirection(self.MoveSpeed);

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

            self.CharacterController.Move(self.MoveSpeed * Time.deltaTime * ConstValue.MoveSpeed * self.CurrentPower);

            self.GameObject.GetComponent<Animator>().SetFloat(self.Speed, self.CurrentPower);

            if (self.Camera != null)
            {
                self.Camera.transform.LookAt(self.GameObject.transform.position);

                Vector3 pos = self.GameObject.transform.position;

                self.Camera.transform.position = new Vector3(pos.x, 15, pos.z - 15);
            }
        }
    }
}