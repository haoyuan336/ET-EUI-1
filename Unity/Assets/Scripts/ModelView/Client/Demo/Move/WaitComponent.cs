namespace ET.Client
{
    [ComponentOf(typeof(Entity))]
    public class WaitComponent: Entity, IAwake
    {
        public AIComponent AIComponent;
    }
}