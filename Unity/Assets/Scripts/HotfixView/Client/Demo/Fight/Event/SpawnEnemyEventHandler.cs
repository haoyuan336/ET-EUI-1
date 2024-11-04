using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class SpawnEnemyEventHandler : AEvent<Scene, SpawnOneEnemy>
    {
        protected override async ETTask Run(Scene scene, SpawnOneEnemy a)
        {
            
            Unit unit = a.Unit;

            EnemySpawnPos enemySpawnPos = a.EnemySpawnPos;

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            if (fightManagerComponent == null)
            {
                fightManagerComponent = unit.AddComponent<FightManagerComponent>();
            }

            EnemySpawnPosConfig config = enemySpawnPos.Config;

            int[] enemyConfigIds = config.EnemyConfigId;

            int enemyConfigId = enemyConfigIds[0];

            Enemy enemy = fightManagerComponent.AddChild<Enemy, int, EnemySpawnPos>(enemyConfigId, enemySpawnPos);

            enemySpawnPos.Add(enemy);

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();
            //
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(enemy.Config.HeroConfigId);
            //
            GameObject prefab = globalComponent.ReferenceCollector.Get<GameObject>(heroConfig.PrefabName);
            //
            GameObject enemySpawnObject = enemySpawnPos.GameObject;
            //
            Vector3 endPos = enemySpawnObject.transform.position + Quaternion.Euler(0, RandomGenerator.RandomNumber(0, 360), 0) * Vector3.forward;
            //
            AIComponent aiComponent = enemy.AddComponent<AIComponent>();
            //
            enemy.AddComponent<ObjectComponent, GameObject, Vector3>(prefab, endPos);
            // //
            int layer = LayerMask.GetMask("Hero");
            //
            TimerComponent timerComponent = scene.Root().GetComponent<TimerComponent>();
            //
            enemy.AddComponent<PatrolComponent, Vector3>(endPos);
            //
            enemy.AddComponent<TrackComponent>();
            //
            enemy.AddComponent<AttackComponent>();
            //
            enemy.AddComponent<FightDataComponent, Enemy>(enemy);
            //
            int level = enemy.Config.Level;
            //
            enemy.AddComponent<SkillComponent, int, int>(enemy.ConfigId, level);
            //
            enemy.AddComponent<AnimComponent>();
            //
            enemy.AddComponent<MoveObjectComponent>();
            //
            enemy.AddComponent<FindEnemyComponent, int>(layer);
            //
            aiComponent.EnterAIState(AIState.Patrol);

            await ETTask.CompletedTask;
        }
    }
}