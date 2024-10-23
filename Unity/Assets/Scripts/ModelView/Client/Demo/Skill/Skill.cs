namespace ET.Client
{
    [ChildOf(typeof(SkillComponent))]
    public class Skill : Entity, IAwake<int>
    {
        public int ConfigId = 0;

        public SkillConfig Config => SkillConfigCategory.Instance.Get(this.ConfigId);

        public int Level = 0; //等级

        public long CastTimeStamp = 0;
    }
}