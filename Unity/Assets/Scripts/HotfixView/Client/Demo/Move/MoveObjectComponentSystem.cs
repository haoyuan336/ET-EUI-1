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

            GameObject gameObject = objectComponent.GameObject;

            self.NavMeshAgent = gameObject.GetComponent<NavMeshAgent>();

            self.Body = gameObject.transform.GetChild(0);

            self.Body.rotation = Camera.main.transform.rotation;

            self.LocalScale = self.Body.localScale;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        public static void Move(this MoveObjectComponent self, Vector3 targetPos)
        {
            self.TargetPos = targetPos;

            self.NavMeshAgent.SetDestination(targetPos);

            // self.NavMeshAgent.updateRotation = false;

            Vector3 direction = self.TargetPos - self.NavMeshAgent.transform.position;

            float angle = Quaternion.Angle(Quaternion.LookRotation(direction), Quaternion.Euler(0, -45, 0));

            if (angle < 90)
            {
                self.Body.localScale = new Vector3(self.LocalScale.x,
                    self.LocalScale.y,
                    self.LocalScale.z);
            }
            else
            {
                self.Body.localScale = new Vector3(self.LocalScale.x * -1,
                    self.LocalScale.y,
                    self.LocalScale.z);
            }
        }

        [EntitySystem]
        public static void Update(this MoveObjectComponent self)
        {
            if (self.Body != null)
            {
                self.Body.rotation = Camera.main.transform.rotation;
            }
        }

        private static void MoveEnd(this MoveObjectComponent self)
        {
            self.NavMeshAgent.SetDestination(self.NavMeshAgent.transform.position);
        }

        public static void OnOutStateAction(this MoveObjectComponent self, AIState aiState)
        {
            Log.Debug($"MoveObjectComponent on out state action {aiState}");
            
            if (aiState == AIState.Moveing)
            {
                self.MoveEnd();
            }
        }
    }
}