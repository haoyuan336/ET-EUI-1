using SharpCompress.Common;
using UnityEngine;

namespace ET.Client
{
    public enum AttackLogicType
    {
        SingleTarget = 1
    }

    [Event(SceneType.Demo)]
    public class AttackEventHandler : AEvent<Scene, AttackEvent>
    {
        protected override async ETTask Run(Scene scene, AttackEvent a)
        {
            Skill skill = a.Skill;

            SkillLogicConfig skillLogicConfig = a.LogicConfig;

            AttackLogicType logicType = EnumHelper.FromString<AttackLogicType>(skillLogicConfig.LogicParam1);

            Log.Debug($"logic type {logicType}");

            switch (logicType)
            {
                case AttackLogicType.SingleTarget:

                    this.AttackSingleRole(skill, skillLogicConfig);

                    break;
            }

            await ETTask.CompletedTask;
        }

        private void AttackSingleRole(Skill skill, SkillLogicConfig logicConfig)
        {
            //找到目标对象
            AttackComponent objectComponent = skill.Parent.Parent.GetComponent<AttackComponent>();

            GameObject attackObject = objectComponent.AttackObject;

            string name = attackObject.name;

            long heroId = FightDataHelper.GetIdByGameObjectName(name);

            FightManagerComponent fightManagerComponent = skill.Parent.Parent.GetParent<FightManagerComponent>();

            Entity entity = fightManagerComponent.GetChild<Entity>(heroId);

            FightDataComponent beAttackDataComponent = entity.GetComponent<FightDataComponent>();

            FightDataComponent fightDataComponent = skill.Parent.Parent.GetComponent<FightDataComponent>();

            //首先计算基础伤害
            float damage = FightDataHelper.Fight(fightDataComponent, beAttackDataComponent);

            beAttackDataComponent.SubHP(damage);

            EventSystem.Instance.Publish(skill.Root(), new PlayDamageAnim()
            {
                Entity = entity, SkillConfig = skill.Config, CurrentHP = beAttackDataComponent.CurrentHP, Damage = damage,
                MaxHP = beAttackDataComponent.GetValueByType(WordBarType.Hp)
            });
        }
    }
}