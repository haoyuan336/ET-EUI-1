using System.Collections.Generic;

namespace ET.Client
{
    public class FightManagerComponent : Entity, IAwake
    {
        public List<HeroCard> HeroCards = new List<HeroCard>();
    }
}