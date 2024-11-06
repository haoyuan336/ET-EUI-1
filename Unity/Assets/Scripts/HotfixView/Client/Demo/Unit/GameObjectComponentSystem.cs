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

            ColliderAction colliderAction = self.GameObject.GetComponent<ColliderAction>();

            colliderAction.OnTriggerEnterAction = self.OnTriggerEnter;

            colliderAction.OnTriggerExitAction = self.OnTriggerExit;
        }

        private static void OnTriggerEnter(this GameObjectComponent self, GameObject gameObject, GameObject otherGameobject)
        {
            if (otherGameobject.CompareTag("EnterMapCollider"))
            {
                Log.Debug($"GameObjectComponent OnTriggerEnter {otherGameobject.name} ");
                string name = otherGameobject.name;

                int number = GetStringNumberHelper.GetNumber(name);

                EventSystem.Instance.Publish(self.Root(), new EnteredMapScene() { MapConfigId = number });
            }
        }

        private static void OnTriggerExit(this GameObjectComponent self, GameObject gameObject, GameObject otherObject)
        {
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

        public static async ETTask MoveUnitToTeleport(this GameObjectComponent self, int mapConfigId)
        {
            MapConfig mapConfig = MapConfigCategory.Instance.Get(mapConfigId);

            int interactivePointConfigId = mapConfig.TeleportConfigId;

            string interactivePointName = "InteractivePoint" + interactivePointConfigId;

            Log.Debug($"MoveUnitToTeleport InteractivePoint {interactivePointName}");

            GameObject taregtObject = GameObject.Find(interactivePointName);

            if (taregtObject == null)
            {
                Log.Debug("target object is null");
                await EventSystem.Instance.PublishAsync(self.Root(), new LoadMapScene() { MapConfigId = mapConfigId });

                taregtObject = GameObject.Find(interactivePointName);

                Log.Debug($"MoveUnitToTeleport load map scene {interactivePointName} {taregtObject == null}");

                if (taregtObject == null)
                {
                    return;
                }
            }

            self.TargetPos = taregtObject.transform.position;

            float time = 0;

            Vector3 startPos = self.GameObject.transform.position;

            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            while (time < 1)
            {
                Vector3 pos = Vector3.Lerp(startPos, taregtObject.transform.position, time);

                self.GameObject.transform.position = pos;

                self.TargetPos = pos;

                time += Time.deltaTime;

                await timerComponent.WaitFrameAsync();
            }

            // self.GameObject.transform.position = unitInitPos.transform.position;
        }
    }
}