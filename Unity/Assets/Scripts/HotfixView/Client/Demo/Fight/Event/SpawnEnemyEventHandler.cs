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

            await ETTask.CompletedTask;
        }
    }
}