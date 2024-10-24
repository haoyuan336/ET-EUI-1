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

            self.SkeletonAnimation = self.Body.GetComponent<SkeletonAnimation>();

            self.SkeletonAnimation.state.SetAnimation(0, "idle", true);
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

        public static void StartMove(this MoveObjectComponent self)
        {
            self.SkeletonAnimation.state.SetAnimation(0, "move", true);
        }

        public static void MoveEnd(this MoveObjectComponent self)
        {
            self.SkeletonAnimation.state.SetAnimation(0, "idle", true);

            self.NavMeshAgent.SetDestination(self.NavMeshAgent.transform.position);
        }

        public static void PlayAnim(this MoveObjectComponent self, string animName, bool isLoop)
        {
            self.SkeletonAnimation.state.SetAnimation(0, animName, isLoop);
        }
    }
}