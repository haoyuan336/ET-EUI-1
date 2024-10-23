using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class EnemySpawnPosConfigCategory : Singleton<EnemySpawnPosConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, EnemySpawnPosConfig> dict = new();
		
        public void Merge(object o)
        {
            EnemySpawnPosConfigCategory s = o as EnemySpawnPosConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public EnemySpawnPosConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EnemySpawnPosConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EnemySpawnPosConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, EnemySpawnPosConfig> GetAll()
        {
            return this.dict;
        }

        public EnemySpawnPosConfig GetOne()
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

	public partial class EnemySpawnPosConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>英雄名称</summary>
		public string Name { get; set; }
		/// <summary>包含的敌人配置id</summary>
		public int[] EnemyConfigId { get; set; }
		/// <summary>每批次刷新的怪物数量</summary>
		public int SpawnCount { get; set; }
		/// <summary>每批次刷新时间间隔</summary>
		public int TimeInterval { get; set; }

	}
}
