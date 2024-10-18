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
        private static void Awake(this GameObjectComponent self, GameObject gameObject)
        {
            self.GameObject = gameObject;

            self.GameObject.transform.forward = Vector3.back;

            self.GameObject.transform.position = Vector3.zero;

            self.CharacterController = self.GameObject.GetComponent<CharacterController>();

            self.Animation = self.GameObject.GetComponent<SkeletonAnimation>();

            self.Animation.state.SetAnimation(0, "idle", true);

            self.LocalScale = self.GameObject.transform.localScale;

            self.PosList = new Vector3[10];

            self.InitPosList();
        }

        public static void InitPosList(this GameObjectComponent self)
        {
            int index = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    // int number = RandomGenerator.RandomNumber(0, 360);

                    Vector3 pos = new Vector3(j, 0, i);

                    Vector3 endPos = Quaternion.Euler(0, 45, 0) * pos;

                    self.PosList[index] = self.GameObject.transform.position + endPos;

                    index++;
                }
            }
        }

        public static void BindCM(this GameObjectComponent self)
        {
            self.Camera = Camera.main;

            self.GameObject.transform.rotation = Camera.main.transform.rotation;
        }

        public static void Move(this GameObjectComponent self, Vector3 targetPos)
        {
            self.TargetPos = targetPos;
        }

        public static void StartMove(this GameObjectComponent self)
        {
            self.TargetPower = 1;

            self.Animation.state.SetAnimation(0, "move", true);
        }

        public static void EndMove(this GameObjectComponent self)
        {
            self.TargetPower = 0;

            self.Animation.state.SetAnimation(0, "idle", true);

            self.TargetPos = self.GameObject.transform.position;
        }

        [EntitySystem]
        public static void Update(this GameObjectComponent self)
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
            
            self.CharacterController.Move(moveSpeed * ConstValue.MoveSpeed * Time.deltaTime);
            
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