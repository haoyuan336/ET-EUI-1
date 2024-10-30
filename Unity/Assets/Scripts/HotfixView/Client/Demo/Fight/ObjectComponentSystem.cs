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

            self.InitPos = position;

            self.CreateObject();

            AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

            aiComponent.EnterStateAction += self.OnEnterState;
        }

        private static void CreateObject(this ObjectComponent self)
        {
            if (self.GameObject != null)
            {
                return;
            }

            GameObject gameObject = UnityEngine.Object.Instantiate(self.Prefab);

            self.GameObject = gameObject;

            self.GameObject.transform.position = self.InitPos;

            self.GameObject.name = "hero" + self.Parent.Id;

            self.NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();

            self.Body = self.GameObject.transform.GetChild(0).gameObject;

            self.Body.transform.rotation = Camera.main.transform.rotation;

            self.SkeletonAnimation = self.Body.GetComponent<SkeletonAnimation>();
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
            self.CreateObject();
        }

        public static void MakeSleep(this ObjectComponent self)
        {
            GameObject.Destroy(self.GameObject);
        }
    }
}