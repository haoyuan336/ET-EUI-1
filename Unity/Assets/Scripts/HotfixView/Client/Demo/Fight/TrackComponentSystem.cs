using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [ComponentOf(typeof(TrackComponent))]
    public static partial class TrackComponentSystem
    {
        [EntitySystem]
        public static void Awake(this TrackComponent self)
        {
            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        public static void SetTrackObject(this TrackComponent self, Entity targetEntity)
        {
            self.TargetEntity = targetEntity;
        }

        [EntitySystem]
        public static void Update(this TrackComponent self)
        {
            if (self.AIComponent != null && self.AIComponent.GetCurrentState() == AIState.Track)
            {
                if (self.TargetEntity == null)
                {
                    self.AIComponent.EnterAIState(AIState.Patrol);

                    return;
                }

                MoveObjectComponent moveObjectComponent = self.Parent.GetComponent<MoveObjectComponent>();

                GameObject gameObject = self.TargetEntity.GetComponent<ObjectComponent>().GameObject;

                if (gameObject == null)
                {
                    self.AIComponent.EnterAIState(AIState.Patrol);

                    return;
                }

                moveObjectComponent.Move(gameObject.transform.position);

                ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                float distance = (objectComponent.GameObject.transform.position - gameObject.transform.position).magnitude;

                if (distance < 2f)
                {
                    AttackComponent attackComponent = self.Parent.GetComponent<AttackComponent>();

                    attackComponent.SetAttackTarget(self.TargetEntity);

                    self.AIComponent.EnterAIState(AIState.Attacking);
                }

                if (distance > ConstValue.FindEnemyDistance + 1)
                {
                    self.TargetEntity = null;
                }
            }
        }

        public static void OnEnterStateAction(this TrackComponent self, AIState aiState)
        {
        }

        public static void OnOutStateAction(this TrackComponent self, AIState aiState)
        {
            
        }
    }
}