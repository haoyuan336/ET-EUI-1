namespace ET.Client
{
    [EntitySystemOf(typeof(Enemy))]
    public static partial class EnemySystem
    {
        [EntitySystem]
        public static void Awake(this Enemy self, int configId, EnemySpawnPos enemySpawnPos)
        {
            self.ConfigId = configId;

            self.EnemySpawnPos = enemySpawnPos;
        }

        // public static void Show(this Enemy self)
        // {
        //     EventSystem.Instance.Publish(self.Root(), new ShowEnemyObject() { Enemy = self });
        // }
        //
        // public static void Hide(this Enemy self)
        // {
        //     EventSystem.Instance.Publish(self.Root(), new HideEnemyObject() { Enemy = self });
        // }
    }
}