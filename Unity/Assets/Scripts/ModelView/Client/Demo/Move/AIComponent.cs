using System;
using System.Collections.Generic;

namespace ET.Client
{
    public enum AIState
    {
        Invalide = 1,
        MoveToUnit = 2,
        Moveing = 3,
        FindEnemy = 4,
        Attacking = 5,
        Patrol = 6, //巡逻
        Track = 7, //跟踪
        Death = 8, // 死亡
    }

    [ComponentOf(typeof(Entity))]
    public class AIComponent : Entity, IAwake
    {
        public Action<AIState> EnterStateAction;

        public Action<AIState> OutStateAction;

        public Stack<AIState> StateStack = new Stack<AIState>();
    }
}