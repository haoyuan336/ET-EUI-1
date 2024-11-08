using System;
using System.Collections.Generic;
using System.Reflection;
using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(SkillComponent))]
    public static partial class SkillComponentSystem
    {
        [EntitySystem]
        public static void Awake(this SkillComponent self, int ownerConfigId, int level)
        {
            self.Level = level;

            self.OwnerConfigId = ownerConfigId;

            List<SkillConfig> skillConfigs = SkillConfigCategory.Instance.GetByOnwerConfigs(ownerConfigId);

            for (int i = 0; i < skillConfigs.Count; i++)
            {
                SkillConfig skillConfig = skillConfigs[i];

                Skill skill = self.AddChild<Skill, int>(skillConfig.Id);

                skill.Level = self.GetCurrentSkillLevel(i);
            }
        }

        public static void UpdateLevel(this SkillComponent self, int level)
        {
            self.Level = level;

            List<SkillConfig> skillConfigs = SkillConfigCategory.Instance.GetByOnwerConfigs(self.OwnerConfigId);

            for (int i = 0; i < skillConfigs.Count; i++)
            {
                SkillConfig skillConfig = skillConfigs[i];

                Skill skill = self.GetChild<Skill>(skillConfig.Id);

                if (skill == null)
                {
                    continue;
                }

                skill.Level = self.GetCurrentSkillLevel(i);
            }
        }

        public static int GetCurrentSkillLevel(this SkillComponent self, int index)
        {
            SkillLevelConfig skillLevelConfig = SkillLevelConfigCategory.Instance.Get(self.Level);

            Type type = skillLevelConfig.GetType();

            PropertyInfo propertyInfo = type.GetProperty($"Skill{index + 1}Level");

            int level = Convert.ToInt32(propertyInfo.GetValue(skillLevelConfig));

            return level;
            // return skillLevelConfig.SkillLevel;
        }

        public static Skill GetCurrentSkill(this SkillComponent self)
        {
            if (self.CurrentSkill != null && self.CurrentSkill.GetIsReady())
            {
                return self.CurrentSkill;
            }

            foreach (var kv in self.Children)
            {
                Skill skill = kv.Value as Skill;

                bool isReady = skill.GetIsReady();

                if (isReady)
                {
                    self.CurrentSkill = skill;
                    break;
                }
            }

            return self.CurrentSkill;
        }
    }
}