using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class UnitUpLevelExpConfigCategory : Singleton<UnitUpLevelExpConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, UnitUpLevelExpConfig> dict = new();
		
        public void Merge(object o)
        {
            UnitUpLevelExpConfigCategory s = o as UnitUpLevelExpConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public UnitUpLevelExpConfig Get(int id)
        {
            this.dict.TryGetValue(id, out UnitUpLevelExpConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (UnitUpLevelExpConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, UnitUpLevelExpConfig> GetAll()
        {
            return this.dict;
        }

        public UnitUpLevelExpConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            
            var enumerator = this.dict.Values.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current; 
        }
    }

	public partial class UnitUpLevelExpConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>需要的经验值</summary>
		public int NeedExp { get; set; }
		/// <summary>等级</summary>
		public int NextLevel { get; set; }

	}
}
