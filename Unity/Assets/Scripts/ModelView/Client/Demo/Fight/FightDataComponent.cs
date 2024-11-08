using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Entity))]
    public class FightDataComponent : Entity, IAwake<Enemy>, IAwake<HeroCard>
    {
        public Dictionary<string, float> Datas = new Dictionary<string, float>();

        public float CurrentHP;

        public HeroConfig HeroConfig;
        
    }
}