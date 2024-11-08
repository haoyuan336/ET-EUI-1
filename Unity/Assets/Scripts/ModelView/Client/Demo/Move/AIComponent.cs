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
        // FindEnemy = 10, //寻找英雄
        Transfer = 11, //传送中
        Idle = 12, //等待状态，此状态 
        CutTree = 13, //砍树
        TrackTree = 14, //追踪树
        MoveToInitPos = 15, //移动到初始化位置
        Wait = 16, //等待中
        MovEnd = 17, //移动结束
    }

    [ComponentOf(typeof(Entity))]
    public class AIComponent : Entity, IAwake, IDestroy
    {
        public Action<AIState> EnterStateAction;

        public Action<AIState> OutStateAction;

        public Stack<AIState> StateStack = new Stack<AIState>();

        // public bool IsCanBeAttack = false;
        public bool InSafeArea = false;
    }
}