using System;
using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [ComponentOf(typeof(TrackComponent))]
    public static partial class TrackComponentSystem
    {
        [EntitySystem]
        public static void Awake(this TrackComponent self)
        {
            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        // public static void SetTrackObject(this TrackComponent self, Entity targetEntity)
        // {
        //     self.TargetEntity = targetEntity;
        // }

        public static void SetTargetObjects(this TrackComponent self, List<Entity> targetEntities)
        {
            self.TargetEntities = targetEntities;

            self.AIComponent.EnterAIState(AIState.Track);
        }

        // private static void TrackTarget(this TrackComponent self)
        // {
        //     if (self.AIComponent.InSafeArea)
        //     {
        //         self.AIComponent.EnterAIState(AIState.Idle);
        //
        //         return;
        //     }
        //
        //     if (self.TargetEntity == null)
        //     {
        //         self.AIComponent.EnterAIState(AIState.Patrol);
        //
        //         return;
        //     }
        //
        //     MoveObjectComponent moveObjectComponent = self.Parent.GetComponent<MoveObjectComponent>();
        //
        //     ObjectComponent beAttackObjectComponent = self.TargetEntity.GetComponent<ObjectComponent>();
        //
        //     if (beAttackObjectComponent == null)
        //     {
        //         self.AIComponent.EnterAIState(AIState.Patrol);
        //
        //         return;
        //     }
        //
        //     GameObject beGameObejct = beAttackObjectComponent.GameObject;
        //
        //     if (beGameObejct == null)
        //     {
        //         self.AIComponent.EnterAIState(AIState.Patrol);
        //
        //         return;
        //     }
        //
        //     bool isCanAttack = FightDataHelper.GetCanAttack(self.TargetEntity);
        //
        //     if (!isCanAttack)
        //     {
        //         self.AIComponent.EnterAIState(AIState.Patrol);
        //
        //         return;
        //     }
        //
        //     moveObjectComponent.Move(beGameObejct.transform.position);
        //
        //     ObjectComponent myObjectComponent = self.Parent.GetComponent<ObjectComponent>();
        //
        //     GameObject myGameObject = myObjectComponent.GameObject;
        //
        //     float distance = (myGameObject.transform.position - beGameObejct.transform.position).magnitude;
        //
        //     if (distance < 2f)
        //     {
        //         AttackComponent attackComponent = self.Parent.GetComponent<AttackComponent>();
        //
        //         attackComponent.SetAttackTarget(self.TargetEntity);
        //
        //         self.AIComponent.EnterAIState(AIState.Attacking);
        //     }
        //
        //     if (distance > ConstValue.FindEnemyDistance + 1)
        //     {
        //         self.TargetEntity = null;
        //     }
        // }

        public static async void OnEnterStateAction(this TrackComponent self, AIState aiState)
        {

            if (aiState == AIState.Track)
            {
                TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

                ObjectComponent myObjectComponent = self.Parent.GetComponent<ObjectComponent>();

                FightDataComponent fightDataComponent = self.Parent.GetComponent<FightDataComponent>();

                float maxAttackDistance = fightDataComponent.GetValueByType(WordBarType.MaxAttackDistance);

                float maxTrackDistance = fightDataComponent.GetValueByType(WordBarType.MaxTrackDistance);

                Log.Debug($"max attack distance {maxAttackDistance} {maxTrackDistance}");

                bool isSuccess = false;

                while (true)
                {
                    if (self.AIComponent.GetCurrentState() != AIState.Track)
                    {
                        return;
                    }

                    if (myObjectComponent.GameObject == null || self.TargetEntities[0] == null)
                    {
                        break;
                    }

                    ObjectComponent objectComponent = self.TargetEntities[0].GetComponent<ObjectComponent>();

                    if (objectComponent == null || objectComponent.GameObject == null)
                    {
                        break;
                    }

                    self.Parent.GetComponent<MoveObjectComponent>().Move(objectComponent.GameObject.transform.position);

                    float distance = Vector3.Distance(objectComponent.GameObject.transform.position, myObjectComponent.GameObject.transform.position);

                    if (distance > maxTrackDistance + 1)
                    {
                        break;
                    }

                    if (distance < maxAttackDistance)
                    {
                        isSuccess = true;

                        break;
                    }

                    await timerComponent.WaitFrameAsync();
                }

                Log.Debug($"is success {isSuccess}");

                if (isSuccess)
                {
                    AttackComponent attackComponent = self.Parent.GetComponent<AttackComponent>();

                    attackComponent.SetAttackTargets(self.TargetEntities);
                }
                else
                {
                    self.AIComponent.EnterAIState(AIState.Patrol);
                }
            }
        }

        public static void OnOutStateAction(this TrackComponent self, AIState aiState)
        {
        }
    }
}