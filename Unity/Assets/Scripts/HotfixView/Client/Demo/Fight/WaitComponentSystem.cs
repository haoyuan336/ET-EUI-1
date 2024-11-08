namespace ET.Client
{
    [EntitySystemOf(typeof(WaitComponent))]
    public static partial class WaitComponentSystem
    {
        [EntitySystem]
        public static void Awake(this WaitComponent self)
        {
            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterState;
        }

        private static async void OnEnterState(this WaitComponent self, AIState aiState)
        {
            if (aiState == AIState.Wait)
            {
                TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

                int randomTime = RandomGenerator.RandomNumber(500, 2000);

                await timerComponent.WaitAsync(randomTime);

                if (self.AIComponent.GetCurrentState() == AIState.Wait)
                {
                    self.AIComponent.EnterAIState(AIState.Patrol);
                }
            }
        }
    }
}