using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class TreeConfigCategory : Singleton<TreeConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, TreeConfig> dict = new();
		
        public void Merge(object o)
        {
            TreeConfigCategory s = o as TreeConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public TreeConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TreeConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TreeConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TreeConfig> GetAll()
        {
            return this.dict;
        }

        public TreeConfig GetOne()
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

	public partial class TreeConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>奖励道具类型</summary>
		public int AwardConfigId { get; set; }
		/// <summary>奖励道具数量</summary>
		public int AwardCount { get; set; }
		/// <summary>血量</summary>
		public int HP { get; set; }
		/// <summary>复活时间</summary>
		public int RiseTime { get; set; }

	}
}
