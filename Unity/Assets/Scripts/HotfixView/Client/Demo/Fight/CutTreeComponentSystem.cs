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
        }

        [EntitySystem]
        public static async void Update(this CutTreeComponent self)
        {
            if (self.AIComponent != null)
            {
                Log.Debug($"cut tree {self.AIComponent.GetCurrentState()}");
             
                if (self.AIComponent.GetCurrentState() == AIState.CutTree)
                {
                    ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

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

                    GameObject gameObject = self.TargetTree.TreeObject;

                    float distance = (objectComponent.GameObject.transform.position - gameObject.transform.position).magnitude;

                    if (distance > 2f)
                    {
                        TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                        trackComponent.SetTrackObject(self.TargetTree);

                        self.AIComponent.EnterAIState(AIState.TrackTree);

                        return;
                    }

                    Log.Debug($"is cuting {self.IsCuting}");
                    
                    if (self.IsCuting)
                    {
                        return;
                    }

                    self.IsCuting = true;

                    List<ETTask> tasks = new List<ETTask>();

                    tasks.Add(self.Attack());

                    tasks.Add(self.PlayAttackAnim());

                    await ETTaskHelper.WaitAll(tasks);

                    self.IsCuting = false;

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

            EventSystem.Instance.Publish(self.Root(), new PlayAddWood()
            {
                Count = 1,
                Tree = self.TargetTree
            });
        }

        public static async ETTask PlayAttackAnim(this CutTreeComponent self)
        {
            AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();

            await animComponent.PlayAnim("attack3", false);
        }

        public static void SetTarget(this CutTreeComponent self, Tree tree)
        {
            self.TargetTree = tree;
        }
    }
}