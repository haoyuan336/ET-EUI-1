using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class WordBarConfigCategory : Singleton<WordBarConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, WordBarConfig> dict = new();
		
        public void Merge(object o)
        {
            WordBarConfigCategory s = o as WordBarConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public WordBarConfig Get(int id)
        {
            this.dict.TryGetValue(id, out WordBarConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (WordBarConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, WordBarConfig> GetAll()
        {
            return this.dict;
        }

        public WordBarConfig GetOne()
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

	public partial class WordBarConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>词条类型</summary>
		public string WordBarType { get; set; }
		/// <summary>数值类型</summary>
		public string NumberType { get; set; }
		/// <summary>属性类型</summary>
		public string WordAttributeType { get; set; }

	}
}
