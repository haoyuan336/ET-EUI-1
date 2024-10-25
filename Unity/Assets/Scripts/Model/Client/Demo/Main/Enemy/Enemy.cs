namespace ET.Client
{
    [ChildOf(typeof(FightManagerComponent))]
    public class Enemy : Entity, IAwake<int, EnemySpawnPos>
    {
        public int ConfigId;

        public EnemySpawnPos EnemySpawnPos;
        public EnemyConfig Config => EnemyConfigCategory.Instance.Get(this.ConfigId);
    }
}