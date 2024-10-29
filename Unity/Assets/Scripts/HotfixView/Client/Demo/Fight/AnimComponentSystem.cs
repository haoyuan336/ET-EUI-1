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
            self.SkeletonAnimation = null;
        }

        [EntitySystem]
        public static void Awake(this AnimComponent self)
        {
            GameObject gameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;

            GameObject body = gameObject.transform.GetChild(0).gameObject;

            self.SkeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();

            self.SkeletonAnimation = body.GetComponent<SkeletonAnimation>();

            self.SkeletonAnimation.state.SetAnimation(0, "idle", true);
        }

        public static async ETTask PlayAnim(this AnimComponent self, string animName, bool isLoop)
        {
            self.SkeletonAnimation.state.SetAnimation(0, animName, isLoop);

            self.AnimTask = ETTask.Create();

            self.SkeletonAnimation.state.Complete += self.OnAnimComplete;

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