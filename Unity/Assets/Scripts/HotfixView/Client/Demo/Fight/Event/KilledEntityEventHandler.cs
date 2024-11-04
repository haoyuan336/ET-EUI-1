using NativeCollection;

namespace ET.Client
{
    public struct KilledEntity
    {
        public Entity AttackEntity;

        public Entity BeAttackEntity;
    }

    [Event(SceneType.Demo)]
    public class KilledEntityEventHandler : AEvent<Scene, KilledEntity>
    {
        protected override async ETTask Run(Scene scene, KilledEntity a)
        {
            Log.Debug("killed entity");
            TimerComponent timerComponent = scene.Root().GetComponent<TimerComponent>();

            Entity attackEntity = a.AttackEntity;

            Entity beAttackEntity = a.BeAttackEntity;

            if (beAttackEntity is Enemy)
            {
                Enemy enemy = beAttackEntity as Enemy;

                EnemyConfig enemyConfig = enemy.Config;

                int exp = enemyConfig.Exp;

                int meat = enemyConfig.Meat;

                ItemConfig itemConfig = ItemConfigCategory.Instance.GetConfigByType(ItemType.Meat);

                ItemHelper.AddItemCount(scene, itemConfig.Id, meat).Coroutine();

                UnitHelper.AddExp(scene, exp).Coroutine();

                EventSystem.Instance.Publish(scene, new PlayAddExpText() { BeAttackEntity = beAttackEntity, Exp = exp });

                await timerComponent.WaitFrameAsync();

                EventSystem.Instance.Publish(scene, new PlayAddMeat() { BeEntity = beAttackEntity, Count = meat });
            }

            await ETTask.CompletedTask;
        }
    }
}