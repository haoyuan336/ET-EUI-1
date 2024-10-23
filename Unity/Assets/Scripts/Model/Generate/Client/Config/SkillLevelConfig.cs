using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class SkillLevelConfigCategory : Singleton<SkillLevelConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, SkillLevelConfig> dict = new();
		
        public void Merge(object o)
        {
            SkillLevelConfigCategory s = o as SkillLevelConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public SkillLevelConfig Get(int id)
        {
            this.dict.TryGetValue(id, out SkillLevelConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (SkillLevelConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, SkillLevelConfig> GetAll()
        {
            return this.dict;
        }

        public SkillLevelConfig GetOne()
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

	public partial class SkillLevelConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>技能1等级</summary>
		public int Skill1Level { get; set; }
		/// <summary>技能2等级</summary>
		public int Skill2Level { get; set; }
		/// <summary>技能3等级</summary>
		public int Skill3Level { get; set; }
		/// <summary>技能4等级</summary>
		public int Skill4Level { get; set; }

	}
}
