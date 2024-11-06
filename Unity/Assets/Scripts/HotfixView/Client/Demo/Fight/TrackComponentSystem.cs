using System;
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

        public static void SetTrackObject(this TrackComponent self, Entity targetEntity)
        {
            self.TargetEntity = targetEntity;
        }

        [EntitySystem]
        public static void Update(this TrackComponent self)
        {
            if (self.AIComponent == null)
            {
                return;
            }

            AIState aiState = self.AIComponent.GetCurrentState();

            if (aiState == AIState.TrackTree)
            {
                self.TrackTree();
            }

            if (aiState == AIState.Track)
            {
                self.TrackTarget();
            }
        }

        private static void TrackTarget(this TrackComponent self)
        {
            if (self.AIComponent.InSafeArea)
            {
                self.AIComponent.EnterAIState(AIState.Idle);

                return;
            }

            if (self.TargetEntity == null)
            {
                self.AIComponent.EnterAIState(AIState.Patrol);

                return;
            }

            MoveObjectComponent moveObjectComponent = self.Parent.GetComponent<MoveObjectComponent>();

            ObjectComponent beAttackObjectComponent = self.TargetEntity.GetComponent<ObjectComponent>();

            if (beAttackObjectComponent == null)
            {
                self.AIComponent.EnterAIState(AIState.Patrol);

                return;
            }

            GameObject beGameObejct = beAttackObjectComponent.GameObject;

            if (beGameObejct == null)
            {
                self.AIComponent.EnterAIState(AIState.Patrol);

                return;
            }

            bool isCanAttack = FightDataHelper.GetCanAttack(self.TargetEntity);

            if (!isCanAttack)
            {
                self.AIComponent.EnterAIState(AIState.Patrol);

                return;
            }

            moveObjectComponent.Move(beGameObejct.transform.position);

            ObjectComponent myObjectComponent = self.Parent.GetComponent<ObjectComponent>();

            GameObject myGameObject = myObjectComponent.GameObject;

            float distance = (myGameObject.transform.position - beGameObejct.transform.position).magnitude;

            if (distance < 2f)
            {
                AttackComponent attackComponent = self.Parent.GetComponent<AttackComponent>();

                attackComponent.SetAttackTarget(self.TargetEntity);

                self.AIComponent.EnterAIState(AIState.Attacking);
            }

            if (distance > ConstValue.FindEnemyDistance + 1)
            {
                self.TargetEntity = null;
            }
        }

        private static void TrackTree(this TrackComponent self)
        {
            if (self.AIComponent.InSafeArea)
            {
                self.AIComponent.EnterAIState(AIState.Idle);

                return;
            }

            if (self.TargetEntity == null)
            {
                self.AIComponent.EnterAIState(AIState.Patrol);

                return;
            }

            ObjectComponent myObjectComponent = self.Parent.GetComponent<ObjectComponent>();

            GameObject myGameObject = myObjectComponent.GameObject;

            MoveObjectComponent moveObjectComponent = self.Parent.GetComponent<MoveObjectComponent>();

            GameObject beGameObejct = null;

            Tree tree = null;

            try
            {
                tree = self.TargetEntity as Tree;

                beGameObejct = tree.TreeObject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                self.AIComponent.EnterAIState(AIState.Patrol);

                return;
            }

            if (beGameObejct == null)
            {
                self.AIComponent.EnterAIState(AIState.Patrol);

                return;
            }

            moveObjectComponent.Move(beGameObejct.transform.position);

            float distance = (myGameObject.transform.position - beGameObejct.transform.position).magnitude;

            if (distance < 2f)
            {
                CutTreeComponent cutTreeComponent = self.Parent.GetComponent<CutTreeComponent>();

                if (self.TargetEntity is not Tree)
                {
                    self.AIComponent.EnterAIState(AIState.Patrol);

                    return;
                }

                cutTreeComponent.SetTarget(self.TargetEntity as Tree);

                self.AIComponent.EnterAIState(AIState.CutTree);
            }

            if (distance > ConstValue.FindEnemyDistance + 1)
            {
                self.TargetEntity = null;
            }
        }

        public static void OnEnterStateAction(this TrackComponent self, AIState aiState)
        {
        }

        public static void OnOutStateAction(this TrackComponent self, AIState aiState)
        {
        }
    }
}