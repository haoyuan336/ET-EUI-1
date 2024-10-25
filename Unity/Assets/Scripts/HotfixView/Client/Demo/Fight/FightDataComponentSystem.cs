using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ET.Client
{
    public enum WordAttributeType
    {
        BaseAttribute = 1, //基础属性
        SpecicalAttribute = 2, //特殊属性
        ElementAttribute = 3, //元素属性
    }

    // 这个可弄个配置表生成
    public enum WordBarType
    {
        // public const int Max = 10000;
        //
        // public const int Speed = 1000;
        // public const int SpeedBase = Speed * 10 + 1;
        // public const int SpeedAdd = Speed * 10 + 2;
        // public const int SpeedPct = Speed * 10 + 3;
        // public const int SpeedFinalAdd = Speed * 10 + 4;
        // public const int SpeedFinalPct = Speed * 10 + 5;
        //
        // public const int Hp = 1001;
        // public const int HpBase = Hp * 10 + 1; 
        //
        // public const int MaxHp = 1002;
        // public const int MaxHpBase = MaxHp * 10 + 1;
        // public const int MaxHpAdd = MaxHp * 10 + 2;
        // public const int MaxHpPct = MaxHp * 10 + 3;
        // public const int MaxHpFinalAdd = MaxHp * 10 + 4;
        // public const int MaxHpFinalPct = MaxHp * 10 + 5;
        //
        // public const int AOI = 1003;
        // public const int AOIBase = AOI * 10 + 1;
        // public const int AOIAdd = AOI * 10 + 2;
        // public const int AOIPct = AOI * 10 + 3;
        // public const int AOIFinalAdd = AOI * 10 + 4;
        // public const int AOIFinalPct = AOI * 10 + 5;

        Hp = 101,
        Attack = 102,
        BaoJiLv = 103,
        BaoShangJiaCheng = 104,
        MingZhongLv = 105,
        ShanBiLv = 106,
        XiXueLv = 107,
        XiXueJiaCheng = 108,
        KangBaoJiaLv = 109,
        KangBaoJiaCheng = 110,
        KangXiXueLv = 111,
        KangXiXueJiaCheng = 112,
        KangGeDangLv = 113,
        KangGeDangJiaCheng = 114,
        DuXiQiangDu = 115,
        DuXiKangXing = 116,
        HuoXiKangXing = 117,
        DianXiKangXing = 118,
        AnXiQiangDu = 119,
        AnXiKangXing = 120,
        XueXiKangXing = 121,
    }

    [EntitySystemOf(typeof(FightDataComponent))]
    public static partial class FightDataComponentSystem
    {
        [EntitySystem]
        public static void Awake(this FightDataComponent self, Enemy enemy)
        {
            EnemyConfig enemyConfig = enemy.Config;

            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(enemyConfig.HeroConfigId);

            int level = enemyConfig.Level;

            int star = enemyConfig.Star;

            Type heroConfigType = heroConfig.GetType();

            List<WordBarConfig> configs = WordBarConfigCategory.Instance.GetAll().Values.ToList();

            foreach (var wordBarConfig in configs)
            {
                string wordBarType = wordBarConfig.WordBarType;

                if (wordBarConfig.WordAttributeType == WordAttributeType.BaseAttribute.ToString())
                {
                    PropertyInfo info = heroConfigType.GetProperty($"{wordBarType}");

                    int value = Convert.ToInt32(info.GetValue(heroConfig));

                    PropertyInfo baseInfo = heroConfigType.GetProperty($"{wordBarType}Grow");

                    int valueGrow = Convert.ToInt32(baseInfo.GetValue(heroConfig));

                    self.Datas[wordBarType] = HeroCardHelper.GetHeroBaseDataValue(value, valueGrow, level, star);
                }
                else
                {
                    PropertyInfo info = enemy.GetType().GetProperty(wordBarType);

                    if (info == null)
                    {
                        continue;
                    }

                    int value = Convert.ToInt32(info.GetValue(enemyConfig));

                    self.Datas[wordBarType] = value;
                }
            }
        }

        public static void Awake(this FightDataComponent self, HeroCard heroCard)
        {
            foreach (var kv in heroCard.Datas)
            {
                self.Datas[kv.Key] = kv.Value;
            }
        }

        public static float GetValueByType(this FightDataComponent self, WordBarType wordBarType)
        {
            if (self.Datas.TryGetValue(wordBarType.ToString(), out float value))
            {
                return value;
            }

            return 0;
        }

        public static void SubHP(this FightDataComponent self, float damage)
        {
            self.CurrentHP -= damage;
        }

        // public static void SubValue(this FightDataComponent self, WordBarType wordBarType, float damage)
        // {
        //     float value = self.GetValueByType(wordBarType);
        //
        //     value -= damage;
        //
        //     self.Datas[wordBarType.ToString()] = value;
        // }
    }
}