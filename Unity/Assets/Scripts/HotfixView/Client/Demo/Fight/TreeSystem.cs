using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(Tree))]
    public static partial class TreeSystem
    {
        [EntitySystem]
        public static void Awake(this Tree self, int configId)
        {
            self.ConfigId = configId;

            self.HP = self.TreeConfig.HP;
        }

        public static void BindObject(this Tree self, GameObject gameObject)
        {
            self.TreeObject = gameObject;

            Transform body = gameObject.transform.GetChild(0);

            self.SkeletonAnimation = body.GetComponent<SkeletonAnimation>();

            self.SkeletonAnimation.state.SetAnimation(0, "sm1", true);
        }

        public static void BeAttack(this Tree self)
        {
            self.HP--;

            Log.Debug($"this Tree self {self.HP}");
            if (self.HP <= 0)
            {
                self.PlayDeathAnim();
            }
            else
            {
                self.PlayBeAttack();
            }
        }

        public static bool GetIsCanCut(this Tree self)
        {
            return !(self.HP <= 0 || self.TreeObject == null);
        }

        private static void PlayDeathAnim(this Tree self)
        {
            if (self.SkeletonAnimation != null)
            {
                self.SkeletonAnimation.state.SetAnimation(0, "sm3", false);
            }
        }

        private static void PlayBeAttack(this Tree self)
        {
            if (self.SkeletonAnimation != null)
            {
                self.SkeletonAnimation.state.SetAnimation(0, "sm2", false);
            }
        }
    }
}