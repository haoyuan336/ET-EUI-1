using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ET;
using UnityEditor.PackageManager;

namespace ET.Client
{
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

            self.CurrentHP = self.GetValueByType(WordBarType.Hp);
        }

        [EntitySystem]
        public static void Awake(this FightDataComponent self, HeroCard heroCard)
        {
            Log.Debug($"data count {heroCard.Datas.Count}");

            foreach (var kv in heroCard.Datas)
            {
                Log.Debug($"kv kye {kv.Key} {kv.Value}");
                self.Datas[kv.Key] = kv.Value;
            }

            self.CurrentHP = self.GetValueByType(WordBarType.Hp);
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

            if (self.CurrentHP <= 0)
            {
                // self.Parent.GetParent<MoveObjectComponent>().PlayAnim("death", false);

                EventSystem.Instance.Publish(self.Root(), new PlayDeadAnim()
                {
                    Entity = self.GetParent<Entity>()
                });

                AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();
                
                aiComponent.EnterAIState(AIState.Death);
                
                EventSystem.Instance.Publish(self.Root(), new CheckGameLoseLogic());
            }
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