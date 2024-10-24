using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(SkillComponent))]
    public static partial class SkillTimeComponentSystem
    {
        [EntitySystem]
        public static void Update(this SkillTimeComponent self)
        {
            if (self.IsPauseing)
            {
                return;
            }

            self.NowTime += (int)(Time.deltaTime * 1000);

            if (self.timeId.Count == 0)
            {
                return;
            }

            long timeNow = self.GetNow();

            if (timeNow < self.minTime)
            {
                return;
            }

            foreach (var kv in self.timeId)
            {
                long k = kv.Key;
                if (k > timeNow)
                {
                    self.minTime = k;
                    break;
                }

                self.timeOutTime.Enqueue(k);
            }

            while (self.timeOutTime.Count > 0)
            {
                long time = self.timeOutTime.Dequeue();
                var list = self.timeId[time];
                for (int i = 0; i < list.Length; ++i)
                {
                    long timerId = list[i];
                    self.timeOutTimerIds.Enqueue(timerId);
                }

                self.timeId.Remove(time);
            }

            if (self.timeId.Count == 0)
            {
                self.minTime = long.MaxValue;
            }

            while (self.timeOutTimerIds.Count > 0)
            {
                long timerId = self.timeOutTimerIds.Dequeue();

                if (!self.timerActions.Remove(timerId, out TimerAction timerAction))
                {
                    continue;
                }

                self.Run(timerId, ref timerAction);
            }

            Log.Debug($"self  now time {self.NowTime}");
        }

        public static long GetNow(this SkillTimeComponent self)
        {
            return self.NowTime;
        }

        private static long GetId(this SkillTimeComponent self)
        {
            return ++self.idGenerator;
        }

        public static async ETTask<bool> WaitAsync(this SkillTimeComponent self, long time, ETCancellationToken cancellationToken = null)
        {
            if (time == 0)
            {
                return false;
            }

            long timeNow = self.GetNow();

            ETTask<bool> tcs = ETTask<bool>.Create(true);

            long timerId = self.GetId();

            TimerAction timer = new(TimerClass.OnceWaitTimer, timeNow, time, 0, tcs);

            self.AddTimer(timerId, ref timer);

            void CancelAction()
            {
                if (self.Remove(timerId))
                {
                    tcs.SetResult(true);
                }
            }

            try
            {
                cancellationToken?.Add(CancelAction);
                await tcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);
            }

            return false;
        }

        public static bool Remove(this SkillTimeComponent self, ref long id)
        {
            long i = id;
            id = 0;
            return self.Remove(i);
        }

        private static bool Remove(this SkillTimeComponent self, long id)
        {
            if (id == 0)
            {
                return false;
            }

            if (!self.timerActions.Remove(id, out TimerAction _))
            {
                return false;
            }

            return true;
        }

        private static void AddTimer(this SkillTimeComponent self, long timerId, ref TimerAction timer)
        {
            long tillTime = timer.StartTime + timer.Time;
            self.timeId.Add(tillTime, timerId);
            self.timerActions.Add(timerId, timer);
            if (tillTime < self.minTime)
            {
                self.minTime = tillTime;
            }
        }

        private static void Run(this SkillTimeComponent self, long timerId, ref TimerAction timerAction)
        {
            switch (timerAction.TimerClass)
            {
                // case TimerClass.OnceTimer:
                // {
                //     EventSystem.Instance.Invoke(timerAction.Type, new TimerCallback() { Args = timerAction.Object });
                //     break;
                // }
                case TimerClass.OnceWaitTimer:
                {
                    ETTask<bool> tcs = timerAction.Object as ETTask<bool>;
                    tcs.SetResult(false);
                    break;
                }
                // case TimerClass.RepeatedTimer:
                // {
                //     long timeNow = self.GetNow();
                //     timerAction.StartTime = timeNow;
                //     self.AddTimer(timerId, ref timerAction);
                //     EventSystem.Instance.Invoke(timerAction.Type, new TimerCallback() { Args = timerAction.Object });
                //     break;
                // }
            }
        }
    }
}