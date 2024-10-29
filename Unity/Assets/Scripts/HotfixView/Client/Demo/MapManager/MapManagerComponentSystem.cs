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

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MapManager");

            foreach (var gameObject in gameObjects)
            {
                ColliderAction mapCollider = gameObject.GetComponent<ColliderAction>();

                mapCollider.OnTriggerEnterAction = self.OnColliderEnter;

                mapCollider.OnTriggerExitAction = self.OnColliderExit;
            }
        }

        public static void OnColliderEnter(this MapManagerComponent self, GameObject gameObject)
        {
            string name = gameObject.name;

            string pattern = @"[^0-9]+";

            string nameString = System.Text.RegularExpressions.Regex.Replace(name, pattern, "");

            int number = int.Parse(nameString);

            Log.Debug($"show map scene {number}");

            MapConfig mapConfig = MapConfigCategory.Instance.Get(number);

            if (mapConfig == null)
            {
                return;
            }

            self.AddChildWithId<MapScene>(mapConfig.Id);
        }

        public static void OnColliderExit(this MapManagerComponent self, GameObject gameObject)
        {
            string name = gameObject.name;

            string pattern = @"[^0-9]+";

            string nameString = System.Text.RegularExpressions.Regex.Replace(name, pattern, "");

            int number = int.Parse(nameString);

            Log.Debug($"dispose map scene {number}");

            MapConfig mapConfig = MapConfigCategory.Instance.Get(number);

            if (mapConfig != null)
            {
                MapScene mapScene = self.GetChild<MapScene>(mapConfig.Id);

                mapScene.Dispose();
            }

            // EventSystem.Instance.Publish(self.Root(), new ExitMapCollider() { Unit = unit, ColliderName = collider.gameObject.name });
        }
    }
}