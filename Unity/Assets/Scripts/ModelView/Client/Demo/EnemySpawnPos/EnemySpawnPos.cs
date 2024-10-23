using System.Collections.Generic;

namespace ET.Client
{
    [ChildOf(typeof(EnemySpawnPosComponent))]
    public class EnemySpawnPos : Entity, IAwake<string>
    {
        public bool IsShow = false;

        public TimerComponent TimerComponent;

        public Dictionary<long, Enemy> Enemies = new Dictionary<long, Enemy>();
        public EnemySpawnPosConfig Config => EnemySpawnPosConfigCategory.Instance.Get((int)this.Id);

        public string EnemySpawnPosName;
    }
}