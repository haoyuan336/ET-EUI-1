using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class EndMoveUnitPosEventHandler : AEvent<Scene, EndMoveUnitPos>
    {
        protected override async ETTask Run(Scene scene, EndMoveUnitPos a)
        {
            Unit unit = scene.GetComponent<UnitComponent>().MyUnit;

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            gameObjectComponent.EndMove();

            HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();

            foreach (var card in heroCardComponent.FormationHeroCards)
            {
                HeroCardObjectComponent heroCardObjectComponent = card.GetComponent<HeroCardObjectComponent>();

                if (heroCardObjectComponent != null && !heroCardObjectComponent.IsDisposed)
                {
                    heroCardObjectComponent.MoveEnd();
                }
            }

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            globalComponent.ArrowGameObject.SetActive(false);

            // TimerComponent timerComponent = scene.Root().GetComponent<TimerComponent>();
            //
            // await timerComponent.WaitAsync(1000);
            //
            // Log.Debug("EndMoveUnitPos");
            //
            EventSystem.Instance.Publish(scene.Root(), new DiffuseHero() { Unit = unit });

            await ETTask.CompletedTask;
        }
    }
}