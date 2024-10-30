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
            GameObject gameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;

            GameObject body = gameObject.transform.GetChild(0).gameObject;
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

        public static void OnAnimComplete(this AnimComponent self, TrackEntry trackEntry)
        {
            if (self.AnimTask != null && !self.AnimTask.IsCompleted)
            {
                self.AnimTask.SetResult();
            }
        }
    }
}