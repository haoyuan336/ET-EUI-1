using UnityEditor.Rendering.LookDev;
using WeChatWASM;

namespace ET.Client
{
    public static class FightDataHelper
    {
        public static float Fight(FightDataComponent attackDataComponent, FightDataComponent beAttackDataComponent)
        {
            float attack = attackDataComponent.GetValueByType(WordBarType.Attack);

            float hp = beAttackDataComponent.CurrentHP;

            float damage = 0;

            if (hp < attack)
            {
                damage = hp;
            }
            else
            {
                damage = attack;
            }

            return damage;
        }

        public static bool GetIsDead(Entity entity)
        {
            AIComponent aiComponent = entity.GetComponent<AIComponent>();

            return aiComponent.GetCurrentState() == AIState.Death;
        }

        public static bool GetCanAttack(Entity entity)
        {
            AIComponent aiComponent = entity.GetComponent<AIComponent>();

            AIState aiState = aiComponent.GetCurrentState();

            return !(aiState == AIState.Death || aiState == AIState.Transfer || aiComponent.InSafeArea);
        }

        public static bool GetIsDead(FightManagerComponent fightManagerComponent, long entityId)
        {
            Entity entity = fightManagerComponent.GetChild<Entity>(entityId);

            if (entity == null || entity.IsDisposed)
            {
                return true;
            }

            FightDataComponent fightDataComponent = entity.GetComponent<FightDataComponent>();

            return fightDataComponent.CurrentHP <= 0;
        }

        public static long GetIdByGameObjectName(string name)
        {
            long heroId = GetStringNumberHelper.GetLong(name);

            return heroId;
        }
    }
}