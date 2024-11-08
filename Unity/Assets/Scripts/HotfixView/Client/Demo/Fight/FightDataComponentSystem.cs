using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Rendering.LookDev;

namespace ET.Client
{
    [EntitySystemOf(typeof(FightDataComponent))]
    public static partial class FightDataComponentSystem
    {
        [EntitySystem]
        public static void Awake(this FightDataComponent self, Enemy enemy)
        {
            EnemyConfig enemyConfig = enemy.Config;

            self.HeroConfig = HeroConfigCategory.Instance.Get(enemyConfig.HeroConfigId);

            int level = enemyConfig.Level;

            int star = enemyConfig.Star;

            Type heroConfigType = self.HeroConfig.GetType();

            List<WordBarConfig> configs = WordBarConfigCategory.Instance.GetAll().Values.ToList();

            foreach (var wordBarConfig in configs)
            {
                string wordBarType = wordBarConfig.WordBarType;

                if (wordBarConfig.WordAttributeType == WordAttributeType.BaseAttribute.ToString() ||
                    wordBarConfig.WordAttributeType == WordAttributeType.FixedValueAttribute.ToString())
                {
                    PropertyInfo baseInfo = heroConfigType.GetProperty($"{wordBarType}");

                    float baseValue = Convert.ToSingle(baseInfo.GetValue(self.HeroConfig));

                    PropertyInfo growInfo = heroConfigType.GetProperty($"{wordBarType}Grow");

                    float valueGrow = 0;

                    if (growInfo != null)
                    {
                        valueGrow = Convert.ToSingle(growInfo.GetValue(self.HeroConfig));
                    }

                    self.Datas[wordBarType] = HeroCardHelper.GetHeroBaseDataValue(baseValue, valueGrow, level, star);
                }
                else
                {
                    // PropertyInfo info = heroConfigType.GetProperty($"{wordBarType}");
                    //
                    // Log.Debug($"  base value WordAttributeType  {wordBarType} {info == null}");
                    //
                    // if (info == null)
                    // {
                    //     continue;
                    // }
                    //
                    // float value = Convert.ToSingle(info.GetValue(heroConfigType));
                    //
                    // Log.Debug($"  base value WordAttributeType {wordBarType} {value}");
                    //
                    // self.Datas[wordBarType] = value;
                }
            }

            self.CurrentHP = self.GetValueByType(WordBarType.Hp);

            self.InitAIComponent();
        }

        private static void OnEnterStateAction(this FightDataComponent self, AIState aiState)
        {
            if (aiState == AIState.Rise)
            {
                self.CurrentHP = self.GetValueByType(WordBarType.Hp);
            }
        }

        [EntitySystem]
        public static void Awake(this FightDataComponent self, HeroCard heroCard)
        {
            self.Datas = heroCard.Datas;

            self.CurrentHP = self.GetValueByType(WordBarType.Hp);

            self.InitAIComponent();
        }

        private static void InitAIComponent(this FightDataComponent self)
        {
            AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

            aiComponent.EnterStateAction += self.OnEnterStateAction;
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
                AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

                aiComponent.EnterAIState(AIState.Death);

                EventSystem.Instance.Publish(self.Root(), new PlayDeadAnim()
                {
                    Entity = self.GetParent<Entity>()
                });

                EventSystem.Instance.Publish(self.Root(), new CheckGameLoseLogic()
                {
                    Entity = self.Parent
                });
            }
        }
    }
}