using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class PlayDamageAnimEventHandler : AEvent<Scene, PlayDamageAnim>
    {
        protected override async ETTask Run(Scene scene, PlayDamageAnim a)
        {
            float damage = a.Damage;

            float currentHP = a.CurrentHP;

            float maxValue = a.MaxHP;

            Entity entity = a.Entity;

            ObjectComponent objectComponent = entity.GetComponent<ObjectComponent>();

            // moveObjectComponent.PlayAnim();

            HPBarComponent hpBarComponent = entity.GetComponent<HPBarComponent>();

            if (hpBarComponent == null)
            {
                hpBarComponent = entity.AddComponent<HPBarComponent, GameObject>(objectComponent.GameObject);
            }

            hpBarComponent.SetBarValue(currentHP, maxValue);

            Vector3 herdPos = objectComponent.GetHeadPos();

            EventSystem.Instance.Publish(scene, new PlayDamageText()
            {
                StartPos = herdPos, Text = damage.ToString()
            });

            await ETTask.CompletedTask;
        }
    }
}