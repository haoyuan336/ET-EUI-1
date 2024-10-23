namespace ET.Client
{
    [EntitySystemOf(typeof(AIComponent))]
    public static partial class AIComponentSystem
    {
        [EntitySystem]
        public static void Awake(this AIComponent self)
        {
        }

        public static void EnterAIState(this AIComponent self, AIState aiState)
        {
            Log.Debug($"enter ai state {aiState} {self.CurrentAIState}");

            if (self.CurrentAIState == aiState)
            {
                return;
            }

            if (self.OutStateAction != null)
            {
                self.OutStateAction.Invoke(self.CurrentAIState);
            }

            self.CurrentAIState = aiState;

            if (self.EnterStateAction != null)
            {
                self.EnterStateAction.Invoke(aiState);
            }
        }
    }
}