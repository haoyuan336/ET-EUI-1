namespace ET.Server
{
    [ChildOf(typeof (PlayerComponent))]
    public sealed class Player: Entity, IAwake<string>
    {
        public string Account { get; set; }

        public int ZoneConfigId = 0;
    }
}