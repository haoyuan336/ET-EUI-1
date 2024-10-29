using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class MakeEnemyRunningEventHandler : AEvent<Scene, MakeEnemyRunning>
    {
        protected override async ETTask Run(Scene scene, MakeEnemyRunning a)
        {
            Enemy enemy = a.Enemy;

            AIComponent aiComponent = enemy.GetComponent<AIComponent>();

            ObjectComponent objectComponent = enemy.GetComponent<ObjectComponent>();

            objectComponent.MakeRunning();
            
            aiComponent.EnterAIState(AIState.Patrol);

            await ETTask.CompletedTask;
        }
    }
}