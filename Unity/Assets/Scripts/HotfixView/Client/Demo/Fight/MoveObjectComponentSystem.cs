using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;

namespace ET.Client
{
    [EntitySystemOf(typeof(MoveObjectComponent))]
    public static partial class MoveObjectComponentSystem
    {
        [EntitySystem]
        public static void Destroy(this MoveObjectComponent self)
        {
        }

        [EntitySystem]
        public static void Awake(this MoveObjectComponent self)
        {
            ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

            self.LocalScale = objectComponent.Body.transform.localScale;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterState;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        public static void SetPos(this MoveObjectComponent self, Vector3 targetPos)
        {
            self.TargetPos = targetPos;

            ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

            if (objectComponent.GameObject != null)
            {
                objectComponent.GameObject.transform.position = targetPos;

                objectComponent.NavMeshAgent.SetDestination(targetPos);
            }
        }

        public static void Move(this MoveObjectComponent self, Vector3 targetPos)
        {
            self.TargetPos = targetPos;

            NavMeshAgent navMeshAgent = self.Parent.GetComponent<ObjectComponent>().NavMeshAgent;

            if (navMeshAgent == null)
            {
                Log.Debug("navMeshAgent is null");
                return;
            }

            navMeshAgent.SetDestination(targetPos);

            Vector3 direction = self.TargetPos - navMeshAgent.transform.position;

            float angle = Quaternion.Angle(Quaternion.LookRotation(direction), Quaternion.Euler(0, -45, 0));

            ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

            if (angle < 90)
            {
                objectComponent.Body.transform.localScale = new Vector3(self.LocalScale.x,
                    self.LocalScale.y,
                    self.LocalScale.z);
            }
            else
            {
                objectComponent.Body.transform.localScale = new Vector3(self.LocalScale.x * -1,
                    self.LocalScale.y,
                    self.LocalScale.z);
            }
        }

        [EntitySystem]
        public static void Update(this MoveObjectComponent self)
        {
        }

        private static void MoveEnd(this MoveObjectComponent self)
        {
            ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

            if (objectComponent == null || objectComponent.IsDisposed)
            {
                return;
            }

            self.OldPos = objectComponent.GameObject.transform.position;

            NavMeshAgent navMeshAgent = self.Parent.GetComponent<ObjectComponent>().NavMeshAgent;

            if (navMeshAgent == null)
            {
                return;
            }

            navMeshAgent.SetDestination(self.Parent.GetComponent<ObjectComponent>().GameObject.transform.position);
        }

        public static void OnEnterState(this MoveObjectComponent self, AIState aiState)
        {
            // if (aiState == AIState.Death || aiState == AIState.Attacking || aiState == AIState.Idle || aiState == AIState.CutTree)
            // {
            // }

            if (aiState == AIState.Attacking)
            {
                self.MoveEnd();
            }
            
            if (aiState == AIState.MovEnd)
            {
                AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

                if (!aiComponent.InSafeArea)
                {
                    aiComponent.EnterAIState(AIState.Patrol);
                }

                self.MoveEnd();
            }
        }

        public static void OnOutStateAction(this MoveObjectComponent self, AIState aiState)
        {
        }
    }
}