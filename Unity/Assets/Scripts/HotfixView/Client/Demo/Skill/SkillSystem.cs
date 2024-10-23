using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(Skill))]
    public static partial class SkillSystem
    {
        [EntitySystem]
        public static void Awake(this Skill self, int configId)
        {
            self.ConfigId = configId;
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
    }
}