using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    public enum EnemySpawnState
    {
        Running = 1,
        Sleep = 2
    }

    [ChildOf(typeof(EnemySpawnPosComponent))]
    public class EnemySpawnPos : Entity, IAwake<string>, IUpdate
    {
        // public bool IsShow = false;

        public TimerComponent TimerComponent;

        public Dictionary<long, Enemy> Enemies = new Dictionary<long, Enemy>();
        public EnemySpawnPosConfig Config => EnemySpawnPosConfigCategory.Instance.Get((int)this.Id);

        // public string EnemySpawnPosName;

        public GameObject GameObject;

        public EnemySpawnState CurrentSpawnState;

        public long SpawnEnemyTime = 0;
    }
}