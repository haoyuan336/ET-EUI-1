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

        [EntitySystem]
        public static async void Update(this AttackComponent self)
        {
            if (self.AIComponent != null)
            {
                if (self.AIComponent.GetCurrentState() == AIState.Attacking)
                {
                    ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                    if (self.TargetEntity == null || self.TargetEntity.IsDisposed)
                    {
                        self.AIComponent.EnterAIState(AIState.Patrol);

                        return;
                    }

                    AIState targetState = self.TargetEntity.GetComponent<AIComponent>().GetCurrentState();

                    if (targetState == AIState.Sleep || targetState == AIState.Death)
                    {
                        self.AIComponent.EnterAIState(AIState.Patrol);

                        return;
                    }

                    GameObject gameObject = self.TargetEntity.GetComponent<ObjectComponent>().GameObject;

                    float distance = (objectComponent.GameObject.transform.position - gameObject.transform.position).magnitude;

                    if (distance > 2f)
                    {
                        TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                        trackComponent.SetTrackObject(self.TargetEntity);

                        self.AIComponent.EnterAIState(AIState.Track);

                        return;
                    }

                    if (self.CurrentCastSkill == null)
                    {
                        SkillComponent skillComponent = self.Parent.GetComponent<SkillComponent>();

                        self.CurrentCastSkill = skillComponent.GetCurrentSkill();

                        await self.CurrentCastSkill.Cast();

                        self.CurrentCastSkill = null;
                    }

                    FightManagerComponent fightManagerComponent = self.Parent.GetParent<FightManagerComponent>();

                    long entityId = self.TargetEntity.Id;

                    bool isDead = FightDataHelper.GetIsDead(fightManagerComponent, entityId);

                    if (isDead)
                    {
                        TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                        trackComponent.SetTrackObject(self.TargetEntity);

                        self.AIComponent.EnterAIState(AIState.Patrol);
                    }
                }
            }
        }

        public static void SetAttackTarget(this AttackComponent self, Entity entity)
        {
            self.TargetEntity = entity;
        }

        private static void OnEnterStateAction(this AttackComponent self, AIState aiState)
        {
        }

        private static void OnOutStateAction(this AttackComponent self, AIState aiState)
        {
        }
    }
}