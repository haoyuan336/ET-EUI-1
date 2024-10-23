using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [EntitySystemOf(typeof(MapScene))]
    public static partial class MapSceneSystem
    {
        [EntitySystem]
        public static void Destroy(this MapScene self)
        {
            MapConfig mapConfig = MapConfigCategory.Instance.Get((int)self.Id);

            Unit unit = self.Parent.GetParent<Unit>();

            EventSystem.Instance.Publish(self.Root(), new HideEnemySpawnPos() { Unit = unit, MapConfig = mapConfig });

            SceneManager.UnloadSceneAsync(mapConfig.SceneName);
        }

        [EntitySystem]
        public static async void Awake(this MapScene self)
        {
            MapConfig mapConfig = MapConfigCategory.Instance.Get((int)self.Id);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(mapConfig.SceneName, LoadSceneMode.Additive);

            await asyncOperation.GetAwaiter();

            Unit unit = self.Parent.GetParent<Unit>();

            EventSystem.Instance.Publish(self.Root(), new ShowEnemySpawnPos() { Unit = unit, MapConfig = mapConfig });
        }
    }
}