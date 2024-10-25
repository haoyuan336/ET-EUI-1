namespace ET.Client
{
    public struct AttackEvent
    {
        public Skill Skill;

        public SkillLogicConfig LogicConfig;
    }

    public struct PlayDamageAnim
    {
        public Entity Entity;

        public float Damage;

        public float MaxHP;

        public SkillConfig SkillConfig;
    }
}