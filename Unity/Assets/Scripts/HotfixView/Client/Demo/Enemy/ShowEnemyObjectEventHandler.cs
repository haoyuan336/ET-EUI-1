using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class ShowEnemyObjectEventHandler : AEvent<Scene, ShowEnemyObject>
    {
        protected override async ETTask Run(Scene scene, ShowEnemyObject a)
        {
            Enemy enemy = a.Enemy;

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(enemy.Config.PrefabName);

            EnemySpawnPos enemySpawnPos = enemy.EnemySpawnPos;

            string enemySpawnPosName = enemySpawnPos.EnemySpawnPosName;

            GameObject enemySpawnObject = GameObject.Find(enemySpawnPosName);

            Vector3 endPos = enemySpawnObject.transform.position + Quaternion.Euler(0, RandomGenerator.RandomNumber(0, 360), 0) * Vector3.forward;

            enemy.AddComponent<ObjectComponent, GameObject, Vector3>(prefab, endPos);

            enemy.AddComponent<MoveObjectComponent>();

            int layer = LayerMask.GetMask("Hero");

            TimerComponent timerComponent = scene.Root().GetComponent<TimerComponent>();

            AIComponent aiComponent = enemy.AddComponent<AIComponent>();

            enemy.AddComponent<PatrolComponent, Vector3, int>(endPos, layer);

            enemy.AddComponent<TrackComponent>();

            enemy.AddComponent<AttackComponent>();

            int level = enemy.Config.Level;

            enemy.AddComponent<SkillComponent, int, int>(enemy.ConfigId, level);

            await timerComponent.WaitAsync(1000);

            aiComponent.EnterAIState(AIState.Patrol);

            await ETTask.CompletedTask;
        }
    }
}