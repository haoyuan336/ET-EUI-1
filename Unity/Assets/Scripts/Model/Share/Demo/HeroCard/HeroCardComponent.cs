using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Unit))]
#if UNITY
    public class HeroCardComponent : Entity, IAwake, ITransfer
#else
    public class HeroCardComponent: Entity, IAwake, ITransfer, IUnitCache
#endif
    {
#if UNITY

        public List<HeroCard> FormationHeroCards = new List<HeroCard>();
#endif
    }
}