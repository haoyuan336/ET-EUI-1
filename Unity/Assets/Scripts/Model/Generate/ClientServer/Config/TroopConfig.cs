using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class TroopConfigCategory : Singleton<TroopConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, TroopConfig> dict = new();
		
        public void Merge(object o)
        {
            TroopConfigCategory s = o as TroopConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public TroopConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TroopConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TroopConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TroopConfig> GetAll()
        {
            return this.dict;
        }

        public TroopConfig GetOne()
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

	public partial class TroopConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>队伍类型</summary>
		public string TroopType { get; set; }
		/// <summary>队伍认数</summary>
		public int TroopCount { get; set; }

	}
}
