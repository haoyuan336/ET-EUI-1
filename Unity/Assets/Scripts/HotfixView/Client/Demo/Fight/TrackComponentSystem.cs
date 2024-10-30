using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [ComponentOf(typeof(TrackComponent))]
    public static partial class TrackComponentSystem
    {
        public static void SetTrackObject(this TrackComponent self, GameObject targetObject)
        {
            self.TrackGameObject = targetObject;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        [EntitySystem]
        public static void Update(this TrackComponent self)
        {
            if (self.AIComponent != null && self.AIComponent.GetCurrentState() == AIState.Track)
            {
                if (self.TrackGameObject == null)
                {
                    self.AIComponent.EnterAIState(AIState.Patrol);

                    return;
                }

                MoveObjectComponent moveObjectComponent = self.Parent.GetComponent<MoveObjectComponent>();

                moveObjectComponent.Move(self.TrackGameObject.transform.position);

                ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                float distance = (objectComponent.GameObject.transform.position - self.TrackGameObject.transform.position).magnitude;

                if (distance < 2f)
                {
                    AttackComponent attackComponent = self.Parent.GetComponent<AttackComponent>();

                    attackComponent.SetAttackTarget(self.TrackGameObject);

                    self.AIComponent.EnterAIState(AIState.Attacking);
                }

                if (distance > ConstValue.FindEnemyDistance)
                {
                    self.TrackGameObject = null;
                }
            }
        }

        public static void OnEnterStateAction(this TrackComponent self, AIState aiState)
        {
            if (aiState == AIState.Track)
            {
                AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();

                animComponent.PlayAnim("move", true).Coroutine();
            }
        }

        public static void OnOutStateAction(this TrackComponent self, AIState aiState)
        {
            Log.Debug($"out state action {aiState}");
            if (aiState == AIState.Track)
            {
                AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();

                animComponent.PlayAnim("idle", true).Coroutine();
            }
        }
    }
}