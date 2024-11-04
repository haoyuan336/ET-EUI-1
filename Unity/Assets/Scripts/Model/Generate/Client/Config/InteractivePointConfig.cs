using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class InteractivePointConfigCategory : Singleton<InteractivePointConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, InteractivePointConfig> dict = new();
		
        public void Merge(object o)
        {
            InteractivePointConfigCategory s = o as InteractivePointConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public InteractivePointConfig Get(int id)
        {
            this.dict.TryGetValue(id, out InteractivePointConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (InteractivePointConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, InteractivePointConfig> GetAll()
        {
            return this.dict;
        }

        public InteractivePointConfig GetOne()
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

	public partial class InteractivePointConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>交互点名称</summary>
		public string InteractivePointName { get; set; }
		/// <summary>交互点类型</summary>
		public string InteractveType { get; set; }

	}
}
