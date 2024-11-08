using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(CutTreeComponent))]
    public static partial class CutTreeComponentSystem
    {
        [EntitySystem]
        public static void Awake(this CutTreeComponent self)
        {
            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterState;
        }

        public static async void OnEnterState(this CutTreeComponent self, AIState aiState)
        {
            if (aiState == AIState.CutTree)
            {
                while (true)
                {
                    if (self.AIComponent.GetCurrentState() != AIState.CutTree)
                    {
                        return;
                    }

                    if (self.TargetTree == null || self.TargetTree.IsDisposed)
                    {
                        self.AIComponent.EnterAIState(AIState.Patrol);

                        return;
                    }

                    if (!self.TargetTree.GetIsCanCut())
                    {
                        self.AIComponent.EnterAIState(AIState.Patrol);

                        return;
                    }

                    List<ETTask> tasks = new List<ETTask>();

                    tasks.Add(self.Attack());

                    tasks.Add(self.PlayAttackAnim());

                    await ETTaskHelper.WaitAll(tasks);

                    if (!self.TargetTree.GetIsCanCut())
                    {
                        self.TargetTree = null;
                    }
                }
            }
        }

        public static async ETTask Attack(this CutTreeComponent self)
        {
            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            await timerComponent.WaitAsync(500);

            self.TargetTree.BeAttack();

            EventSystem.Instance.Publish(self.Root(), new PlayCutWoodDamage()
            {
                Damage = 1,
                Tree = self.TargetTree
            });

            if (self.TargetTree.HP <= 0)
            {
                EventSystem.Instance.Publish(self.Root(), new PlayAddWoodCountAnim()
                {
                    Tree = self.TargetTree
                });
            }
        }

        public static async ETTask PlayAttackAnim(this CutTreeComponent self)
        {
            AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();

            await animComponent.PlayAnim("attack3", false);
        }

        public static void SetTarget(this CutTreeComponent self, Tree tree)
        {
            self.TargetTree = tree;

            self.AIComponent.EnterAIState(AIState.CutTree);
        }
    }
}