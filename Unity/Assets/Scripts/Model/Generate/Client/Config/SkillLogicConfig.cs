using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class SkillLogicConfigCategory : Singleton<SkillLogicConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, SkillLogicConfig> dict = new();
		
        public void Merge(object o)
        {
            SkillLogicConfigCategory s = o as SkillLogicConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public SkillLogicConfig Get(int id)
        {
            this.dict.TryGetValue(id, out SkillLogicConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (SkillLogicConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, SkillLogicConfig> GetAll()
        {
            return this.dict;
        }

        public SkillLogicConfig GetOne()
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

	public partial class SkillLogicConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>所属技能</summary>
		public int OwnerSkillConfigId { get; set; }
		/// <summary>延迟时间</summary>
		public int DelayTime { get; set; }
		/// <summary>触发逻辑</summary>
		public string LogicCode { get; set; }
		/// <summary>逻辑参数</summary>
		public string LogicParam { get; set; }
		/// <summary>数值参数</summary>
		public int DataParam { get; set; }
		/// <summary>等级</summary>
		public int Level { get; set; }

	}
}
