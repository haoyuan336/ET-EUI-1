using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class DiffuserHeroEventHandler : AEvent<Scene, DiffuseHero>
    {
        protected override async ETTask Run(Scene scene, DiffuseHero a)
        {
            Log.Debug("DiffuseHero");
            //
            // TimerComponent timerComponent = scene.Root().GetComponent<TimerComponent>();
            //
            // Unit unit = a.Unit;
            //
            // HeroCardComponent heroCardComponent = unit.GetComponent<HeroCardComponent>();
            //
            // int layerMask = LayerMask.GetMask("Hero");
            //
            // Log.Debug($"diffusehero {layerMask}");
            //
            // while (true)
            // {
            //     List<GameObject> gameObjects = new List<GameObject>();
            //
            //     foreach (var card in heroCardComponent.FormationHeroCards)
            //     {
            //         if (card == null || card.IsDisposed)
            //         {
            //             continue;
            //         }
            //
            //         HeroCardObjectComponent heroCardObjectComponent = card.GetComponent<HeroCardObjectComponent>();
            //
            //         for (int i = 0; i < 6; i++)
            //         {
            //             Vector3 dir = Quaternion.Euler(0, 60 * i, 0) * Vector3.forward;
            //
            //             bool isHited = Physics.SphereCast(heroCardObjectComponent.GameObject.transform.position, 1f, dir, out RaycastHit hit,
            //                 1f,
            //                 layerMask);
            //
            //             if (isHited && !gameObjects.Contains(hit.transform.gameObject) &&
            //                 hit.transform.name != heroCardObjectComponent.GameObject.name)
            //             {
            //                 gameObjects.Add(hit.transform.gameObject);
            //             }
            //         }
            //     }
            //
            //     Log.Debug($"gameobject count {gameObjects.Count}");
            //     if (gameObjects.Count == 0)
            //     {
            //         return;
            //     }
            //
            //     Vector3 sumPos = Vector3.zero;
            //
            //     foreach (var gameObject in gameObjects)
            //     {
            //         sumPos += new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            //     }
            //
            //     sumPos /= gameObjects.Count;
            //
            //     foreach (var gameObject in gameObjects)
            //     {
            //         Vector3 direction = (sumPos - gameObject.transform.position).normalized;
            //
            //         gameObject.transform.position += direction * Time.deltaTime * 4;
            //     }
            //
            //     await timerComponent.WaitFrameAsync();
            // }

            await ETTask.CompletedTask;
        }
    }
}