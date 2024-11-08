using System.Drawing;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;

namespace ET.Client
{
    [EntitySystemOf(typeof(ObjectComponent))]
    public static partial class ObjectComponentSystem
    {
        [EntitySystem]
        public static void Destroy(this ObjectComponent self)
        {
            if (self.GameObject != null)
            {
                GameObject.Destroy(self.GameObject);

                self.GameObject = null;
            }
        }

        [EntitySystem]
        public static void Awake(this ObjectComponent self, GameObject prefab, Vector3 position)
        {
            self.Prefab = prefab;

            self.CreateObject();

            self.InitPos = position;

            self.GameObject.transform.position = position;

            AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

            aiComponent.EnterStateAction += self.OnEnterState;
        }

        private static bool CreateObject(this ObjectComponent self)
        {
            if (self.GameObject != null)
            {
                return false;
            }

            GameObject gameObject = UnityEngine.Object.Instantiate(self.Prefab);

            self.GameObject = gameObject;

            // self.GameObject.transform.position = self.InitPos;

            self.GameObject.name = "hero" + self.Parent.Id;

            self.NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();

            self.Body = self.GameObject.transform.GetChild(0).gameObject;

            self.Body.transform.rotation = Camera.main.transform.rotation;

            self.SkeletonAnimation = self.Body.GetComponent<SkeletonAnimation>();

            ColliderAction colliderAction = gameObject.GetComponent<ColliderAction>();

            if (colliderAction != null)
            {
                colliderAction.OnTriggerEnterAction = self.OnTriggerEnter;

                colliderAction.OnTriggerExitAction = self.OnTriggerExit;
            }

            return true;
        }

        private static void OnTriggerEnter(this ObjectComponent self, GameObject gameObject, GameObject otherObject)
        {
            Log.Debug($"object component enter {otherObject.name}");
            if (otherObject.CompareTag("TeleportFence"))
            {
                AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

                aiComponent.InSafeArea = true;
            }

            Log.Debug($"object component enter {self.Parent.GetComponent<AIComponent>().InSafeArea}");
        }

        private static void OnTriggerExit(this ObjectComponent self, GameObject gameObject, GameObject otherObject)
        {
            Log.Debug($"object component exit {otherObject.name}");
            if (otherObject.CompareTag("TeleportFence"))
            {
                AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

                aiComponent.InSafeArea = false;
                
                
            }
        }

        [EntitySystem]
        public static void Update(this ObjectComponent self)
        {
            if (self.Body != null)
            {
                self.Body.transform.rotation = Camera.main.transform.rotation;
            }
        }

        public static void OnEnterState(this ObjectComponent self, AIState state)
        {
            if (state == AIState.Sleep)
            {
                self.MakeSleep();
            }
        }

        public static Vector3 GetHeadPos(this ObjectComponent self)
        {
            if (self.GameObject == null)
            {
                return Vector3.zero;
            }

            Bounds bounds = self.GameObject.GetComponent<Collider>().bounds;

            return self.GameObject.transform.position + bounds.size.y * 1.5f * Vector3.up;
        }

        public static void MakeRunning(this ObjectComponent self)
        {
            bool isNew = self.CreateObject();

            if (isNew)
            {
                self.GameObject.transform.position = self.InitPos;
            }

            AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();

            animComponent.PlayAnim("move");
        }

        public static void MakeSleep(this ObjectComponent self)
        {
            if (self.GameObject != null)
            {
                // self.LastPos = self.GameObject.transform.position;

                GameObject.Destroy(self.GameObject);
            }
        }
    }
}