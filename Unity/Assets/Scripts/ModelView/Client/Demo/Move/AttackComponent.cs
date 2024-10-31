using UnityEngine;

namespace ET.Client
{
    public class AttackComponent : Entity, IAwake, IUpdate
    {
        public AIComponent AIComponent;

        public Skill CurrentCastSkill = null;

        public Entity TargetEntity;
    }
}