using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ComponentOf(typeof (Scene))]
    public class ServerManagerComponent: Entity, IAwake, ISerializeToEntity
    {
        [BsonIgnore]
        public bool IsChanged = false;

        [BsonIgnore]
        public long TimerId = 0;
    }
}