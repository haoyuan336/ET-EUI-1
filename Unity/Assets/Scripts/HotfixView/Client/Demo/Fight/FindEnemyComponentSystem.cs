using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(FindEnemyComponent))]
    public static partial class FindEnemyComponentSystem
    {
        [EntitySystem]
        public static void Awake(this FindEnemyComponent self, int colliderLayer)
        {
            self.ColliderLayer = colliderLayer;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;

            self.SkillComponent = self.Parent.GetComponent<SkillComponent>();

            self.FightDataComponent = self.Parent.GetComponent<FightDataComponent>();

            self.FindEnemyDistance = self.FightDataComponent.GetValueByType(WordBarType.MaxTrackDistance);
        }

        [EntitySystem]
        public static void Update(this FindEnemyComponent self)
        {
            if (self.AIComponent != null && !self.AIComponent.InSafeArea)
            {
                if (self.AIComponent.GetCurrentState() == AIState.Patrol)
                {
                    //首先取出来 当前需要释放的技能
                    Skill skill = self.SkillComponent.GetCurrentSkill();

                    if (skill == null)
                    {
                        return;
                    }

                    int targetCount = skill.Config.TargetCount;

                    if (self.FindAngle % 2 == 0)
                    {
                        // Vector3 forword = Quaternion.Euler(0, self.FindAngle, 0) * Vector3.forward;

                        GameObject gameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;

                        if (gameObject == null)
                        {
                            return;
                        }

                        Vector3 sourcePos = gameObject.transform.position + Vector3.up * 15;

                        int size = Physics.SphereCastNonAlloc(sourcePos, self.FindEnemyDistance, Vector3.down, self.RaycastHits, 20,
                            self.ColliderLayer);

                        if (size > 0)
                        {
                            List<Entity> canAttackTarget = new List<Entity>();

                            for (int i = 0; i < size; i++)
                            {
                                RaycastHit hit = self.RaycastHits[i];

                                long entityId = FightDataHelper.GetIdByGameObjectName(hit.transform.gameObject.name);

                                FightManagerComponent fightManagerComponent = self.GetFightManagerComponent();

                                Entity entity = fightManagerComponent.GetChild<Entity>(entityId);

                                if (entity == null || entity.IsDisposed)
                                {
                                    continue;
                                }

                                bool canAttack = FightDataHelper.GetCanAttack(entity);

                                if (!canAttack)
                                {
                                    continue;
                                }

                                canAttackTarget.Add(entity);
                            }

                            List<(float, Entity)> list = new List<(float, Entity)>();

                            foreach (var entity in canAttackTarget)
                            {
                                ObjectComponent objectComponent = entity.GetComponent<ObjectComponent>();

                                GameObject beAttackObject = objectComponent.GameObject;

                                float distance = Vector3.Distance(gameObject.transform.position, beAttackObject.transform.position);

                                list.Add((distance, entity));
                            }

                            list.Sort((a, b) => a.Item1.CompareTo(b.Item1));

                            List<Entity> targetList = new List<Entity>();

                            for (int i = 0; i < Math.Min(list.Count, targetCount); i++)
                            {
                                targetList.Add(list[i].Item2);
                            }

                            //找到了最近的几个敌人，然后进行攻击

                            if (targetList.Count > 0)
                            {
                                TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                                trackComponent.SetTargetObjects(targetList);
                            }
                        }
                    }

                    self.FindAngle++;
                }
            }
        }

        public static void OnEnterStateAction(this FindEnemyComponent self, AIState aiState)
        {
            if (aiState == AIState.Patrol)
            {
            
            }
        }

        public static void OnOutStateAction(this FindEnemyComponent self, AIState aiState)
        {
        }

        private static FightManagerComponent GetFightManagerComponent(this FindEnemyComponent self)
        {
            FightManagerComponent fightManagerComponent = self.Parent.GetParent<FightManagerComponent>();

            return fightManagerComponent;
        }
    }
}