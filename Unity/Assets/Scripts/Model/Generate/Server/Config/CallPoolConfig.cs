using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class CallPoolConfigCategory : Singleton<CallPoolConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, CallPoolConfig> dict = new();
		
        public void Merge(object o)
        {
            CallPoolConfigCategory s = o as CallPoolConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public CallPoolConfig Get(int id)
        {
            this.dict.TryGetValue(id, out CallPoolConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CallPoolConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CallPoolConfig> GetAll()
        {
            return this.dict;
        }

        public CallPoolConfig GetOne()
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

	public partial class CallPoolConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>产出概率</summary>
		public int Rate { get; set; }
		/// <summary>对应英雄</summary>
		public string HeroName { get; set; }

	}
}
