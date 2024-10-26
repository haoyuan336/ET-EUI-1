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
        }
    }
}