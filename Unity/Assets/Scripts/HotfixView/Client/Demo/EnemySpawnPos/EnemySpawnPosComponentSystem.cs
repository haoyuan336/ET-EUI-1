namespace ET.Client
{
    [EntitySystemOf(typeof(EnemySpawnPosComponent))]
    public static partial class EnemySpawnPosComponentSystem
    {
        [EntitySystem]
        public static void Awake(this EnemySpawnPosComponent self)
        {
            
        }
        
    }
}