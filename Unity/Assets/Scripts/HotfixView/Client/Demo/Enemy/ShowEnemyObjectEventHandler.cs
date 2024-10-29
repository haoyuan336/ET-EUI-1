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

            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(enemy.Config.HeroConfigId);

            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(heroConfig.PrefabName);

            EnemySpawnPos enemySpawnPos = enemy.EnemySpawnPos;

            string enemySpawnPosName = enemySpawnPos.EnemySpawnPosName;

            GameObject enemySpawnObject = GameObject.Find(enemySpawnPosName);

            Vector3 endPos = enemySpawnObject.transform.position + Quaternion.Euler(0, RandomGenerator.RandomNumber(0, 360), 0) * Vector3.forward;

            enemy.AddComponent<ObjectComponent, GameObject, Vector3>(prefab, endPos);

            int layer = LayerMask.GetMask("Hero");

            TimerComponent timerComponent = scene.Root().GetComponent<TimerComponent>();

            AIComponent aiComponent = enemy.AddComponent<AIComponent>();

            enemy.AddComponent<PatrolComponent, Vector3, int>(endPos, layer);

            enemy.AddComponent<TrackComponent>();

            enemy.AddComponent<AttackComponent>();

            enemy.AddComponent<FightDataComponent, Enemy>(enemy);

            int level = enemy.Config.Level;

            enemy.AddComponent<SkillComponent, int, int>(enemy.ConfigId, level);

            enemy.AddComponent<AnimComponent>();

            enemy.AddComponent<MoveObjectComponent>();

            // enemy.AddComponent<FindEnemyComponent, int>(layer);

            await timerComponent.WaitAsync(1000);

            aiComponent.EnterAIState(AIState.Patrol);

            await ETTask.CompletedTask;
        }
    }
}