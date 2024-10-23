using System;

namespace ET.Client
{
    public enum AIState
    {
        Invalide = -1,
        MoveToUnit = 1,
        Moveing = 2,
        FindEnemy = 3,
        Attacking = 4,
        Patrol = 5, //巡逻
        Track = 6, //跟踪
    }

    [ComponentOf(typeof(Entity))]
    public class AIComponent : Entity, IAwake
    {
        public AIState CurrentAIState = AIState.Invalide;

        public Action<AIState> EnterStateAction;

        public Action<AIState> OutStateAction;
    }
}