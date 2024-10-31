using System;
using System.Collections.Generic;

namespace ET.Client
{
    public enum AIState
    {
        Invalide = 1,
        MoveToUnit = 2,
        Moving = 3,

        // FindEnemy = 4,
        Attacking = 4,
        Patrol = 5, //巡逻
        Track = 6, //跟踪
        Death = 7, // 死亡
        Sleep = 8, //睡眠状态 
        Rise = 9, //复活
        FindEnemy = 10, //寻找英雄
        Transfer = 11, //传送中
    }

    [ComponentOf(typeof(Entity))]
    public class AIComponent : Entity, IAwake, IDestroy
    {
        public Action<AIState> EnterStateAction;

        public Action<AIState> OutStateAction;

        public Stack<AIState> StateStack = new Stack<AIState>();
    }
}