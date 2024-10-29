namespace ET.Client
{
    [EntitySystemOf(typeof(AIComponent))]
    public static partial class AIComponentSystem
    {
        [EntitySystem]
        public static void Awake(this AIComponent self)
        {
            self.StateStack.Push(AIState.Invalide);
        }

        public static void EnterAIState(this AIComponent self, AIState aiState)
        {
            Log.Debug($"enter ai state  {self.Id},{aiState} {self.GetCurrentState()}");

            if (self.OutStateAction != null)
            {
                self.OutStateAction.Invoke(self.GetCurrentState());
            }

            self.StateStack.Push(aiState);

            if (self.EnterStateAction != null)
            {
                self.EnterStateAction.Invoke(aiState);
            }
        }

        public static AIState GetCurrentState(this AIComponent self)
        {
            if (self.StateStack.Count > 0)
            {
                return self.StateStack.Peek();
            }

            return AIState.Invalide;
        }

        public static void OutAIState(this AIComponent self)
        {
            Log.Debug($"out ai state {self.StateStack.Count}");

            if (self.StateStack.Count > 1)
            {
                AIState oldState = self.StateStack.Pop();

                Log.Debug($"old state {oldState}");

                AIState state = self.StateStack.Peek();

                if (self.OutStateAction != null)
                {
                    self.OutStateAction.Invoke(oldState);
                }

                if (self.EnterStateAction != null)
                {
                    self.EnterStateAction.Invoke(state);
                }
            }
        }
    }
}