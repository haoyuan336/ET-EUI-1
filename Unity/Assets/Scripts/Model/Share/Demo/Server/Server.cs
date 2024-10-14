namespace ET.Server
{
    [ChildOf(typeof (ServerManagerComponent))]
    public class Server: Entity, IAwake, ISerializeToEntity
    {
        public int ZoneConfigId;

        public int ServerState = ServerStateType.Stop.GetHashCode();

        public string ServerName;

        public long StartTimeStamp = TimeInfo.Instance.ServerNow();
    }
}