using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class BindEnemySpawnPosEventHandler : AEvent<Scene, BindEnemySpawnPos>
    {
        protected override async ETTask Run(Scene scene, BindEnemySpawnPos a)
        {
            Unit unit = a.Unit;

            MapConfig mapConfig = a.MapConfig;

            UnityEngine.SceneManagement.Scene gameScene = SceneManager.GetSceneByName(mapConfig.SceneName);

            GameObject[] gameObjects = gameScene.GetRootGameObjects();

            List<GameObject> list = new List<GameObject>();

            foreach (var gameObject in gameObjects)
            {
                Log.Debug($"gameobject {gameObject.name} {gameObject.tag}");

                if (gameObject.CompareTag("EnemySpawnPos"))
                {
                    list.Add(gameObject);
                }
            }

            Log.Debug($"game object count {list.Count}");

            EnemySpawnPosComponent enemySpawnPosComponent = unit.GetComponent<EnemySpawnPosComponent>();

            if (enemySpawnPosComponent == null)
            {
                enemySpawnPosComponent = unit.AddComponent<EnemySpawnPosComponent>();
            }

            foreach (var gameObject in list)
            {
                string name = gameObject.name;

                string pattern = @"[^0-9]+";

                string nameString = System.Text.RegularExpressions.Regex.Replace(name, pattern, "");

                int number = int.Parse(nameString);

                EnemySpawnPos enemySpawnPos = enemySpawnPosComponent.GetChild<EnemySpawnPos>(number);

                if (enemySpawnPos == null)
                {
                    enemySpawnPos = enemySpawnPosComponent.AddChildWithId<EnemySpawnPos, string>(number, name);
                }

                enemySpawnPos.BindObject(gameObject);
            }
        }
    }
}