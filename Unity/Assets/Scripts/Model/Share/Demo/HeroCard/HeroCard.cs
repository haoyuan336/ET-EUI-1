using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ChildOf(typeof(HeroCardComponent))]

#if UNITY
    public class HeroCard : Entity, IAwake, IAwake<int>
#else
    public class HeroCard: Entity, IAwake<int>, ISerializeToEntity
#endif
    {
        public int HeroConfigId { get; set; }

        [BsonIgnore]
        public HeroConfig Config => HeroConfigCategory.Instance.Get(this.HeroConfigId);

        public Dictionary<string, float> Datas = new Dictionary<string, float>();
    }
}