namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class PlayDeadAnimEventHandler: AEvent<Scene, PlayDeadAnim>
    {
        protected override async ETTask Run(Scene scene, PlayDeadAnim a)
        {
            Entity entity = a.Entity;

            AnimComponent animComponent = entity.GetComponent<AnimComponent>();
            
            await animComponent.PlayAnim("death", false);
            
            await ETTask.CompletedTask;
        }
    }
}