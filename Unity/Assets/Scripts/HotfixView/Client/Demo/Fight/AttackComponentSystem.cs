using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(AttackComponent))]
    public static partial class AttackComponentSystem
    {
        [EntitySystem]
        public static void Awake(this AttackComponent self)
        {
            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }
        public static void SetAttackTargets(this AttackComponent self, List<Entity> entities)
        {
            self.TargetEntities = entities;

            self.AIComponent.EnterAIState(AIState.Attacking);
        }

        private static async void OnEnterStateAction(this AttackComponent self, AIState aiState)
        {
            if (aiState == AIState.Attacking)
            {
                FightDataComponent fightDataComponent = self.Parent.GetComponent<FightDataComponent>();

                float maxAttackDiantance = fightDataComponent.GetValueByType(WordBarType.MaxAttackDistance);

                TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

                SkillComponent skillComponent = self.Parent.GetComponent<SkillComponent>();

                while (true)
                {
                    if (self.AIComponent.GetCurrentState() != AIState.Attacking)
                    {
                        return;
                    }

                    Skill skill = skillComponent.GetCurrentSkill();

                    if (skill == null)
                    {
                        self.AIComponent.EnterAIState(AIState.Track);

                        return;
                    }

                    Entity entity = self.TargetEntities[0];

                    if (entity == null)
                    {
                        self.AIComponent.EnterAIState(AIState.Patrol);

                        return;
                    }

                    bool isAllDead = true;

                    foreach (var en in self.TargetEntities)
                    {
                        AIComponent aiComponent = en.GetComponent<AIComponent>();

                        if (aiComponent.GetCurrentState() != AIState.Death)
                        {
                            isAllDead = false;

                            break;
                        }
                    }

                    if (isAllDead)
                    {
                        self.AIComponent.EnterAIState(AIState.Patrol);

                        return;
                    }

                    ObjectComponent entityObjectComponent = entity.GetComponent<ObjectComponent>();

                    float distance = Vector3.Distance(self.Parent.GetComponent<ObjectComponent>().GameObject.transform.position,
                        entityObjectComponent.GameObject.transform.position);

                    if (distance > maxAttackDiantance + 1)
                    {
                        self.AIComponent.EnterAIState(AIState.Track);

                        return;
                    }

                    await skill.Cast();

                    await timerComponent.WaitFrameAsync();
                }
            }
        }

        private static void OnOutStateAction(this AttackComponent self, AIState aiState)
        {
        }
    }
}