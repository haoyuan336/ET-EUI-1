namespace ET.Client
{
    [ComponentOf(typeof(HeroCard))]
    public class CutTreeComponent : Entity, IAwake, IUpdate
    {
        public AIComponent AIComponent;

        public Tree TargetTree;

        public bool IsCuting = false;
    }
}