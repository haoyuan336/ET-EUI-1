using Spine.Unity;
using UnityEngine;
using WeChatWASM;

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

            if (self.HP <= 0)
            {
                self.SkeletonAnimation.state.SetAnimation(0, self.TreeConfig.DeathAnim, true);
            }
            else
            {
                self.SkeletonAnimation.state.SetAnimation(0, self.TreeConfig.IdleAnim, true);
            }
        }

        public static async void BeAttack(this Tree self)
        {
            self.HP--;

            Log.Debug($"this Tree self {self.HP}");
            if (self.HP <= 0)
            {
                self.PlayDeathAnim();

                TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

                int time = self.TreeConfig.RiseTime;

                await timerComponent.WaitAsync(time);

                self.HP = self.TreeConfig.HP;

                self.IsAwardItem = false;

                if (self.SkeletonAnimation != null)
                {
                    self.SkeletonAnimation.state.SetAnimation(0, self.TreeConfig.IdleAnim, true);
                }
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
                self.SkeletonAnimation.state.SetAnimation(0, self.TreeConfig.DeathAnim, false);
            }
        }

        private static void PlayBeAttack(this Tree self)
        {
            if (self.SkeletonAnimation != null)
            {
                self.SkeletonAnimation.state.SetAnimation(0, self.TreeConfig.BeAttackAnim, false);
            }
        }
    }
}