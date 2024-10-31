using DotRecast.Recast.Geom;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(AnimComponent))]
    public static partial class AnimComponentSystem
    {
        [EntitySystem]
        public static void Destroy(this AnimComponent self)
        {
        }

        [EntitySystem]
        public static void Awake(this AnimComponent self)
        {
            self.PlayAnim("idle");

            AIComponent aiComponent = self.Parent.GetComponent<AIComponent>();

            aiComponent.EnterStateAction += self.OnEnterState;

            aiComponent.OutStateAction += self.OutEnterState;
        }

        public static void PlayAnim(this AnimComponent self, string animName)
        {
            ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

            objectComponent.SkeletonAnimation.state.SetAnimation(0, animName, true);
        }

        public static async ETTask PlayAnim(this AnimComponent self, string animName, bool isLoop)
        {
            ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

            objectComponent.SkeletonAnimation.state.SetAnimation(0, animName, isLoop);

            self.AnimTask = ETTask.Create();

            objectComponent.SkeletonAnimation.state.Complete += self.OnAnimComplete;

            await self.AnimTask.GetAwaiter();
        }

        private static void OnEnterState(this AnimComponent self, AIState aiState)
        {
            if (aiState == AIState.Rise)
            {
                self.PlayAnim("idle");
            }

            if (aiState == AIState.Moving || aiState == AIState.Track)
            {
                self.PlayAnim("move");
            }
            
        }

        private static void OutEnterState(this AnimComponent self, AIState aiState)
        {
            if (aiState == AIState.Moving)
            {
                self.PlayAnim("idle");
            }
        }

        public static void OnAnimComplete(this AnimComponent self, TrackEntry trackEntry)
        {
            if (self.AnimTask != null && !self.AnimTask.IsCompleted)
            {
                self.AnimTask.SetResult();
            }
        }
    }
}