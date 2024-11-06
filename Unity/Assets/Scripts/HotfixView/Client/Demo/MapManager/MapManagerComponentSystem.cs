using System.Text.RegularExpressions;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(MapManagerComponent))]
    public static partial class MapManagerComponentSystem
    {
        [EntitySystem]
        public static void Awake(this MapManagerComponent self)
        {
            //找出地图里面所有的场景碰撞器

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MapCollider");

            // foreach (var gameObject in gameObjects)
            // {
            //     ColliderAction mapCollider = gameObject.GetComponent<ColliderAction>();
            //
            //     mapCollider.OnTriggerEnterAction = self.OnColliderEnter;
            //
            //     mapCollider.OnTriggerExitAction = self.OnColliderExit;
            // }

            for (int i = 0; i < gameObjects.Length; i++)
            {
                GameObject gameObject = gameObjects[i];

                long number = GetStringNumberHelper.GetLong(gameObject.name);

                self.AddChildWithId<MapScene, GameObject>(number, gameObject);
            }
        }

        // public static async void OnColliderEnter(this MapManagerComponent self, GameObject gameObject)
        // {
        //     string name = gameObject.name;
        //
        //     int number = GetStringNumberHelper.GetNumber(name);
        //
        //     Log.Debug($"show map scene {number}");
        //
        //     await EventSystem.Instance.PublishAsync(self.Root(), new LoadMapScene() { MapConfigId = number });
        // }
        //
        // public static void OnColliderExit(this MapManagerComponent self, GameObject gameObject)
        // {
        //     string name = gameObject.name;
        //
        //     int number = GetStringNumberHelper.GetNumber(name);
        //
        //     EventSystem.Instance.Publish(self.Root(), new UnLoadMapScene() { MapConfigId = number });
        // }
    }
}