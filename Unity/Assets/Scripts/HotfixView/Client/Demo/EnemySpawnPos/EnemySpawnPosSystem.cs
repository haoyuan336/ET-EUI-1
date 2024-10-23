namespace ET.Client
{
    [EntitySystemOf(typeof(EnemySpawnPos))]
    public static partial class EnemySpawnPosSystem
    {
        [EntitySystem]
        public static void Awake(this EnemySpawnPos self, string spawnName)
        {
            self.TimerComponent = self.Root().GetComponent<TimerComponent>();

            self.EnemySpawnPosName = spawnName;
        }

        public static void Hide(this EnemySpawnPos self)
        {
            self.IsShow = false;

            foreach (var enemy in self.Enemies)
            {
                enemy.Value.Hide();
            }
        }

        public static async void Show(this EnemySpawnPos self)
        {
            self.IsShow = true;

            foreach (var enemy in self.Enemies)
            {
                enemy.Value.Show();
            }

            while (self.IsShow)
            {
                self.SpawnEnemy();

                await self.TimerComponent.WaitAsync(5000);
            }
        }

        public static void SpawnEnemy(this EnemySpawnPos self)
        {
            Log.Debug("spawn enemy");
            EnemySpawnPosConfig config = EnemySpawnPosConfigCategory.Instance.Get((int)self.Id);

            int spawnCount = config.SpawnCount - self.Enemies.Count;

            Log.Debug($"spawn count {spawnCount}");
            Unit unit = self.Parent.GetParent<Unit>();

            for (int i = 0; i < spawnCount; i++)
            {
                EventSystem.Instance.Publish(self.Root(), new SpawnOneEnemy() { Unit = unit, EnemySpawnPos = self });
            }
        }

        public static void Add(this EnemySpawnPos self, Enemy enemy)
        {
            self.Enemies.Add(enemy.Id, enemy);
        }

        public static void Remove(this EnemySpawnPos self, long enemyId)
        {
            self.Enemies.Remove(enemyId);
        }
    }
}