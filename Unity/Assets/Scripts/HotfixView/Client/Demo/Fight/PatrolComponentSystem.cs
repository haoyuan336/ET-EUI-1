using UnityEngine;
using Random = System.Random;

namespace ET.Client
{
    [EntitySystemOf(typeof(PatrolComponent))]
    public static partial class PatrolComponentSystem
    {
        [EntitySystem]
        public static void Awake(this PatrolComponent self, Vector3 initPos)
        {
            self.InitPos = initPos;

            self.TargetPos = self.InitPos;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        [EntitySystem]
        public static void Update(this PatrolComponent self)
        {
            if (self.AIComponent.GetCurrentState() == AIState.Patrol)
            {
                ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                if (objectComponent.GameObject == null)
                {
                    
                    return;
                }

                Vector3 currentPos = objectComponent.GameObject.transform.position;

                float distance = (self.TargetPos - currentPos).magnitude;

                if (distance < 0.5f)
                {
                    self.MoveToRandomPos();
                }

            }
        }

        private static FightManagerComponent GetFightManagerComponent(this PatrolComponent self, Entity entity)
        {
            FightManagerComponent fightManagerComponent = entity.GetParent<FightManagerComponent>();

            return fightManagerComponent;
        }

        private static void OnEnterStateAction(this PatrolComponent self, AIState aiState)
        {
            if (aiState == AIState.Patrol)
            {
                self.MoveToRandomPos();
            }
        }

        private static void OnOutStateAction(this PatrolComponent self, AIState outState)
        {
            // if (outState == AIState.Patrol)
            // {
            //     AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();
            //
            //     animComponent.PlayAnim("idle", true).Coroutine();
            // }
        }

        private static void MoveToRandomPos(this PatrolComponent self)
        {
            self.TargetPos = Quaternion.Euler(0, RandomGenerator.RandomNumber(0, 360), 0) * Vector3.forward *
                    (4 + RandomGenerator.RandFloat01() * 4) + self.InitPos;

            MoveObjectComponent moveComponent = self.Parent.GetComponent<MoveObjectComponent>();

            moveComponent.Move(self.TargetPos);
        }
    }
}