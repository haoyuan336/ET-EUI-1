namespace ET
{
    public partial class MapConfigCategory
    {
        private MapConfig MainCity = null;

        public override void EndInit()
        {
            this.MainCity = this.Get(1000);
        }
        public MapConfig GetMainCity()
        {
            return this.MainCity;
        }
    }
}