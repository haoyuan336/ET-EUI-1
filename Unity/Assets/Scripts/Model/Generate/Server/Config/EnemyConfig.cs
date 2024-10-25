using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class EnemyConfigCategory : Singleton<EnemyConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, EnemyConfig> dict = new();
		
        public void Merge(object o)
        {
            EnemyConfigCategory s = o as EnemyConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public EnemyConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EnemyConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EnemyConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, EnemyConfig> GetAll()
        {
            return this.dict;
        }

        public EnemyConfig GetOne()
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

	public partial class EnemyConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>英雄名称</summary>
		public string Name { get; set; }
		/// <summary>对应模型</summary>
		public int HeroConfigId { get; set; }
		/// <summary>等级</summary>
		public int Level { get; set; }
		/// <summary>星级</summary>
		public int Star { get; set; }

	}
}
