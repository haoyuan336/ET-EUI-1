namespace ET.Client
{
    [ComponentOf(typeof(HeroCard))]
    public class CutTreeComponent : Entity, IAwake
    {
        public AIComponent AIComponent;

        public Tree TargetTree;

        public bool IsCuting = false;
    }
}