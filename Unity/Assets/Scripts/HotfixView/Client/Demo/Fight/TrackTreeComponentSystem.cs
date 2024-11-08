using System;
using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [ComponentOf(typeof(TrackTreeComponent))]
    public static partial class TrackTreeComponentSystem
    {
        [EntitySystem]
        public static void Awake(this TrackTreeComponent self)
        {
            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;

            FightDataComponent fightDataComponent = self.Parent.GetComponent<FightDataComponent>();

            self.FindEnemyDistance = fightDataComponent.GetValueByType(WordBarType.MaxTrackDistance);
        }

        public static void SetTrackObject(this TrackTreeComponent self, Tree targetEntity)
        {
            // self.TargetEntity = targetEntity;

            self.Tree = targetEntity;

            self.AIComponent.EnterAIState(AIState.TrackTree);
        }

        public static async void OnEnterStateAction(this TrackTreeComponent self, AIState aiState)
        {
            if (aiState == AIState.TrackTree)
            {
                GameObject gameObject = self.Tree.TreeObject;

                MoveObjectComponent moveObjectComponent = self.Parent.GetComponent<MoveObjectComponent>();

                moveObjectComponent.Move(gameObject.transform.position);

                TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

                ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                while (true)
                {
                    float distance = Vector3.Distance(objectComponent.GameObject.transform.position, gameObject.transform.position);

                    if (distance < 1.5f)
                    {
                        CutTreeComponent cutTreeComponent = self.Parent.GetComponent<CutTreeComponent>();

                        moveObjectComponent.Move(objectComponent.GameObject.transform.position);
                        
                        cutTreeComponent.SetTarget(self.Tree);

                        return;
                    }

                    if (distance > 2)
                    {
                        self.AIComponent.EnterAIState(AIState.Patrol);

                        return;
                    }

                    await timerComponent.WaitFrameAsync();
                }
            }
        }

        public static void OnOutStateAction(this TrackTreeComponent self, AIState aiState)
        {
        }
    }
}