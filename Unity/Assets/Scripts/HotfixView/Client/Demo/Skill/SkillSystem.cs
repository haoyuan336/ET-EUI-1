using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(Skill))]
    public static partial class SkillSystem
    {
        [EntitySystem]
        public static void Awake(this Skill self, int configId)
        {
            self.ConfigId = configId;

            self.AddComponent<SkillTimeComponent>();
        }

        public static bool GetIsReady(this Skill self)
        {
            if (self.Level == 0)
            {
                return false;
            }

            long passTime = TimeInfo.Instance.ClientNow() - self.CastTimeStamp;

            return passTime >= self.Config.CDTime;
        }

        public static async ETTask Cast(this Skill self)
        {
            List<SkillLogicConfig> logicConfigs = SkillLogicConfigCategory.Instance.GetLogicConfigs(self.ConfigId, self.Level);

            Log.Debug($"cast skill {logicConfigs.Count}");

            if (logicConfigs.Count == 0)
            {
                return;
            }

            List<ETTask> tasks = new List<ETTask>();

            foreach (var logicConfig in logicConfigs)
            {
                tasks.Add(self.ProcessLogic(logicConfig));
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static async ETTask ProcessLogic(this Skill self, SkillLogicConfig config)
        {
            ETCancellationToken token = new ETCancellationToken();

            self.Tokens.Add(token);

            SkillTimeComponent skillTimeComponent = self.GetComponent<SkillTimeComponent>();

            bool isCancel = await skillTimeComponent.WaitAsync(config.DelayTime, token);

            if (isCancel)
            {
                return;
            }

            string logic = config.LogicCode;

            switch (logic)
            {
                case "PlayAnim":

                    MoveObjectComponent moveObjectComponent = self.Parent.Parent.GetComponent<MoveObjectComponent>();

                    string animName = config.LogicParam1;

                    moveObjectComponent.PlayAnim(animName, false);

                    break;

                case "Damage":

                    EventSystem.Instance.Publish(self.Root(), new DamageEvent()
                    {
                        Skill = self,

                        LogicConfig = config
                    });

                    break;
            }
        }
    }
}