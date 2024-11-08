using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class AttackEventHandler : AEvent<Scene, AttackEvent>
    {
        protected override async ETTask Run(Scene scene, AttackEvent a)
        {
            Skill skill = a.Skill;

            SkillLogicConfig skillLogicConfig = a.LogicConfig;

            this.AttackSingleRole(skill, skillLogicConfig);

            await ETTask.CompletedTask;
        }

        private void AttackSingleRole(Skill skill, SkillLogicConfig logicConfig)
        {
            //找到目标对象
            AttackComponent objectComponent = skill.Parent.Parent.GetComponent<AttackComponent>();

            List<Entity> entities = objectComponent.TargetEntities;

            for (int i = 0; i < entities.Count; i++)
            {
                Entity entity = entities[i];

                if (entity == null)
                {
                    continue;
                }

                FightDataComponent beAttackDataComponent = entity.GetComponent<FightDataComponent>();

                FightDataComponent fightDataComponent = skill.Parent.Parent.GetComponent<FightDataComponent>();

                AIComponent aiComponent = entity.GetComponent<AIComponent>();

                if (aiComponent.GetCurrentState() == AIState.Death)
                {
                    continue;
                }

                //首先计算基础伤害
                float damage = FightDataHelper.Fight(fightDataComponent, beAttackDataComponent);

                beAttackDataComponent.SubHP(damage);

                if (aiComponent.GetCurrentState() == AIState.Death)
                {
                    Entity parent = skill.Parent.Parent;

                    EventSystem.Instance.Publish(skill.Root(), new KilledEntity()
                    {
                        AttackEntity = parent, BeAttackEntity = entity
                    });
                }

                EventSystem.Instance.Publish(skill.Root(), new PlayDamageAnim()
                {
                    Entity = entity, SkillConfig = skill.Config, CurrentHP = beAttackDataComponent.CurrentHP, Damage = damage,
                    MaxHP = beAttackDataComponent.GetValueByType(WordBarType.Hp)
                });
            }
        }
    }
}