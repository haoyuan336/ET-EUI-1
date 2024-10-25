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
                if (self.AIComponent.CurrentAIState == AIState.Attacking)
                {
                    ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                    float distance = (objectComponent.GameObject.transform.position - self.AttackObject.transform.position).magnitude;

                    if (distance > 2f)
                    {
                        TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                        trackComponent.SetTrackObject(self.AttackObject);

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
                }
            }
        }

        public static void SetAttackTarget(this AttackComponent self, GameObject targetObject)
        {
            self.AttackObject = targetObject;
        }

        private static void OnEnterStateAction(this AttackComponent self, AIState aiState)
        {
        }

        private static void OnOutStateAction(this AttackComponent self, AIState aiState)
        {
        }
    }
}