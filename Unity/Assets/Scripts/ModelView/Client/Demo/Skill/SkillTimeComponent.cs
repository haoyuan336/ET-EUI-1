using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Skill))]
    public class SkillTimeComponent: Entity, IAwake, IUpdate
    {
        public bool IsPauseing = false;
        
        public readonly NativeCollection.MultiMap<long, long> timeId = new(1000);

        public readonly Queue<long> timeOutTime = new();

        public readonly Queue<long> timeOutTimerIds = new();

        public readonly Dictionary<long, TimerAction> timerActions = new();

        public long idGenerator;

        // 记录最小时间，不用每次都去MultiMap取第一个值
        public long minTime = long.MaxValue;

        public long NowTime = 0;
    }
}