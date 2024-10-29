using System.Drawing;
using UnityEngine;

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
            GameObject gameObject = UnityEngine.Object.Instantiate(prefab);

            self.GameObject = gameObject;

            self.GameObject.transform.position = position;

            self.GameObject.name = "hero" + self.Parent.Id;

            // ColliderAction colliderAction = self.GameObject.GetComponent<ColliderAction>();
            //
            // colliderAction.OnTriggerEnterAction = self.OnTriggerEnter;
            //
            // colliderAction.OnTriggerStayAction = self.OnTriggerStay;
            //
            // colliderAction.OnTriggerExitAction = self.OnTriggerExit;
        }

        // public static void OnTriggerEnter(this ObjectComponent self, GameObject gameObject)
        // {
        //     Log.Debug($"OnTriggerEnter {gameObject.name}");
        //     // EventSystem.Instance.Publish(self.Root(), new );
        //     EventSystem.Instance.Publish(self.Root(), new ColliderEnter()
        //     {
        //         Entity = self.Parent,
        //
        //         ColliderObject = gameObject
        //     });
        // }
        //
        // public static void OnTriggerStay(this ObjectComponent self, GameObject gameObject)
        // {
        // }
        //
        // public static void OnTriggerExit(this ObjectComponent self, GameObject gameObject)
        // {
        //     EventSystem.Instance.Publish(self.Root(), new ColliderExit()
        //     {
        //         Entity = self.Parent,
        //
        //         ColliderObject = gameObject
        //     });
        // }

        public static Vector3 GetHeadPos(this ObjectComponent self)
        {
            Bounds bounds = self.GameObject.GetComponent<Collider>().bounds;

            return self.GameObject.transform.position + bounds.size.y * 1.5f * Vector3.up;
        }
    }
}