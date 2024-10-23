namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class HideEnemyObjectEventHandler : AEvent<Scene, HideEnemyObject>
    {
        protected override async ETTask Run(Scene scene, HideEnemyObject a)
        {
            Enemy enemy = a.Enemy;

            ObjectComponent objectComponent = enemy.GetComponent<ObjectComponent>();

            if (objectComponent != null)
            {
                objectComponent.Dispose();

                enemy.GetComponent<MoveComponent>().Dispose();

                enemy.GetComponent<PatrolComponent>().Dispose();

                enemy.GetComponent<AIComponent>().Dispose();

                enemy.GetComponent<TrackComponent>().Dispose();

                enemy.GetComponent<AttackComponent>().Dispose();
            }

            await ETTask.CompletedTask;
        }
    }
}