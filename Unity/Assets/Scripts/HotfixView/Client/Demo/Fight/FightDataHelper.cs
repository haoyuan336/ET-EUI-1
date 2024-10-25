namespace ET.Client
{
    public static class FightDataHelper
    {
        public static float Fight(FightDataComponent attackDataComponent, FightDataComponent beAttackDataComponent)
        {
            float attack = attackDataComponent.GetValueByType(WordBarType.Attack);

            float hp = beAttackDataComponent.GetValueByType(WordBarType.Hp);

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