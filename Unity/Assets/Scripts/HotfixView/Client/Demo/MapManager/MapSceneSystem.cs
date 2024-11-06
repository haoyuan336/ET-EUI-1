using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [EntitySystemOf(typeof(MapScene))]
    public static partial class MapSceneSystem
    {
        [EntitySystem]
        public static void Awake(this MapScene self, GameObject gameObject)
        {
            ColliderAction colliderAction = gameObject.GetComponent<ColliderAction>();

            colliderAction.OnTriggerEnterAction = self.OnTriggerEnter;

            colliderAction.OnTriggerExitAction = self.OnTriggerExit;
        }

        public static async void OnTriggerEnter(this MapScene self, GameObject gameObject, GameObject otherObject)
        {
            Log.Debug($"on trigger enter {self.Id}");

            await self.LoadScene();
        }

        public static void OnTriggerExit(this MapScene self, GameObject gameObject, GameObject otherObject)
        {
            self.UnLoadScene();
        }

        [EntitySystem]
        public static void Destroy(this MapScene self)
        {
        }

        public static async ETTask LoadScene(this MapScene self)
        {
            if (self.IsLoaded)
            {
                return;
            }

            self.IsLoaded = true;

            MapConfig mapConfig = MapConfigCategory.Instance.Get((int)self.Id);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(mapConfig.SceneName, LoadSceneMode.Additive);

            await asyncOperation.GetAwaiter();

            Unit unit = UnitHelper.GetMyUnit(self.Root());

            EventSystem.Instance.Publish(self.Root(), new BindEnemySpawnPos() { Unit = unit, MapConfig = mapConfig });

            EventSystem.Instance.Publish(self.Root(), new BindTreeObject()
            {
                MapConfig = mapConfig
            });

            EventSystem.Instance.Publish(self.Root(), new LoadMapSceneFinish() { MapConfigId = mapConfig.Id });
        }

        public static void UnLoadScene(this MapScene self)
        {
            if (!self.IsLoaded)
            {
                return;
            }

            self.IsLoaded = false;

            MapConfig mapConfig = MapConfigCategory.Instance.Get((int)self.Id);

            Log.Debug($"unload scene {mapConfig.SceneName}");

            SceneManager.UnloadSceneAsync(mapConfig.SceneName);
        }
    }
}