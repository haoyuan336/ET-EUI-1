namespace ET.Client
{
    [ComponentOf(typeof(Entity))]
    public class SkillComponent : Entity, IAwake<int, int>
    {
        public int ConfigId;

        public int Level;

        public int OwnerConfigId;

        public Skill CurrentSkill;
    }
}