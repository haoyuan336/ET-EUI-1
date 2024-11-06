using System.Diagnostics;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(EnemySpawnPos))]
    public static partial class EnemySpawnPosSystem
    {
        [EntitySystem]
        public static void Awake(this EnemySpawnPos self, string spawnName)
        {
            self.TimerComponent = self.Root().GetComponent<TimerComponent>();

            // self.EnemySpawnPosName = spawnName;
        }

        public static void BindObject(this EnemySpawnPos self, GameObject gameObject)
        {
            self.GameObject = gameObject;

            ColliderAction colliderAction = self.GameObject.GetComponent<ColliderAction>();

            colliderAction.OnTriggerEnterAction = self.OnTriggerEnter;

            colliderAction.OnTriggerExitAction = self.OnTriggerExit;

            self.EnterState(EnemySpawnState.Sleep);
        }

        public static void EnterState(this EnemySpawnPos self, EnemySpawnState state)
        {
            if (state == EnemySpawnState.Running)
            {
                self.SpawnEnemy();

                self.MakeEnemyToRuning();
            }

            if (state == EnemySpawnState.Sleep)
            {
                self.MakeEnemyToSleep();
            }

            self.CurrentSpawnState = state;
        }

        public static void MakeEnemyToRuning(this EnemySpawnPos self)
        {
            foreach (var kv in self.Enemies)
            {
                EventSystem.Instance.Publish(self.Root(), new MakeEnemyRunning() { Enemy = kv.Value });
            }
        }

        public static void MakeEnemyToSleep(this EnemySpawnPos self)
        {
            foreach (var kv in self.Enemies)
            {
                EventSystem.Instance.Publish(self.Root(), new MakeEnemySleep() { Enemy = kv.Value });
            }
        }

        [EntitySystem]
        public static void Update(this EnemySpawnPos self)
        {
            if (self.CurrentSpawnState == EnemySpawnState.Running)
            {
                if (TimeInfo.Instance.ClientNow() - self.SpawnEnemyTime > self.Config.TimeInterval)
                {
                    self.SpawnEnemy();
                }
            }
        }

        private static void OnTriggerEnter(this EnemySpawnPos self, GameObject gameObject, GameObject otherObject)
        {
            Log.Debug($"OnTriggerEnter {gameObject.name}");

            self.EnterState(EnemySpawnState.Running);
        }

        private static void OnTriggerExit(this EnemySpawnPos self, GameObject gameObject, GameObject otherObject)
        {
            self.EnterState(EnemySpawnState.Sleep);
        }
        public static void SpawnEnemy(this EnemySpawnPos self)
        {
            EnemySpawnPosConfig config = EnemySpawnPosConfigCategory.Instance.Get((int)self.Id);

            int spawnCount = config.SpawnCount - self.Enemies.Count;

            Unit unit = self.Parent.GetParent<Unit>();

            for (int i = 0; i < spawnCount; i++)
            {
                EventSystem.Instance.Publish(self.Root(), new SpawnOneEnemy() { Unit = unit, EnemySpawnPos = self });
            }

            self.SpawnEnemyTime = TimeInfo.Instance.ClientNow();
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