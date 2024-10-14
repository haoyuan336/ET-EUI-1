namespace ET.Server
{
    [ComponentOf(typeof (Account))]
    public class RoleComponent: Entity, IAwake, ISerializeToEntity
    {
    }
}