using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class HeroConfigCategory : Singleton<HeroConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, HeroConfig> dict = new();
		
        public void Merge(object o)
        {
            HeroConfigCategory s = o as HeroConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public HeroConfig Get(int id)
        {
            this.dict.TryGetValue(id, out HeroConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (HeroConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, HeroConfig> GetAll()
        {
            return this.dict;
        }

        public HeroConfig GetOne()
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

	public partial class HeroConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>预制体名字</summary>
		public string PrefabName { get; set; }
		/// <summary>受击动作</summary>
		public string BeAttackAnim { get; set; }
		/// <summary>英雄名称</summary>
		public string HeroName { get; set; }
		/// <summary>生命值基础之</summary>
		public float Hp { get; set; }
		/// <summary>生命值成长系数</summary>
		public float HpGrow { get; set; }
		/// <summary>攻击力基础数值</summary>
		public float Attack { get; set; }
		/// <summary>攻击力成长系数</summary>
		public float AttackGrow { get; set; }
		/// <summary>移动速度</summary>
		public float MoveSpeed { get; set; }
		/// <summary>最大攻击距离</summary>
		public float MaxAttackDistance { get; set; }
		/// <summary>最大跟踪距离</summary>
		public float MaxTrackDistance { get; set; }

	}
}
