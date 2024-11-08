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
        }

        private static FightManagerComponent GetFightManagerComponent(this PatrolComponent self, Entity entity)
        {
            FightManagerComponent fightManagerComponent = entity.GetParent<FightManagerComponent>();

            return fightManagerComponent;
        }

        private static async void OnEnterStateAction(this PatrolComponent self, AIState aiState)
        {
            if (aiState == AIState.Patrol)
            {
                bool isMoveSuccess = await self.MoveToRandomPos();

                if (self.IsDisposed)
                {
                    return;
                }

                if (isMoveSuccess)
                {
                    self.AIComponent.EnterAIState(AIState.Wait);
                }
                else
                {
                    self.AIComponent.EnterAIState(AIState.Sleep);
                }
            }
        }

        private static void OnOutStateAction(this PatrolComponent self, AIState outState)
        {
        }

        private static async ETTask<bool> MoveToRandomPos(this PatrolComponent self)
        {
            self.TargetPos = Quaternion.Euler(0, RandomGenerator.RandomNumber(0, 360), 0) * Vector3.forward *
                    (2 + RandomGenerator.RandFloat01() * 4) + self.InitPos;

            MoveObjectComponent moveComponent = self.Parent.GetComponent<MoveObjectComponent>();

            moveComponent.Move(self.TargetPos);

            AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();


            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            GameObject gameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;

            if (gameObject == null)
            {
                return false;
            }

            await timerComponent.WaitFrameAsync();
            
            animComponent.PlayAnim("move");

            while (true)
            {
                if (gameObject == null)
                {
                    return false;
                }

                float distance = Vector3.Distance(gameObject.transform.position, self.TargetPos);

                if (distance < 0.5f)
                {
                    break;
                }

                await timerComponent.WaitFrameAsync();
            }

            return true;
        }
    }
}