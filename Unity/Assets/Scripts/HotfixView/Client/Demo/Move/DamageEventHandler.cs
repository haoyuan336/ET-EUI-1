namespace ET.Client
{
    public class DamageEventHandler: AEvent<Scene, DamageEvent>
    {
        protected override async ETTask Run(Scene scene, DamageEvent a)
        {
            Skill skill = a.Skill;
            
            

            await ETTask.CompletedTask;
        }
    }
}