using UnityEditor.Rendering.LookDev;

namespace ET.Client
{
    public static class FightDataHelper
    {
        public static float Fight(FightDataComponent attackDataComponent, FightDataComponent beAttackDataComponent)
        {
            float attack = attackDataComponent.GetValueByType(WordBarType.Attack);

            Log.Debug($"FightDataHelper fight {attack}");

            float hp = beAttackDataComponent.CurrentHP;

            Log.Debug($"current hp {hp}");
            
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
    }
}