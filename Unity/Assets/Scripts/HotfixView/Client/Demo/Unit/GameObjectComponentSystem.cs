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
        }

        public static void Move(this GameObjectComponent self, Vector3 targetPos)
        {
            self.TargetPos = targetPos;
        }

        public static void StartMove(this GameObjectComponent self)
        {
        }

        public static void EndMove(this GameObjectComponent self)
        {
            self.TargetPos = self.GameObject.transform.position;
        }

        [EntitySystem]
        public static void Update(this GameObjectComponent self)
        {
            if (self.GameObject != null)
            {
                Vector3 moveSpeed = self.TargetPos - self.GameObject.transform.position;

                moveSpeed = new Vector3(moveSpeed.x, 0, moveSpeed.z).normalized;

                self.CharacterController.Move(moveSpeed * ConstValue.MoveSpeed * Time.deltaTime * self.SpeedValue);

                self.GameObject.transform.position = new Vector3(self.GameObject.transform.position.x, 0, self.GameObject.transform.position.z);
            }
        }

        public static async ETTask MoveUnitToMainCity(this GameObjectComponent self)
        {
            GameObject unitInitPos = GameObject.Find("UnitInitPos");

            self.TargetPos = unitInitPos.transform.position;

            self.GameObject.transform.position = self.TargetPos;

            float time = 0;

            Vector3 startPos = self.GameObject.transform.position;

            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            while (time < 1)
            {
                Vector3 pos = Vector3.Lerp(startPos, unitInitPos.transform.position, time);

                self.GameObject.transform.position = pos;

                self.TargetPos = pos;

                time += Time.deltaTime;

                await timerComponent.WaitFrameAsync();
            }

            // self.GameObject.transform.position = unitInitPos.transform.position;
        }
    }
}