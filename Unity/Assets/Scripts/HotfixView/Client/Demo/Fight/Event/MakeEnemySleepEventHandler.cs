namespace ET.Client
{
    public struct MakeEnemySleep
    {
        public Enemy Enemy;
    }

    [Event(SceneType.Demo)]
    public class MakeEnemySleepEventHandler : AEvent<Scene, MakeEnemySleep>
    {
        protected override async ETTask Run(Scene scene, MakeEnemySleep a)
        {
            Enemy enemy = a.Enemy;

            AIComponent aiComponent = enemy.GetComponent<AIComponent>();

            aiComponent.EnterAIState(AIState.Sleep);

            HPBarComponent hpBarComponent = enemy.GetComponent<HPBarComponent>();

            if (hpBarComponent != null)
            {
                hpBarComponent.Dispose();
            }

            await ETTask.CompletedTask;
        }
    }
}