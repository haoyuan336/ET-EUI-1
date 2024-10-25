using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class PlayDamageAnimEventHandler : AEvent<Scene, PlayDamageAnim>
    {
        protected override async ETTask Run(Scene scene, PlayDamageAnim a)
        {
            float damage = a.Damage;

            float maxValue = a.MaxHP;

            Entity entity = a.Entity;

            ObjectComponent objectComponent = entity.GetComponent<ObjectComponent>();

            // moveObjectComponent.PlayAnim();

            HPBarComponent hpBarComponent = entity.GetComponent<HPBarComponent>();

            if (hpBarComponent == null)
            {
                hpBarComponent = entity.AddComponent<HPBarComponent, GameObject>(objectComponent.GameObject);
            }

            hpBarComponent.SetBarValue(maxValue, damage);

            await ETTask.CompletedTask;
        }
    }
}