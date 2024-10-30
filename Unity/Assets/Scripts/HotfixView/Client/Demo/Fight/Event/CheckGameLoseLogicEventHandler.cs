using System.Collections.Generic;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class CheckGameLoseLogicEventHandler : AEvent<Scene, CheckGameLoseLogic>
    {
        protected override async ETTask Run(Scene scene, CheckGameLoseLogic a)
        {
            bool isHero = a.Entity is HeroCard;

            if (!isHero)
            {
                await this.RemoveEnemyFromSpawn(a.Entity);

                return;
            }

            Unit unit = UnitHelper.GetMyUnit(scene);

            FightManagerComponent fightManagerComponent = unit.GetComponent<FightManagerComponent>();

            List<Troop> troops = TroopHelper.GetTroops(scene);

            Troop troop = troops[0];

            bool isAllDead = true;

            for (int i = 0; i < troop.HeroCardIds.Length; i++)
            {
                long cardId = troop.HeroCardIds[i];

                HeroCard heroCard = fightManagerComponent.GetChild<HeroCard>(cardId);

                if (heroCard == null)
                {
                    continue;
                }

                AIComponent aiComponent = heroCard.GetComponent<AIComponent>();

                if (aiComponent.GetCurrentState() != AIState.Death)
                {
                    isAllDead = false;

                    break;
                }
            }

            Log.Debug($"is all dead {isAllDead}");
            if (isAllDead)
            {
                EventSystem.Instance.Publish(scene, new ShowLayerById() { WindowID = WindowID.GameLoseLayer });
            }
        }

        public async ETTask RemoveEnemyFromSpawn(Entity entity)
        {
            Enemy enemy = entity as Enemy;

            long enemyId = enemy.Id;

            TimerComponent timerComponent = entity.Root().GetComponent<TimerComponent>();

            await timerComponent.WaitAsync(1000);

            enemy.Dispose();

            EnemySpawnPos enemySpawnPos = enemy.EnemySpawnPos;

            if (enemySpawnPos != null && !enemySpawnPos.IsDisposed)
            {
                enemySpawnPos.Remove(enemyId);
            }
        }
    }
}