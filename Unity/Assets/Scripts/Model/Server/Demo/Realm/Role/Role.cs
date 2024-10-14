namespace ET.Server
{
    [ChildOf(typeof (RoleComponent))]
    public class Role: Entity, IAwake, ISerializeToEntity
    {
        public int ZoneConfigId;

        public string RoleName;

        public int HeroConfigId;

        public long LastLoginTime; //最后一次登录的时间
        
    }
}